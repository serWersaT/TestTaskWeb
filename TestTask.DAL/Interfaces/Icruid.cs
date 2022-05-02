using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.DAL.Interfaces
{
    public interface Icruid<M>
    {
        void Dispose();
        int Count();
        bool Update(M model);
        bool Insert(List<M> model);
        List<M> Select(int? topmin, int? topmax);
        List<M> SelectWhere(string where);
    }
}
