using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    /*

    
    public class Expense
{
    public int Id { get; set; }
    public  string Description { get; set; }
    public decimal Amount { get; set; }
    public  string Category { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }  // Foreign key to User
    
    // Navigation property to the User (do not need to be included in POST data)
    public User? User { get; set; }
}
*/
public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
         public int? UserId { get; set; }

        // Ignore the User property during serialization to avoid circular references
        [JsonIgnore]
    

    // Make the navigation property optional as well
    public User? User { get; set; }
    }

}