using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulticheckboxDropdown.Database
{
    public partial class Subjects
    {
        public Subjects()
        {
            TeacherSubjects = new HashSet<TeacherSubjects>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<TeacherSubjects> TeacherSubjects { get; set; }
    }
}
