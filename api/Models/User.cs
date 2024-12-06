using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public  string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        public List<Budget> Budgets { get; set; } = new List<Budget>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
