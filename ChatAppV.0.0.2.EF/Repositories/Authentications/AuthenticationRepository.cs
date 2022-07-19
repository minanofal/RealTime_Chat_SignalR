using AutoMapper;
using ChatAppV._0._0._2.Core.DTOS.Inputs;
using ChatAppV._0._0._2.Core.DTOS.Inputs.Authentication;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Authentication;
using ChatAppV._0._0._2.Core.Interfaces.Authentication;
using ChatAppV._0._0._2.Core.Interfaces.Authentication.IServices;
using ChatAppV._0._0._2.Core.Models.Auithentication;
using ChatAppV._0._0._2.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories.Authentications
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IMapper _mapper;
        private readonly IJWTServices _jwt;
        public AuthenticationRepository(UserManager<ApplicationUser> userManger , IMapper mapper , IJWTServices jwt , ApplicationDbContext context)
        {
            _userManger = userManger;
            _mapper = mapper;
            _jwt = jwt;
        }



        public async Task<Auther> Login(LoginDto dto)
        {
            Auther auther = new Auther() { Message = String.Empty };
            var User = await _userManger.FindByEmailAsync(dto.Email);
            if (User == null)
                auther.Message = "Email or Password is Incorrect";
            if(!await _userManger.CheckPasswordAsync(User, dto.Password))
                auther.Message = "Email or Password is Incorrect";

            if (auther.Message != String.Empty)
                return auther;

            var token = await _jwt.CreateJwtToken(User);

            auther.Id = User.Id;
            auther.Email = User.Email;
            auther.Expire = token.ValidTo;
            auther.FirstName = User.FirstName;
            auther.UserName = User.UserName;
            auther.LastName = User.LastName;
            auther.Image = User.ImageUrl;
            auther.IsAuthenticated = true;
            auther.Token = new JwtSecurityTokenHandler().WriteToken(token);
            auther.Message = "Login Successfully !";

            return auther;

        }

        public async Task<Auther> Register(RegisterDto dto)
        {
            Auther auther = new Auther() { Message = String.Empty};
            if (await _userManger.FindByEmailAsync(dto.Email) != null)
                auther.Message = $"{dto.Email} is already Exist";
            if (await _userManger.FindByNameAsync(dto.UserName) != null)
                auther.Message = $" {dto.UserName} is already Exist";
            var gender = GenderServices.Services(dto.Gender);
            if(gender == 'W')
                auther.Message = $" {dto.Gender} is not Male Or Female";


            if (auther.Message != String.Empty)
                return auther;

            var User = _mapper.Map<ApplicationUser>(dto);
            if (gender == 'm')
                User.ImageUrl = ImageServices.IntialImageMale;
            else
                User.ImageUrl = ImageServices.IntialImageFemale;

            User.Gender = gender;

            var result = await _userManger.CreateAsync(User,dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += " " + error.Description;
                auther.Message = errors;
                return auther;
            }
            var token = await _jwt.CreateJwtToken(User);

            auther.Id = User.Id;
            auther.Email = User.Email;
            auther.Expire = token.ValidTo;
            auther.FirstName = User.FirstName;
            auther.UserName = User.UserName;
            auther.LastName = User.LastName;
            auther.Image = User.ImageUrl;
            auther.IsAuthenticated = true;
            auther.Token = new JwtSecurityTokenHandler().WriteToken(token);
            auther.Message = "Register Successfully !";

            return auther;
        }

        public async Task<string> UpdateImage(UpdateImageDto dto)
        {
            var user = await _userManger.FindByNameAsync(dto.Name);
            if (user == null)
                return null;
            var Message = ImageServices.CheckImageValidation(dto.Image);
            if (Message != String.Empty)
            {
                return null ;
            }
            var oldImage = user.ImageUrl;
            ImageServices.DeleteImage(oldImage);
            var image = ImageServices.UploadImage(dto.Image);
            user.ImageUrl = image;
           await _userManger.UpdateAsync(user);
            return user.ImageUrl;

        }
    }
}
