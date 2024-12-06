using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
   
  public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Foreign key to User
        public decimal MonthlyBudget { get; set; }
        public decimal CurrentSpending { get; set; }
        public DateTime MonthYear { get; set; }

        // Navigation property to the User
        public User? User { get; set; }
    }
}