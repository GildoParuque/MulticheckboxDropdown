using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MulticheckboxDropdown.Models
{
    public class TeacherDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> drpSubjects { get; set; }

        [Display(Name="Subjects")]
        public long[] SubjectsIds { get; set; }
    }
}
