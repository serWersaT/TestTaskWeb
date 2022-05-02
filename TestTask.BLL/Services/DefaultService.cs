using System;
using System.Collections.Generic;
using System.Text;
using TestTask.DAL.DB;
using TestTask.DAL.Interfaces;

namespace TestTask.BLL.Services
{
    public class DefaultService
    {
        public GeneralTable gt = new GeneralTable();

        public int FuncCount<M>(Icruid<M> icrud)
        {
            return icrud.Count();
        }

        public bool FuncInsert<M>(Icruid<M> icrud, List<M> model)
        {
            return icrud.Insert(model);
        }

        public void FuncUpdate<M>(Icruid<M> icrud, M model)
        {
            icrud.Update(model);
        }

        public List<M> FuncSelect<M>(Icruid<M> icrud, int? topmin, int? topmax)
        {
            return icrud.Select(topmin, topmax);
        }

        public List<M> FuncSelectWhere<M>(Icruid<M> icrud, string where)
        {
            return icrud.SelectWhere(where);
        }
    }
}
