using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Repositories;
using MyApp.Models;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;

        public HomeController(ITeacherRepository teachRepository, IStudentRepository studentRepository)
        {
            _teacherRepository = teachRepository;
            _studentRepository = studentRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var teachers = _teacherRepository.GetAllTeachers();

            var viewModel = new StudentTeacherViewModel()
            {
                Student = new Student(),
                Teachers = teachers
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Student()
        {
            var studnets = _studentRepository.GetAllStudents();

            var viewModel = new StudentTeacherViewModel()
            {
                Student = new Student(),
                Students = studnets
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Student(StudentTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                // model 데이터를 Student 테이블에 저장
                _studentRepository.AddStudent(model.Student);
                _studentRepository.Save();
                ModelState.Clear();
            }
            else
            {
                // 에러를 보여준다
            }

            var students = _studentRepository.GetAllStudents();
            var viewModel = new StudentTeacherViewModel()
            {
                Student = new Student(),
                Students = students
            };

            return View(viewModel);
        }

        public IActionResult Detail(int id)
        {
            var result = _studentRepository.GetStudent(id);
            return View(result);
        }

        public IActionResult Edit(int id)
        {
            var result = _studentRepository.GetStudent(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Edit(student);
                _studentRepository.Save();

                return RedirectToAction("Student");
            }

            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var result = _studentRepository.GetStudent(id);

            if (result != null)
            {
                _studentRepository.Delete(result);
                _studentRepository.Save();
            }

            return RedirectToAction("Student");
        }
   }
}
