using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUDwebAPI.Models
{
    public class Customer
    {
        [Required]
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }

        public Customer()
        {

        }
    }
}
