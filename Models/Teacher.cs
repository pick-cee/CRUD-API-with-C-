using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string Full_name { get; set; }
        public int age { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        public Class classes { get; set; }
        public ICollection<Student> student { get; set; }
    }
}
