﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rentacar.Authentication;
using Rentacar.Authentication.Model;
using Rentacar.DTO;

namespace Rentacar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody, Required] RegisterDTO model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = "Error", Massage = "Girilen Kullanıcı Adı Zaten Kullanılıyor."});
            }

            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) == true ? null : model.PhoneNumber
            };
            

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new Response {Status = "Error", Massage = "Gönderilen Değerler Uyumsuz. Kullanıcı Oluşturma Başarısız"});
            }

            return Ok(new Response {Status = "Success", Massage = "Kullanıcı Başarı İle Oluşturuldu."});
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody, Required] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user,model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRole));
                }
                
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                
                var token = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"], expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));

                return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
            }

            return Unauthorized();
        }
        
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody, Required] RegisterDTO model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = "Error", Massage = "Girilen Kullanıcı Adı Zaten Kullanılıyor."});
            }

            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) == true ? null : model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new Response {Status = "Error", Massage = "Gönderilen Değerler Uyumsuz. Kullanıcı Oluşturma Başarısız"});
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response {Status = "Success", Massage = "Kullanıcı Başarı İle Oluşturuldu."});
        }
    }
}