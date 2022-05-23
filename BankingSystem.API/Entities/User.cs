using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>(); 

    }
}
