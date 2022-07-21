using Microsoft.AspNetCore.Mvc;
using Domain.Main;
using AutoMapper;
using Domain.Main.DTO;
using Application.Common.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : EntityControllerBase<Customer, CustomerDTO, CustomerForCreationDTO, CustomerForUpdateDTO, int>
    {
        public CustomersController(IEntityService<Customer> entityService, ILogger<EntityControllerBase<Customer, CustomerDTO, CustomerForCreationDTO, CustomerForUpdateDTO, int>> logger, IMapper mapper)
            : base(entityService, logger, mapper, "Customer")
        {
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<CustomerDTO>> GetById(int id, bool includeChildren = false)
        {
            return await base.GetById(id, includeChildren);
        }
    }
}