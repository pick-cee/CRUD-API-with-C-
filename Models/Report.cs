using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Models
{
    public class Report
    {
        [Key, ForeignKey("Student, Class")]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        
        [Required]
        public string Grade { get; set; }
        [StringLength(150)]
        public string Comment { get; set; }

        public Student student { get; set; }
        public Class classes { get; set; }
    }
}
