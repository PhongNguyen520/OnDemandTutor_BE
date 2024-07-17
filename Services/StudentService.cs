using BusinessObjects;
using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository iStudentRepository;

        public StudentService()
        {
                iStudentRepository = new StudentRepository();
        }

        //---------------------------
        public StudentService(IStudentRepository _iStudentRepository)
        {
            this.iStudentRepository = _iStudentRepository;
        }

        public bool AddStudent(Student student)
        {
            return iStudentRepository.AddStudent(student);
        }

        public bool DelStudents(int id)
        {
            return iStudentRepository.DelStudents(id);
        }

        public List<Student> GetStudents()
        {
            return iStudentRepository.GetStudents();
        }

        public bool UpdateStudents(Student student)
        {
            return iStudentRepository.UpdateStudents(student);
        }

        public async Task<StudentVM> UpdateStudent(string accountId, StudentVM studentVM)
        {
            return await iStudentRepository.UpdateStudent(accountId, studentVM);
        }
        public async Task<StudentVM> GetStudentCurrent(string accountId)
        {
            return await iStudentRepository.GetStudentCurrent(accountId);
        }

        public Task<IQueryable<string>> ListNameSupsectGroup(string textFind)
        {
            return iStudentRepository.GetNameSupsectGroup(textFind);
        }

        public async Task<Student10VM> GetStudentById(string idModel)
        {
            return await iStudentRepository.GetStudentById(idModel);
        }
    }
}
