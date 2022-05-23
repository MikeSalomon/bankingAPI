using BankingSystem.API.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Entities
{
    public class TransactionLog
    {
        [Key]
        public Guid Id { get; set; }

        public TransactionType TransactionType { get; set; }


    }
}
