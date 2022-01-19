using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulticheckboxDropdown.Database
{
    public partial class TeacherSubjects
    {
        [Key]
        public long Id { get; set; }
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        [InverseProperty(nameof(Subjects.TeacherSubjects))]
        public virtual Subjects Subject { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty("TeacherSubjects")]
        public virtual Teacher Teacher { get; set; }
    }
}
