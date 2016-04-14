using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_Maze
{
    public class MainRunner
    {

        public void startProgram()
        {
            _2DMaze<int> maze = new _2DMaze<int>(5, 5);            
            GeneralMaze<int> Cm = new GeneralMaze<int>(maze);
            Cm.Generate("mazushshsh", 1);
            Cm.Solve(0);
            maze.Print();
        }
    }
}
