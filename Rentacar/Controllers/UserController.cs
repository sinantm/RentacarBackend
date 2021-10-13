using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentacar.Authentication;
using Rentacar.Authentication.Model;
using Rentacar.DTO;

namespace Rentacar.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public UserController(UserDbContext userDbContext,IMapper mapper,UserManager<AppUser> userManager)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        //GET
        //[Authorize(Roles = "Admin")]
        [HttpGet("UserAdmins")]
        [ProducesResponseType(typeof(AppUser), (int)HttpStatusCode.OK)]
        public IActionResult GetUserAdmins()
        {
            // var query = _userDbContext.Users.Join(_userDbContext.UserRoles, user => user.Id, role => role.UserId,
            //     (user, role) => user);
            
            var query = _userDbContext.Users.Where(user => _userDbContext.UserRoles.Select(role => role.UserId).Contains(user.Id));

            return Ok(query.ProjectTo<AppUser>(_mapper.ConfigurationProvider));
            //return Ok(_userDbContext.Users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider));
        }
        
        //GET
        [HttpGet("Users")]
        [ProducesResponseType(typeof(AppUser), (int)HttpStatusCode.OK)]
        public IActionResult GetUser()
        {
            var query = _userDbContext.Users.Where(user => !_userDbContext.UserRoles.Select(role => role.UserId).Contains(user.Id));

            return Ok(query.ProjectTo<AppUser>(_mapper.ConfigurationProvider));
            //return Ok(_userDbContext.Users.ProjectTo<AppUser>(_mapper.ConfigurationProvider));
        }
        
        //GET {Id}
        [HttpGet("{userId}")]
        public async Task<ActionResult<AppUser>> GetUserId(string userId)
        {
            var query = await _userDbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            var user = _mapper.Map<AppUser>(query);
            
            if (user == null)
            {
                return NotFound();
            }
            
            if (query.Id != userId)
            {
                return BadRequest();
            }
        
            return Ok(_mapper.Map<AppUser>(user));
        }
        
        //PUT
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId,[FromBody, Required] UserDTO model)
        {
            if (!await _userDbContext.Users.AnyAsync(x => x.Id == userId))
            {
                return NotFound();
            }
             
            var userExist = await _userManager.FindByNameAsync(model.UserName);

            //KULLANICI KENDİ KULLANICI ADINI GÜNCELLEYEBİLMESİ GEREKİYOR.
            if (userExist != null)
            {
                
            }

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = "Error", Massage = "Girilen Kullanıcı Adı Zaten Kullanılıyor."});
            }

            var user = await _userDbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            
            
            
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.NormalizedUserName = model.UserName.ToUpper();
            user.NormalizedEmail = model.Email.ToUpper();
            user.PhoneNumber = model.PhoneNumber;

            _userDbContext.Users.Update(user);
            await _userDbContext.SaveChangesAsync();
        
            return Ok(_mapper.Map<AppUser>(user));
        }
        
        //DELETE
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = await _userDbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AppUser>(user));
        }

    }
}