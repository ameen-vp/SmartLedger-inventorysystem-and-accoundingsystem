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
using Applications.Helpers;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.IO;

namespace Applications.Service
{
    public class AuthService : Iauthservice
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepo _authRepo;
        private readonly IMapper _mapper;

        private readonly byte[] _encryptionKey;
        private readonly byte[] _encryptionIV;

        public AuthService(IConfiguration configuration, IMapper mapper, IAuthRepo authRepo)
        {
            _configuration = configuration;
            _mapper = mapper;
            _authRepo = authRepo;

            _encryptionKey = Convert.FromBase64String(_configuration["EncryptionSettings:Key"]);
            _encryptionIV = Convert.FromBase64String(_configuration["EncryptionSettings:IV"]);
        }
        private string HashEmail(string email)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(email.ToLower().Trim());
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public async Task<Apiresponse<string>> Register(UserregisterDto userregisterDto)
        {
            try
            {

                userregisterDto.Name = userregisterDto.Name.Trim().ToUpper();
                var hashedEmail = HashEmail(userregisterDto.Email);
                userregisterDto.Email = hashedEmail;
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
                user.RefreshToken = SymmetricEncryptionHelper.Encrypt(refreshToken, _encryptionKey, _encryptionIV);

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


                var hashedEmail = HashEmail(logindto.Email);
                var user = await _authRepo.Getemail(hashedEmail);
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

                user.RefreshToken = SymmetricEncryptionHelper.Encrypt(refreshToken, _encryptionKey, _encryptionIV);

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
                        UserEmail = logindto.Email,
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

                var hashedEmail = HashEmail(email);
                var user = await _authRepo.Getemail(hashedEmail);

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

                var decryptedStoredToken = SymmetricEncryptionHelper.Decrypt(user.RefreshToken, _encryptionKey, _encryptionIV);
                if (decryptedStoredToken != refreshTokenRequestDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
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

                user.RefreshToken = SymmetricEncryptionHelper.Encrypt(newRefreshToken, _encryptionKey, _encryptionIV);

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
        private string EncryptString(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _encryptionKey;
            aes.IV = _encryptionIV;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        private string DecryptString(string cipherText)
        {
            var buffer = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = _encryptionKey;
            aes.IV = _encryptionIV;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }


    }
}

    
