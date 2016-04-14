using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex1_Maze
{
    public class RandomCreation<T> 
    {
        private int height;
        private int width;
        private GeneralMaze<T> maze;
        private Node<T> last;
        private Random rand;

        /// <summary>
        /// Main Method that will created a maze using Prims Algorithm </summary>
        /// <param name="createable">Maze</param>
        public void create(ICreateable<T> createable)
        {
            this.maze = createable.GetMaze();
            this.height = createable.GetHeight();
            this.width = createable.GetWidth();
            List<Node<T>> primList = new List<Node<T>>();
            rand = new Random();
            
            //Sets a random Starting Point
            int rRow = rand.Next(height);
            int rCol = rand.Next(width);
            maze.GetNode(rRow, rCol).SetValue(0);
            maze.SetStartPoints(rRow, rCol);

            //Adds Node to the Prims List
            primList.Add(maze.GetNode(rRow, rCol));
            
            //Will run as long as the List is not empty
            while (primList.Count > 0)
            {
                primList = primList.Distinct().ToList();
                this.getNeighbors(primList);
            }
            //Sets the end Point of the maze
            this.maze.SetEndPoints(last.GetRow(), last.GetCol());
        }


        /// <summary>
        /// This will get all the neighbours of a certain Node</summary>
        /// <param name="neighbors">Returns the list of neighbours</param>
        /// <returns></returns>
        public List<Node<T>> getNeighbors(List<Node<T>> neighbors)
        {
            int rundomNumber = new Random().Next(neighbors.Count);
            //choose a randon neighbor to go to
            Node<T> current = neighbors[rundomNumber];
            maze.SetCell(current.GetRow(), current.GetCol(), 0);

            //check if ther are parents to mark the point between
            if (current.GetParent() != null)
            {
                int colBetwen = (current.GetParent().GetCol() + current.GetCol()) / 2;
                int rowBetwen = (current.GetParent().GetRow() + current.GetRow()) / 2;
                if (colBetwen < 0)
                {
                    colBetwen *= -1;
                }
                if (rowBetwen < 0)
                {
                    rowBetwen *= -1;
                }
                this.maze.SetCell(rowBetwen, colBetwen, 0);
            }


            //Removes the current Node from the List
            neighbors.Remove(current);
            this.last = current;
            //Saves the indecies of the current node
            int col = current.GetCol();
            int row = current.GetRow();
           
            //Checks Up
            if (row - 2 >= 0 && this.maze.GetValue(row - 1, col) == 1 &&
                this.maze.GetValue(row - 2, col) == 1)
            {
                neighbors.Add(new Node<T>(row - 2, col, current));
            }
            //Checks Down
            if (row + 2 < this.height && this.maze.GetValue(row + 1, col) == 1 &&
                this.maze.GetValue(row + 2, col) == 1)
            {
                neighbors.Add(new Node<T>(row + 2, col, current));
            }
            //Checks Left
            if (col - 2 >= 0 && this.maze.GetValue(row, col - 1) == 1 &&
                this.maze.GetValue(row, col - 2) == 1)
            {
                neighbors.Add(new Node<T>(row, col - 1, current));
            }
            //Checks Right
            if (col + 2 < this.width && this.maze.GetValue(row, col + 1) == 1 &&
               this.maze.GetValue(row, col + 2) == 1)
            { 
                neighbors.Add(new Node<T>(row, col +2, current));
            }
            return neighbors;
        }
    }
}