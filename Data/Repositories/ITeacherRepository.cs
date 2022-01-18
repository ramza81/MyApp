using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Data.Repositories
{
    public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetAllTeachers();
        Teacher GetTeacher(int id);
    }
}