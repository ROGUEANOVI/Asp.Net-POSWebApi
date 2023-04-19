﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS.Application.Commons.Bases;
using POS.Application.DTOs.User.Request;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace POS.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email!),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<string>> GenerateToken(TokenRequestDTO requestDTO)
        {
            var response = new BaseResponse<string>();
            var account = await _unitOfWork.UserRepository.AccountByUserName(requestDTO.UserName!);

            if (account is not null)
            {
                if (BC.Verify(requestDTO.Password, account.Password))
                {
                    response.IsSuccess = true;
                    response.Data = GenerateToken(account);
                    response.Message = ReplyMessage.MESSAGE_TOKEN;

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
                }

                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDTO requesDTO)
        {
            var response = new BaseResponse<bool>();
            var account = _mapper.Map<User>(requesDTO);
            account.Password = BC.HashPassword(account.Password);

            if(requesDTO.Image is not null)
            {
                account.Image = await _unitOfWork.AzureStorage.SaveFile(AzureContainers.USERS, requesDTO.Image);
            }

            response.Data = await _unitOfWork.UserRepository.RegisterAsync(account);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}
