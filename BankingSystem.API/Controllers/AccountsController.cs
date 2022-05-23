using AutoMapper;
using BankingSystem.API.Dtos;
using BankingSystem.API.Entities;
using BankingSystem.API.Helpers;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBankRepository _repo;
        private readonly IMapper _mapper;

        public AccountsController(IBankRepository bankRepository, IMapper mapper)
        {
            _repo = bankRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAccount/{accountNumber}")]
        public ActionResult<AccountReadDto> GetAccount(int accountNumber)
        {
            var account = _repo.GetAccount(accountNumber);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpGet]
        [Route("GetAccountsForUser")]
        public ActionResult<IEnumerable<AccountReadDto>> GetAccountsForUser(Guid userId)
        {
            var user = _repo.GetUserById(userId);
            if (user == null)
                return NotFound(); 

            var accounts = _repo.GetAllUserAccounts(userId);

            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts)); 
        }

        [HttpPost]
        [Route("CreateAccount")]
        public ActionResult<AccountReadDto> CreateAccount([FromBody] AccountCreateDto accountCreateDto)
        {
            if (!TryValidateModel(accountCreateDto))
                return ValidationProblem(ModelState);

            var accountModel = _mapper.Map<Account>(accountCreateDto);
            _repo.CreateAccount(accountModel);
            _repo.SaveChanges();

            var accountReadDto = _mapper.Map<AccountReadDto>(accountModel);
            return CreatedAtAction(nameof(GetAccount), new { AccountNumber = accountModel.AccountNumber }, accountReadDto);
        }

        [HttpDelete]
        [Route("DeleteAccount{accountNumber}")]
        public ActionResult DeleteAccount(int accountNumber)
        {
            var account = _repo.GetAccount(accountNumber);

            if (account == null)
                return NotFound(); 

            _repo.DeleteAccount(account);
            _repo.SaveChanges();

            return NoContent(); 
        }

        [HttpPost]
        [Route("Deposit")]
        public ActionResult DepositFunds([FromBody] AccountDepositDto account)
        {
            if (_repo.GetAccount(account.AccountNumber) == null)
                return NotFound();

            _repo.DepositFunds(account.AccountNumber, account.DepositAmount);
            _repo.SaveChanges(); 

            return Ok(); 
        }

        [HttpPost]
        [Route("Withdraw")]
        public ActionResult WithdrawFunds([FromBody] AccountWithdrawDto withdrawDetails)
        {
            var account = _repo.GetAccount(withdrawDetails.AccountNumber);
            if (account == null)
                return NotFound();

            // validate withdraw 
            if (!ValidateWithdraw.IsValidWithdraw(account.AccountBalance, withdrawDetails.WithdrawAmount))
                return BadRequest("Withdraw not allowed"); 

            _repo.WithdrawFunds(withdrawDetails.AccountNumber, withdrawDetails.WithdrawAmount);
            _repo.SaveChanges();

            return Ok();
        }
    }
}
