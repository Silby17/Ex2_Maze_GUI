using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_Maze
{
        public interface ISearcher<T>
        {
            // the search method
            Solution<T> Search(ISearchable<T> searchable);
            // get how many nodes were evaluated by the algorithm
           // int getNumberOfNodesEvaluated();
            //adds to the open list all the states that are in the list
           //  void addToOpenList(List<Node<T>> list);
            //pops the best state
           // Node<T> popOpenList();

    } 
}
