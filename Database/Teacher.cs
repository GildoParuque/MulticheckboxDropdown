using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulticheckboxDropdown.Database
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeacherSubjects = new HashSet<TeacherSubjects>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTimeInLocalTime { get; set; }
        [Column("DateTimeInUTC", TypeName = "datetime")]
        public DateTime DateTimeInUtc { get; set; }

        [InverseProperty("Teacher")]
        public virtual ICollection<TeacherSubjects> TeacherSubjects { get; set; }
    }
}
