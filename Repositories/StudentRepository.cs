using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDAO studentDAO = null;
        private readonly DAOs.DbContext _dbContext;
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;

        public StudentRepository()
        {
            if (studentDAO == null)
            {
                studentDAO = new StudentDAO();
            }
        }

        public StudentRepository(UserManager<Account> userManager, IMapper mapper, DAOs.DbContext dbContext)
        {
            if (studentDAO == null)
            {
                studentDAO = new StudentDAO();
            }
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public bool AddStudent(Student student)
        {
            return studentDAO.AddStudent(student);
        }

        public bool DelStudents(int id)
        {
            return studentDAO.DelStudents(id);
        }

        public List<Student> GetStudents()
        {
            return studentDAO.GetStudents();
        }

        public bool UpdateStudents(Student student)
        {
            return studentDAO.UpdateStudents(student);
        }

        public async Task<StudentVM> UpdateStudent(string accountId, StudentVM studentVM)
        {
            var studentDb = await _dbContext.Students
                                            .Include(t => t.Account)
                                            .FirstOrDefaultAsync(t => t.AccountId == accountId);
            var accountDb = await _userManager.FindByIdAsync(accountId);

            if(studentDb == null)
            {
                return null;
            }
            else
            {
                _mapper.Map(studentVM, studentDb);
                studentDAO.UpdateStudents(studentDb);

                _mapper.Map(studentVM, accountDb);
                _dbContext.Update(accountDb);
                _dbContext.SaveChanges();
                return studentVM;
            }
                                            
        }

        public async Task<StudentVM> GetStudentCurrent(string accountId)
        {
            var studentVM = new StudentVM();
            var studentDb = await _dbContext.Students
                                      .Include(t => t.Account)
                                      .FirstOrDefaultAsync(t => t.AccountId == accountId);
            var accountDb = await _userManager.FindByIdAsync(accountId);
            if (studentDb == null)
            {
                return null;
            }
            else
            {
                studentVM = _mapper.Map<StudentVM>(studentDb);
                _mapper.Map(accountDb, studentVM);
                return studentVM;
            }
        }

        public async Task<IQueryable<string>> GetNameSupsectGroup(string textFind)
        {
            textFind = textFind.ToUpper();
            
            var listSupj = _dbContext
                            .SubjectGroups
                            .Where(_ => _.SubjectName.ToUpper().Contains(textFind))
                            .Select(_ => _.SubjectName)
                            .ToList().AsQueryable();
            return listSupj;
        }
    }
}
