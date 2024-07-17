using BusinessObjects;
using BusinessObjects.Models.FindFormModel;
using BusinessObjects.Models.TutorModel;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FindTutorFormRepository : IFindTutorFormRepository
    {
        private readonly FindTutorFormDAO _findTutorFormDAO = null;

        public FindTutorFormRepository()
        {
            if (_findTutorFormDAO == null)
            {
                _findTutorFormDAO = new FindTutorFormDAO();     
            }
        }

        public bool AddFindTutorForm(FindTutorForm form)
        {
            return _findTutorFormDAO.AddFindTutorForm(form);
        }

        public bool DelFindTutorForms(int id)
        {
            return _findTutorFormDAO.DelFindTutorForms(id);
        }

        public List<FindTutorForm> GetFindTutorForms()
        {
            return _findTutorFormDAO.GetFindTutorForms();
        }

        public bool UpdateFindTutorForms(FindTutorForm form)
        {
            return _findTutorFormDAO.UpdateFindTutorForms(form);
        }

        public IEnumerable<FindTutorForm> Filter(RequestSearchPostModel requestSearchPostModel)
        {
            var result = _findTutorFormDAO.GetFindTutorForms().Where(s => s.IsActived == null && s.Status == true);

            // ĐK Gender
            if (requestSearchPostModel.Gender != null)
            {
                result = result.Where(s => s.TutorGender == requestSearchPostModel.Gender);
            }

            // ĐK HourlyRate
            if (requestSearchPostModel.HourlyRate != null)
            {
                result = result.Where(s => s.MaxHourlyRate >= requestSearchPostModel.HourlyRate 
                                        && s.MinHourlyRate <= requestSearchPostModel.HourlyRate);
            }

            // ĐK TypeOfDegree
            if (requestSearchPostModel.TypeOfDegree != null)
            {
                result = result.Where(s => s.TypeOfDegree == requestSearchPostModel.TypeOfDegree);
            }

            return result;
        }

        public IEnumerable<FormFindTutorVM> Sorting
            (IEnumerable<FormFindTutorVM> query,
            string? sortBy,
            string? sortType)
        {
            //_____SORT_____
            if (!string.IsNullOrEmpty(sortType))
            {
                if (sortType == SortPostTypeEnum.Ascending.ToString() && sortBy == SortPostByEnum.CreateDay.ToString())
                {
                    query = query.OrderBy(t => t.CreateDay);
                }
                else if (sortType == SortPostTypeEnum.Descending.ToString() && sortBy == SortPostByEnum.CreateDay.ToString())
                {
                    query = query.OrderByDescending(t => t.CreateDay);
                }
            }
            else
            {
                query = query.OrderBy(t => t.CreateDay);
            }

            return query;
        }

        public bool HandleSpam(RequestCreateFormFindTutor form, string subjectId)
        {
            var checkForm = _findTutorFormDAO.GetFindTutorForms()
                .Where(s => s.SubjectId == subjectId
                && s.DayStart == form.DayStart
                && s.DayEnd == form.DayEnd
                && s.DayOfWeek == form.DayOfWeek);
            if (checkForm == null)
            {
                return true;
            }

            return false;
        }
    }
}
