using BankingSystem.API.Enums;
using System;

namespace BankingSystem.API.Dtos
{
    public class AccountReadDto
    {

        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid UserId { get; set; }
    }
}
