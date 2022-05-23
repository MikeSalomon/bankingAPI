using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Dtos
{
    public class AccountWithdrawDto
    {
        [Required]
        public int AccountNumber { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum withdraw of $1.00")]
        public decimal WithdrawAmount { get; set; }
    }
}
