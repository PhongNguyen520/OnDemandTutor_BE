using BusinessObjects;
using BusinessObjects.Models.FormModel;
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
        private readonly FindTutorFormDAO findTutorFormDAO = null;

        public FindTutorFormRepository()
        {
            if (findTutorFormDAO == null)
            {
                findTutorFormDAO = new FindTutorFormDAO();
            }
        }

        public bool AddFindTutorForm(FindTutorForm form)
        {
            return findTutorFormDAO.AddFindTutorForm(form);
        }

        public bool DelFindTutorForms(int id)
        {
            return findTutorFormDAO.DelFindTutorForms(id);
        }

        public List<FindTutorForm> GetFindTutorForms()
        {
            return findTutorFormDAO.GetFindTutorForms();
        }

        public bool UpdateFindTutorForms(FindTutorForm form)
        {
            return findTutorFormDAO.UpdateFindTutorForms(form);
        }

        public IEnumerable<FindTutorForm> Filter(RequestSearchPostModel requestSearchPostModel)
        {
            var result = findTutorFormDAO.GetFindTutorForms().Where(s => s.IsActived == true && s.Status == false);

            // ĐK Gender
            if (requestSearchPostModel.Gender != null)
            {
                result = result.Where(s => s.TutorGender == requestSearchPostModel.Gender);
            }

            // ĐK HourlyRate
            if (requestSearchPostModel.MaxRate != null)
            {
                result = result.Where(s => s.MaxHourlyRate <= requestSearchPostModel.MaxRate);
            }
            if (requestSearchPostModel.MinRate != null)
            {
                result = result.Where(s => s.MinHourlyRate >= requestSearchPostModel.MinRate);
            }

            // ĐK TypeOfDegree
            if (requestSearchPostModel.TypeOfDegree != null)
            {
                result = result.Where(s => s.TypeOfDegree == requestSearchPostModel.TypeOfDegree);
            }

            return result;
        }

        public IEnumerable<FormVM> Sorting
            (IEnumerable<FormVM> query,
            string? sortBy,
            string? sortType,
            int pageIndex)
        {
            //_____SORT_____
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortPostTypeEnum.Ascending.ToString() && sortBy == SortPostByEnum.HourlyRate.ToString())
                {
                    query = query.OrderBy(t => t.MaxHourlyRate);
                }
                else if (sortType == SortPostTypeEnum.Descending.ToString() && sortBy == SortPostByEnum.HourlyRate.ToString())
                {
                    query = query.OrderByDescending(t => t.MaxHourlyRate);
                }
                else if (sortType == SortPostTypeEnum.Ascending.ToString() && sortBy == SortPostByEnum.CreateDay.ToString())
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

            //_____PAGING_____
            int validPageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
            int validPageSize = 10;

            if (query.Count() < 10)
            {
                validPageSize = query.Count();
            }

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);

            return query;
        }
    }
}
