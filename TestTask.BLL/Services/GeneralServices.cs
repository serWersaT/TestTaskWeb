using System;
using System.Collections.Generic;
using System.Text;
using TestTask.DAL.Models;


namespace TestTask.BLL.Services
{
    public class GeneralServices: DefaultService
    {
        public int GeneralCnt()
        {
            return FuncCount<GeneralTableModel>(gt);
        }

        public bool InsertGeneral(List<GeneralTableModel> model)
        {
            return FuncInsert<GeneralTableModel>(gt, model);

        }

        public bool UpdateGeneral(GeneralTableModel model)
        {
            FuncUpdate<GeneralTableModel>(gt, model);
            return true;
        }

        public List<GeneralTableModel> SelectGeneral(int? topmin, int? topmax)
        {
            return FuncSelect<GeneralTableModel>(gt, topmin, topmax);
        }
    }
}
