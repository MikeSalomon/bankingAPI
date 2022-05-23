using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Dtos
{
    public class AccountDepositDto
    {
        [Required]
        public int AccountNumber { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Maximum deposit of $10,000.00")]
        public decimal DepositAmount { get; set; }
    }
}
