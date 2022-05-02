using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.DAL.Models
{
    public class GeneralTableModel
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int cntNumber { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
