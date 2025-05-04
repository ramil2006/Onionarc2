using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Responses
{
    public class PaginationResponse<T>
    {
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public PaginationResponse(List<T> datas, int currentPage, int totalPages)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }
    }
}
