using System;
using System.Collections.Generic;


namespace Ex1_Maze
{
    public class GeneralMaze<T> : ICreateable<T>, ISearchable<T>
    {
        private _2DMaze<T> maze;
        public string Name { get; set; }
        public string Maze { get; set; }
        public JPosition Start { get; set; }
        public JPosition End { get; set; }
        private bool SOLVED = false;

        /// <summary>
        /// This is the constructor Method
        /// </summary>
        /// <param name="maze">Gets a new 2D maze</param>
        public GeneralMaze(_2DMaze<T> maze)
        {
            this.maze = maze;
        }


        /// <summary>
        /// This Will update the memebrs of the class
        /// for the JSON serilization </summary>
        public void UpdateMembers()
        {
            this.Maze = this.maze.Maze;
            this.Start = this.maze.Start;
            this.End = this.maze.End;
            this.Name = this.maze.Name;
        }


        /// <summary>
        /// This function will generate the needed Maze
        /// according to the algorithm defined as type</summary>
        /// <param name="name">Name of the maze</param>
        /// <param name="type">Algo used to create the maze</param>
        public void Generate(string name, int type)
        {
            this.maze.Generate(name, type);

            if (type == 1) //Create using DFS
            {
                CreateDFS<T> DFSMaze = new CreateDFS<T>();
                DFSMaze.create(this);
                MakeMazeString();
                UpdateMembers();
            }
            else if (0 == type) //Create using Random Prims Algorithm
            {
                RandomCreation<T> randomMaze = new RandomCreation<T>();
                randomMaze.create(this);
                MakeMazeString();
                UpdateMembers();
            }
        }


        /// <summary>
        /// This method will solve the maze using the algorithm defined 
        /// by the user.
        /// 1 = BestFirst Search
        /// 0 = BFS
        /// </summary>
        /// <param name="name">Name of the maze to be Solved</param>
        /// <param name="type">Algorithm type</param>
        public void Solve(int type)
        {
            if (1 == type) //BestFirst Search
            {
                BestFS<T> B = new BestFS<T>();
                B.Search(this);
                MakeMazeString();
                UpdateMembers();
                SOLVED = true;
            }
            else if (0 == type) //BFS
            {
                BreadthFS<T> B = new BreadthFS<T>();
                B.Search(this);
                MakeMazeString();
                UpdateMembers();
                SOLVED = true;
            }
        }


        /// <summary>
        /// Returns the vale of the node at given Index</summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        /// <returns>Value of the node</returns>
        public int GetValue(int row, int col)
        {
            return this.maze.GetValue(row, col);
        }
  

        /// <summary>
        /// Returns the height of the Maze </summary>
        /// <returns>Maze height</returns>
        public int GetHeight()
        {
            return this.maze.GetHeight();
        }


        /// <summary>
        /// Returns the height of the Maze
        /// </summary>
        /// <returns>The Maze height</returns>
        public int GetWidth()
        {
            return this.maze.GetWidth();
        }


        /// <summary>
        /// Returns the GeneralMaze itself</summary>
        /// <returns>itself</returns>
        public GeneralMaze<T> GetMaze()
        { return this; }


        /// <summary>
        /// Returns a node at the given index </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        /// <returns>The needed node</returns>
        public Node<T> GetNode(int row, int col)
        { return maze.GetNode(row, col); }

        
        /// <summary>
        /// Returns the grid which is the 2D Array</summary>
        /// <returns>The actual grid</returns>
        public Node<T>[,] GetGrid()
        {
            return this.maze.GetMaze();
        }


        /// <summary>
        /// Returns the ending Node </summary>
        /// <returns>Node</returns>
        public Node<T> GetEndPoint()
        { return (this.maze.getEnd());}


        /// <summary>
        /// Sets the starting Point of the Maze </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        public void SetStartPoints(int row, int col)
        { maze.SetStartingCell(row, col); }


        /// <summary>
        /// This sets the ending Point of the maze </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        public void SetEndPoints(int row, int col)
        { maze.SetEndingCell(row, col); }
      

        /// <summary>
        /// returns a list of all possibles moves
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<Node<T>> getAllPossibleStates(Node<T> n)
        {
            List<Node<T>> possibleStates = new List<Node<T>>();
            int i = n.GetRow();
            int j = n.GetCol();
            // cheack up
            if ((j != 0) &&(this.maze.GetValue(i, j - 1) != 1))
            {
                possibleStates.Add(this.maze.GetNode(i, j - 1));
            }
            //down

            if ((j != GetHeight() - 1) && (this.maze.GetValue(i, j + 1) != 1))
            {
                possibleStates.Add(this.maze.GetNode(i, j + 1));
            }
            //left
            if ((i != 0) && (this.maze.GetValue(i - 1, j) != 1))
            {
                possibleStates.Add(this.maze.GetNode(i - 1, j));
            }
            //right
            if ((i != GetWidth() - 1) && (this.maze.GetValue(i + 1, j) != 1))
            {
                possibleStates.Add(this.maze.GetNode(i + 1, j));
            }

            return possibleStates;
        }


        /// <summary>
        /// Gets the Starting Node </summary>
        /// <returns>Starting Node</returns>
        public Node<T> GetStartPoint()
        {
            return this.maze.getStart();
        }


        /// <summary>
        /// Sets the value of the Given Cell</summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        /// <param name="v">Value to be set</param>
        public void SetCell(int row, int col, int v)
        {
            this.maze.SetCell(row, col, v);
        }


        /// <summary>
        /// This will print out on the screen the maze</summary>
        public void Print()
        { this.maze.Print();}


        /// <summary>
        /// Reutrns the maze String</summary>
        /// <returns>Maze in format of string</returns>
        public string GetMazeString()
        { return this.maze.Maze; }


        /// <summary>
        /// Converts the maze into string form </summary>
        public void MakeMazeString()
        { this.maze.MakeMazeString(); }


        /// <summary>
        /// Checks if the Maze has been solved </summary>
        /// <returns>False if not Solved and true if solved </returns>
        public bool IsSolved()
        { return this.SOLVED; }
    }
}