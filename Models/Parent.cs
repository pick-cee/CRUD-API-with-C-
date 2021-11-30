using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Full_name { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone_Number { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Student> student { get; set; }

    }
}
