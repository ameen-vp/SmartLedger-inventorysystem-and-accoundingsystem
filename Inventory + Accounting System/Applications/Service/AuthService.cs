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
                await _authRepo.Register(user);

                return new Apiresponse<string>
                {
                    Data = null,
                    Message = "User registered successfully.",
                    Success = true,
                    Statuscode = 200
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
                var Token = Generate_Token(user);
                return new Apiresponse<UserResponsedto>
                {
                    Statuscode = 200,
                    Message = "Login sucessessfully",
                    Data = new UserResponsedto
                    {
                        Id = user.Id,
                        UserName = user.Name,
                        UserEmail = user.Email,
                        Token = Token
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, Enum.GetName(typeof(Userrole), user.Userrole))
 
            };

            var token = new JwtSecurityToken(
                 issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
    }
    }

    
