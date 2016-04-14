using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_Maze
{
    public interface ICreater<T>
    {
        void create(ICreateable<T> createable);
    }
}
