using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using BCrypt.Net;
using Domain.Enum;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Applications.Service
{
    public class AuthService : Iauthservice
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepo _authRepo;
        private readonly IMapper _mapper;

       

        public AuthService(IConfiguration configuration, IMapper mapper, IAuthRepo authRepo)
        {
            _configuration = configuration;
            _mapper = mapper;
            _authRepo = authRepo;
        }

        public async Task<Apiresponse<string>> Register(UserregisterDto userregisterDto)
        {
            try
            {

                userregisterDto.Name = userregisterDto.Name.Trim().ToUpper();
                userregisterDto.Email = userregisterDto.Email.Trim().ToLower();
                userregisterDto.Password = userregisterDto.Password.Trim();


                var exists = await _authRepo.UserExits(userregisterDto.Email);
                if (exists)
                {
                    return new Apiresponse<string>
                    {
                        Data = null,
                        Message = "User already exists.",
                        Success = false,
                        Statuscode = 400
                    };
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userregisterDto.Password);
                var user = _mapper.Map<User>(userregisterDto);
                user.Password = hashedPassword;
                string refreshToken = Generate_RefreshToken();
                var refreshTokenExpiryDays = int.Parse(_configuration["JwtSettings:RefreshTokenExpiryDays"]);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

                await _authRepo.Register(user);

                return new Apiresponse<string>
                {
                    Data = refreshToken,
                    Message = "User registered successfully.",
                    Success = true,
                    Statuscode = 200,
                
                };
            }
            catch (Exception ex)
            {


                return new Apiresponse<string>
                {
                    Data = null,
                    Message = $"An error occurred: {ex.Message}",
                    Success = false,
                    Statuscode = 500
                };
            }
        }
        public async Task<Apiresponse<UserResponsedto>> Loginuser(Logindto logindto)
        {
            try
            {
                logindto.Email = logindto.Email.ToLower().Trim();
                logindto.Password = logindto.Password.Trim();

                var user = await _authRepo.Getemail(logindto.Email);
                if (user == null)
                {
                    return new Apiresponse<UserResponsedto>{Data = null, Message = "User not found", Statuscode = 404, Success = false};
                }
                var pas = BCrypt.Net.BCrypt.Verify(logindto.Password, user.Password);
                if (!pas)
                {
                    return new Apiresponse<UserResponsedto> { Data = null, Message = "Invalid Password", Statuscode = 400, Success = false };
                }
                var token = Generate_Token(user);
                var refreshToken = Generate_RefreshToken();
                var refreshTokenExpiryDays = int.Parse(_configuration["JwtSettings:RefreshTokenExpiryDays"]);
                var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
                await _authRepo.Updateuser(user);


                return new Apiresponse<UserResponsedto>
                {
                    Statuscode = 200,
                    Message = "Login successfully",
                    Success = true,
                    Data = new UserResponsedto
                    {
                        Id = user.Id,
                        UserName = user.Name,
                        UserEmail = user.Email,
                        Token = token,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiryTime = refreshTokenExpiryTime
                    }
                };



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            }
        private string Generate_Token(User user)
        {
            var key = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:AccessTokenExpiryMinutes");


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email)
    };

            if (Enum.IsDefined(typeof(Userrole), user.Userrole))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Userrole.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Generate_RefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }


        public async Task<Apiresponse<string>> Deleteuser(string name, int userid)
        {
            var res = await _authRepo.Deleteuser(name);
            if (!res)
            {
                return new Apiresponse<string>
                {   Success = false,
                    Message = "User not found",
                    Statuscode = 400
                };
            }
            return new Apiresponse<string>
            {
                Success = true,
                Message = "Deleted sucssess fully",
                Statuscode = 200

            };
        }
       public async Task<List<User>> Getusers()
        {
            return await _authRepo.Getusers();
         
        }
        public async Task<Apiresponse<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(refreshTokenRequestDto.AccessToken);
                var email = principal?.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return new Apiresponse<TokenResponseDto>
                    {
                        Data = null,
                        Message = "Invalid token claims",
                        Success = false,
                        Statuscode = 400
                    };
                }

                var user = await _authRepo.Getemail(email);
                if (user == null)
                {
                    return new Apiresponse<TokenResponseDto>
                    {
                        Data = null,
                        Message = "User not found",
                        Success = false,
                        Statuscode = 404
                    };
                }

                if (user.RefreshToken != refreshTokenRequestDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    return new Apiresponse<TokenResponseDto>
                    {
                        Data = null,
                        Message = "Invalid refresh token",
                        Success = false,
                        Statuscode = 400
                    };
                }

                var newAccessToken = Generate_Token(user);
                var newRefreshToken = Generate_RefreshToken();

                var refreshTokenExpiryDays = _configuration.GetValue<int>("JwtSettings:RefreshTokenExpiryDays");

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);
                await _authRepo.Updateuser(user);

                return new Apiresponse<TokenResponseDto>
                {
                    Data = new TokenResponseDto
                    {
                        AccessToken = newAccessToken,
                        RefreshToken = newRefreshToken,
                        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                    },
                    Message = "Token refreshed successfully",
                    Success = true,
                    Statuscode = 200
                };
            }
            catch (Exception ex)
            {
                return new Apiresponse<TokenResponseDto>
                {
                    Data = null,
                    Message = $"An error occurred: {ex.Message}",
                    Success = false,
                    Statuscode = 500
                };
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"])),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}

    
