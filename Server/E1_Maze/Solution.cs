using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_Maze
{
    public class Solution<T>
    {
        private List<Node<T>> list;
       
        


        public Solution(List<Node<T>> list)
        {
            this.list = list;
            
        }

        public List<Node<T>> GetList()
        {
            return this.list;
        }

        public Node<T> FindNode(Node<T> given)
        {
            foreach (Node<T> s in this.list)
            {
                if (s.Equals(given))
                {
                    return s;
                }
            }
            return null;
        }

        public void AddSolution(Node<T> newSolution)
        {
            this.list.Add(newSolution);
        }

        public int GetLength()
        {
            return this.list.Count;
        }

        public int GetCost(Node<T> givenState)
        {
            int ans = givenState.GetCost();
            foreach (Node<T> s in this.list)
                {
                    if( s.GetCost() <= ans)
                    {
                        ans = s.GetCost();
                    }
                }
            return ans;
        }
      
      
        public bool Containe(Node<T> s)
        {
            if (this.list.Contains(s))
                return true;
            else
                return false;
        }
    }
}