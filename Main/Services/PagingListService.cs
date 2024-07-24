using BusinessObjects.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Services
{
    public interface IPagingListService<T>
    {
        public PagingResult<T> Paging(List<T> list, int pageIndex, int validPageSize);
    }
    public class PagingListService<T> : IPagingListService<T>
    {
        public PagingResult<T> Paging(List<T> list, int pageIndex, int validPageSize)
        {
            int validPageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
            int totalItems = list.Count();
            int limitPage = 0;

            if (list.Count() < validPageSize)
            {
                validPageSize = list.Count();
            }

            if (totalItems != 0)
            {
                limitPage = (int)Math.Ceiling((double)totalItems / validPageSize);
            }

            list = list.Skip(validPageIndex * validPageSize).Take(validPageSize).ToList();

            var result = new PagingResult<T>()
            {
                ListResult = list,
                LimitPage = limitPage,
            };

            return result;
        }
    }
}
