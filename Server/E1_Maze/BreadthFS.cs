using System.Collections.Generic;
using System;


namespace Ex1_Maze
{
    class BreadthFS<T> : ISearcher<T>
    {
        private Queue<Node<T>> q = new Queue<Node<T>>();
        private Solution<T> resultList = new Solution<T>(new List<Node<T>>());


        /// <summary>
        /// Constructor method that will recieve a maze 
        /// that is searchable</summary>
        /// <param name="searchable">THe searchable maze</param>
        /// <returns>The Solution</returns>
        public Solution<T> Search(ISearchable<T> searchable)
        {
            Node<T> n = this.GetPathBFS(searchable.GetMaze().GetStartPoint().GetRow(),
                searchable.GetMaze().GetStartPoint().GetCol(), searchable);

            while (null != n)
            {
                searchable.GetMaze().SetCell(n.GetRow(), n.GetCol(), 2);
                this.resultList.AddSolution(n);
                n = n.GetParent();
            }

            for (int i = 0; i < searchable.GetMaze().GetHeight(); i++)
            {
                for (int j = 0; j < searchable.GetMaze().GetWidth(); j++)
                {
                    if (searchable.GetMaze().GetValue(i, j) == 5)
                    {
                        searchable.GetMaze().SetCell(i, j, 0);
                    }
                }
            }
            return resultList;
        }


        /// <summary>
        /// This method will get the BFS Path</summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="searchable">The searchable maze</param>
        /// <returns></returns>
        public Node<T> GetPathBFS(int x, int y, ISearchable<T> searchable)
        {
            q.Enqueue(new Node<T>(x, y, null));
            //untill the queue is empty or found end node
            while (q.Count > 0)
            {
                Node<T> n = q.Dequeue();
                if (searchable.GetMaze().GetEndPoint().GetRow() == n.GetRow() &&
                    searchable.GetMaze().GetEndPoint().GetCol() == n.GetCol())
                { return n;}


                if (n.GetRow() != searchable.GetMaze().GetHeight() - 1)
                {
                    if (isFree(n.GetRow() + 1, n.GetCol(), searchable))
                    {   //mark as visited
                        searchable.GetMaze().SetCell(n.GetRow(), n.GetCol(), 5);
                        Node<T> nextNode = new Node<T>(n.GetRow() + 1, n.GetCol(), n);
                        q.Enqueue(nextNode);
                    }
                }

                if (n.GetRow() != 0)
                {
                    if (isFree(n.GetRow() - 1, n.GetCol(), searchable))
                    {
                        searchable.GetMaze().SetCell(n.GetRow(), n.GetCol(), 5);
                        Node<T> nextNode = new Node<T>(n.GetRow() - 1, n.GetCol(), n);
                        q.Enqueue(nextNode);
                    }
                }


                if (searchable.GetMaze().GetWidth() - 1 != n.GetCol())
                {
                    if (isFree(n.GetRow(), n.GetCol() + 1, searchable))
                    {
                        searchable.GetMaze().SetCell(n.GetRow(), n.GetCol(), 5);
                        Node<T> nextNode = new Node<T>(n.GetRow(), n.GetCol() + 1, n);
                        q.Enqueue(nextNode);
                    }
                }


                if (0 != n.GetCol())
                {
                    if (isFree(n.GetRow(), n.GetCol() - 1, searchable))
                    {
                        searchable.GetMaze().SetCell(n.GetRow(), n.GetCol(), 5);
                        Node<T> nextNode = new Node<T>(n.GetRow(), n.GetCol() - 1, n);
                        q.Enqueue(nextNode);
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Checks the the cell is free</summary>
        /// <param name="x">The X index</param>
        /// <param name="y">The Y index</param>
        /// <param name="searchable">The searchable maze</param>
        /// <returns></returns>
        public bool isFree(int x, int y, ISearchable<T> searchable)
        {
            if ((x >= 0 && x < searchable.GetMaze().GetWidth()) &&
                (y >= 0 && y < searchable.GetMaze().GetHeight()) &&
                (searchable.GetMaze().GetValue(x, y) == 0))
            { return true; }
            return false;
        }
    }
}