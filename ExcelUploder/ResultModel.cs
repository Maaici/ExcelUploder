using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploder
{
    public class ResultModel
    {
        public bool IsHaveReport { get; set; }

        public string ErrMsg { get; set; }

        public string FileName { get; set; }
    }
}
