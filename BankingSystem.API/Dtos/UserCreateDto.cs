using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankingSystem.API.Dtos
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }
    }
}
