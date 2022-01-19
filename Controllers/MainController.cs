using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MulticheckboxDropdown.Database;
using MulticheckboxDropdown.Models;

using Microsoft.EntityFrameworkCore;


namespace MulticheckboxDropdown.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext db;
        public MainController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var teacherList = db.Teacher.ToList();
            return View(teacherList);
        }


        public IActionResult AddTeacher(long? Id)
        {
            TeacherDto model = new TeacherDto(); List<long> subjectsIds = new List<long>();
            if (Id.HasValue)
            {
                //Get teacher 
                var teacher = db.Teacher.Include("TeacherSubjects").FirstOrDefault(x => x.Id == Id.Value);
                //Get teacher subjects and add each subjectId into subjectsIds list
                teacher.TeacherSubjects.ToList().ForEach(result => subjectsIds.Add(result.SubjectId));

                //bind model 
                model.drpSubjects = db.Subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                model.Id = teacher.Id;
                model.Name = teacher.Name;
                model.SubjectsIds = subjectsIds.ToArray();
            }
            else
            {
                model = new TeacherDto();
                model.drpSubjects = db.Subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddTeacher(TeacherDto model)
        {
            Teacher teacher = new Teacher();
            List<TeacherSubjects> teacherSubjects = new List<TeacherSubjects>();


            if (model.Id > 0)
            {
                //first find teacher subjects list and then remove all from db 
                teacher = db.Teacher.Include("TeacherSubjects").FirstOrDefault(x => x.Id == model.Id);
                teacher.TeacherSubjects.ToList().ForEach(result => teacherSubjects.Add(result));
                db.TeacherSubjects.RemoveRange(teacherSubjects);
                db.SaveChanges();

                //Now update teacher details
                teacher.Name = model.Name;
                if (model.SubjectsIds.Length > 0)
                {
                    teacherSubjects = new List<TeacherSubjects>();

                    foreach (var subjectid in model.SubjectsIds)
                    {
                        teacherSubjects.Add(new TeacherSubjects { SubjectId = subjectid, TeacherId = model.Id });
                    }
                    teacher.TeacherSubjects = teacherSubjects;
                }
                db.SaveChanges();

            }
            else
            {
                teacher.Name = model.Name;
                teacher.DateTimeInLocalTime = DateTime.Now;
                teacher.DateTimeInUtc = DateTime.UtcNow;
                if (model.SubjectsIds.Length > 0)
                {
                    foreach (var subjectid in model.SubjectsIds)
                    {
                        teacherSubjects.Add(new TeacherSubjects { SubjectId = subjectid, TeacherId = model.Id });
                    }
                    teacher.TeacherSubjects = teacherSubjects;
                }
                db.Teacher.Add(teacher);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
