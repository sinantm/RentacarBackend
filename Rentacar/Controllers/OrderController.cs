using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentacar.Authentication;
using Rentacar.DbCon;
using Rentacar.DTO;
using Rentacar.Models;

namespace Rentacar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RentDbContext _rentDbContext;
        private readonly IMapper _mapper;

        public OrderController(RentDbContext rentDbContext, IMapper mapper)
        {
            _rentDbContext = rentDbContext;
            _mapper = mapper;
        }
        
        //GET
        [HttpGet]
        [ProducesResponseType(typeof(OrderGetDTO), (int)HttpStatusCode.OK)]
        public IActionResult GetOrders()
        {
            return Ok(_rentDbContext.Orders.ProjectTo<OrderGetDTO>(_mapper.ConfigurationProvider));
        }
        
        //GET {Id}
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderGetDTO>> GetOrder(Guid orderId)
        {
            var query = await _rentDbContext.Orders.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            var order = _mapper.Map<OrderGetDTO>(query);
            
            if (query.OrderId != orderId)
            {
                return BadRequest();
            }
            
            if (order == null)
            {
                return NotFound();
            }
        
            return Ok(order);
        }
        
        //POST
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder([FromBody, Required]OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            var carid = "eed80cd2-cc2b-43c5-8233-79db57ef6240";
            
            order.OrderId = Guid.NewGuid();
            order.CarId = Guid.Parse(carid);

            _rentDbContext.Orders.Add(order);
            await _rentDbContext.SaveChangesAsync();

            return Ok(_mapper.Map<Order>(order));
        }
        
        //PUT
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid orderId,[FromBody, Required] OrderDTO model)
        {
            if (!await _rentDbContext.Orders.AnyAsync(x => x.OrderId == orderId))
            {
                return NotFound();
            }

            var order = await _rentDbContext.Orders.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();
            
            order = _mapper.Map(model,order);

            _rentDbContext.Orders.Update(order);
            await _rentDbContext.SaveChangesAsync();
        
            return Ok(_mapper.Map<Order>(order));
        }
        
    }
}