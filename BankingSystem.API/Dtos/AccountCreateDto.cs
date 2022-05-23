using BankingSystem.API.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Dtos
{
    public class AccountCreateDto
    {
        [Required]
        public AccountType AccountType { get; set; }
        [Required]
        [Range(100.0, Double.MaxValue, ErrorMessage = "Minimum balance of $100.00")]
        public decimal AccountBalance { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
