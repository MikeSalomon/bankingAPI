using BankingSystem.API.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.API.Entities
{
    public class Account
    {
        private readonly Random rand = new Random();

        public Account()
        {
            AccountNumber = rand.Next(1000, 100000); 
        }

        [Key]
        public Guid Id { get; set; }


        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [Range(100.00, double.MaxValue, ErrorMessage = "Minimum balance of $100 is required.")]
        public decimal AccountBalance { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }

    }
}
