﻿using System.ComponentModel.DataAnnotations;

namespace MVCBookShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }


        public string? Name { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
