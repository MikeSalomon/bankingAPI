using AutoMapper;
using BankingSystem.API.Dtos;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBankRepository _repo;
        private readonly IMapper _mapper; 

        public UsersController(IBankRepository bankRepository, IMapper mapper)
        {
            _repo = bankRepository ??
                throw new ArgumentNullException(nameof(bankRepository)); ;  
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public ActionResult<UserReadDto> GetUserById(Guid id)
        {
            var user = _repo.GetUserById(id);
            if (user == null)
                return NotFound(); 

            return Ok(_mapper.Map<UserReadDto>(user)); 
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult<UserReadDto> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!TryValidateModel(userCreateDto))
                return ValidationProblem(ModelState);

            if (_repo.UserExists(userCreateDto.Email))
                return BadRequest($"User with email address {userCreateDto.Email} already exists."); 

            var userModel = _mapper.Map<User>(userCreateDto);
            _repo.CreateUser(userModel);
            _repo.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel); 
            return CreatedAtAction(nameof(GetUserById), new { Id = userReadDto.Id}, userReadDto);
        }
    }
}
