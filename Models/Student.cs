using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string F_Nmae { get; set; }
        [Required]
        public string L_Name { get; set; }
        [Required]
        public int age { get; set; }
        public string Gender { get; set; }
        public DateTime Date_of_Birth { get; set; } = new DateTime(1900, 1, 1);

        [ForeignKey("Parent")]
        [Required]
        public int ParentId { get; set; }
        public Parent parent { get; set; }

        public virtual ICollection<Class> classes { get; set; }

        public virtual Report report {get; set;}

        public virtual ICollection<Teacher> teacher { get; set; }
    }
}
