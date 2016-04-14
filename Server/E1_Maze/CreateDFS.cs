using System;
using System.Collections.Generic;


namespace Ex1_Maze
{
    public class CreateDFS<T> : ICreater<T>
    {
        private int HEIGHT;
        private int WIDTH;
        private GeneralMaze<T> maze;
        private int endRow;
        private int endCol;
        

        /// <summary>
        /// This method will create the maze using the DFS
        /// algorithm
        /// </summary>
        /// <param name="createable">A creatable maze</param>
        public void create(ICreateable<T> createable)
        {
            this.maze = createable.GetMaze();
            this.HEIGHT = createable.GetHeight();
            this.WIDTH = createable.GetWidth();
            RandomStart();
        }


        /// <summary>
        /// THis method will sent a random starting point
        /// in the maze and set it to 0
        /// </summary>
        public void RandomStart()
        {
            Random rand = new Random();
            int row = rand.Next(0, HEIGHT);
            int col = rand.Next(0, WIDTH);
            maze.SetCell(row, col, 0);
            maze.SetStartPoints(row, col);

            //This will start createing the maze using recursion
            Recursion(row, col);

            //This will set the end points of the maze
            maze.SetEndPoints(endRow, endCol);
        }


        /// <summary>
        /// This will run through the matrix using recursion 
        /// and start knocking down walls randomly
        /// </summary>
        /// <param name="row">row to start from</param>
        /// <param name="col">col to start from</param>
        public void Recursion(int row, int col)
        {
            //Creates a list of ints between 1 and 4 for directions
            List<int> randDirections = GenerateRandomDirection();

            //Runs throught the list and selects at random a direction to go to
            for(int i = 0; i < randDirections.Count; i++)
            {
                switch(randDirections[i])
                {
                    case 0: //Direction up
                        if (row - 2 <= 0)
                            continue;
                        if(maze.GetValue(row -2, col) != 0)
                        {
                            maze.SetCell(row - 2, col, 0);
                            maze.SetCell(row - 1, col, 0);
                            endRow = row - 2;
                            endCol = col;
                            Recursion(row - 2, col);
                        }
                        break;

                    case 1: //Direction Right
                        if (col + 2 >= WIDTH - 1)
                            continue;
                        if(maze.GetValue(row, col + 2) != 0)
                        {
                            maze.SetCell(row, col + 2, 0);
                            maze.SetCell(row, col + 1, 0);
                            endRow = row;
                            endCol = col + 2;
                            Recursion(row, col + 2);
                        }
                        break;


                    case 2: //Direction Down
                        if (row + 2 >= HEIGHT - 1)
                            continue;
                        if(maze.GetValue(row + 2, col) != 0)
                        {
                            maze.SetCell(row + 2, col, 0);
                            maze.SetCell(row + 1, col, 0);
                            endRow = row + 2;
                            endCol = col;
                            Recursion(row + 2, 0);
                        }
                        break;

                    case 3: //Direction Left
                        if (col - 2 <= 0)
                            continue;
                        if(maze.GetValue(row, col -2) != 0)
                        {
                            maze.SetCell(row, col - 2, 0);
                            maze.SetCell(row, col - 1, 0);
                            endRow = row;
                            endCol = col - 1;
                            Recursion(row, col - 2);
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// This method will generate a int List of random numbers
        /// between 1 and 4.
        /// </summary>
        /// <returns>Returns a shuffled list of numbers</returns>
        public List<int> GenerateRandomDirection()
        {
            List<int> randoms = new List<int>();
            for (int i = 0; i < 4; i++)
                randoms.Add(i);
            List<int> shuffeled = ShuffleList(randoms);
            return shuffeled;
        }


        /// <summary>
        /// This method will randomly shuffle a list of numbers
        /// from 0-4 which will be our directions
        /// </summary>
        /// <param name="list">List of numbers to shuffle</param>
        /// <returns>New Shuffled List</returns>
        public List<int> ShuffleList(List<int> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            List<int> shuffled = list;
            return shuffled;
        }
    }
}