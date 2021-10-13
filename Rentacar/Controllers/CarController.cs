using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentacar.DbCon;
using Rentacar.DTO;
using Rentacar.Models;

namespace Rentacar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly RentDbContext _rentDbContext;
        private readonly IMapper _mapper;

        public CarController(RentDbContext rentDbContext, IMapper mapper)
        {
            _rentDbContext = rentDbContext;
            _mapper = mapper;
        }
        
        //GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarGetDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetCars()
        {
            return Ok(_rentDbContext.Cars.ProjectTo<CarGetDTO>(_mapper.ConfigurationProvider));
        }
        

        //GET {Id}
        [HttpGet("{carId}")]
        public async Task<ActionResult> GetCar(Guid carId)
        {
            var query = await _rentDbContext.Cars.Where(x => x.CarId == carId).FirstOrDefaultAsync();

            var car = _mapper.Map<CarGetDTO>(query);
            
            if (query.CarId != carId)
            {
                return BadRequest();
            }
            
            if (car == null)
            {
                return NotFound();
            }
        
            return Ok(car);
        }
        
        //POST
        [HttpPost]
        [ProducesResponseType(typeof(Car), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> PostCar([FromBody, Required]CarDTO model)
        {
            var car = _mapper.Map<Car>(model);
            car.CarId = Guid.NewGuid();
            if (User.Identity != null) car.AddedUserName = User.Identity.Name;

            _rentDbContext.Cars.Add(car);
            await _rentDbContext.SaveChangesAsync();

            return Ok(_mapper.Map<Car>(car));
        }
        
        //PUT
        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar([FromRoute] Guid carId,[FromBody, Required] CarDTO model)
        {
            if (!await _rentDbContext.Cars.AnyAsync(x => x.CarId == carId))
            {
                return NotFound();
            }

            var car = await _rentDbContext.Cars.Where(x => x.CarId == carId).FirstOrDefaultAsync();
            car = _mapper.Map(model,car);
            
            if (User.Identity != null) car.AddedUserName = User.Identity.Name;

            _rentDbContext.Cars.Update(car);
            await _rentDbContext.SaveChangesAsync();
        
            return Ok(_mapper.Map<Car>(car));
        }
        
        //DELETE
        [HttpDelete("{carId}")]
        public async Task<ActionResult> DeleteCar(Guid carId)
        {
            var query = await _rentDbContext.Cars.Where(x => x.CarId == carId).FirstOrDefaultAsync();

            var car = _mapper.Map<Car>(query);

            if (car == null)
            {
                return NotFound();
            }

            _rentDbContext.Cars.Remove(car);
            await _rentDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}