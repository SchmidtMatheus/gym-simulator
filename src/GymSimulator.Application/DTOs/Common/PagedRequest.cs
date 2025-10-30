using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSimulator.Application.DTOs.Common
{
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
    }
}
