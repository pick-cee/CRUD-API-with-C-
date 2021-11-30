using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Student> student { get; set; }

        public virtual ICollection<Teacher> teacher { get; set; }
        public virtual Report report { get; set; }
    }
}
