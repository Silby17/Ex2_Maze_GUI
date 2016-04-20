using System;
using System.Collections.Generic;

namespace Ex1_Maze
{
    public class _2DMaze<T>
    {
        public string Name { get; set; }
        public string Maze { get; set; }
        public JPosition Start { get; set; }
        public JPosition End { get; set; }


        public int height { get; set; }
        public int width { get; set; }
        private Node<T>[,] grid2D;
        private Node<T> start;
        private Node<T> end;
        private int type { get; set; }

        /// <summary>
        /// Constructor for the Maze that recevies its sizes</summary>
        /// <param Name="height">Height of the Maze</param>
        /// <param Name="width">Width of the maze</param>
        public _2DMaze(int height, int width)
        {
            this.height = height;
            this.width = width;
            Start = new JPosition();
            End = new JPosition();
            this.grid2D = new Node<T>[height, width];
        }

        public _2DMaze() {
            Start = new JPosition();
            End = new JPosition();
        }

        public _2DMaze(_2DMaze<T> maze)
        {
            this.height = maze.GetHeight();
            this.width = maze.GetWidth();
            Start = new JPosition(maze.End.Row, maze.End.Col);
            End = new JPosition(maze.Start.Row, maze.Start.Col);
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.grid2D[i, j] = new Node<T>(i, j, maze.GetValue(i, j));
                }
            }
        }


        /// <summary>
        /// This method will create a copy of the grid
        /// passed as a parameter</summary>
        /// <param name="grid">The grid that needs to be copied</param>
        public void CopyGrid(Node<int>[,] grid)
        {
            this.grid2D = new Node<T>[width, height];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.grid2D[i, j] = new Node<T>(i, j, grid[i,j].GetValue());
                }
            }
        }


        /// <summary>
        /// Generates the maze with the given Parameters</summary>
        /// <param Name="name">Name of the Maze</param>
        /// <param Name="type">The Type of Maze</param>
        public void Generate(string name, int type)
        {
            this.Name = name;
            this.type = type;
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.grid2D[i, j] = new Node<T>(i, j, 1);
                }
            }
        }


        public void SetGrid(Node<T>[,] grid)
        {
            this.grid2D = grid;
        }
       
        /// <summary>
        /// Returns the Starting Node </summary>
        /// <returns>Starting Node</returns>
        public Node<T> getStart()
        {
            return this.start;
        }

       
        /// <summary>
        /// Returns the ending Node</summary>
        /// <returns></returns>
        public Node<T> getEnd()
        {
            return this.end;
        }


        /// <summary>
        /// This will set the Value of a given cell </summary>
        /// <param name="i">The Row Index</param>
        /// <param name="j">The Col Index</param>
        /// <param name="value">Value to be Set</param>
        public void SetCell(int i, int j, int value)
        {
            this.grid2D[i, j].SetValue(value);
        }


        /// <summary>
        /// This will set the starting cell </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        public void SetStartingCell(int row, int col)
        {
            this.Start.Row = row;
            this.Start.Col = col;
            this.start = this.GetNode(row, col);
        }


        /// <summary>
        /// Sets the ending Node </summary>
        /// <param name="row">The Row index</param>
        /// <param name="col">Col Index</param>
        public void SetEndingCell(int row, int col)
        {
            this.End.Row = row;
            this.End.Col = col;
            this.end = this.GetNode(row, col);
        }


        /// <summary>
        /// Returns node at the given index</summary>
        /// <param name="i">Row index</param>
        /// <param name="j">Col index</param>
        /// <returns></returns>
        public Node<T> GetNode(int i, int j)
        {
            return (this.grid2D[i, j]);
        }


        /// <summary>
        /// Sets the node at a given index to a given Node
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        /// <param name="newNode">Node to be set</param>
        public void SetNode(int row, int col , Node<T> newNode)
        {
            this.grid2D[row, col] = newNode;
        }


        /// <summary>
        /// This function is called to Print the maze</summary>
        public void Print()
        {
            Console.WriteLine(this.Name);
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    Console.Write(grid2D[i, j].GetValue().ToString());
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Converts the Maze to a string</summary>
        public void MakeMazeString()
        {
            string mazeString = "";
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    mazeString = mazeString + grid2D[i, j].GetValue().ToString();
                }
            }
            this.Maze = mazeString;
        }



        /// <summary>
        /// Gets the value of a node at the given index</summary>
        /// <param name="i">Row index</param>
        /// <param name="j">Col index</param>
        /// <returns>Node Value</returns>
        public int GetValue(int i, int j)
        {
            return (this.grid2D[i, j].GetValue());
        }


        /// <summary>
        /// Returns the 2D Grid of nodes
        /// </summary>
        /// <returns>The Maze of Nodes</returns>
        public Node<T>[,] GetMaze()
        {
            return this.grid2D;
        }


        /// <summary>
        /// Returns the height of the maze</summary>
        /// <returns></returns>
        public int GetHeight()
        {
            return this.height;
        }


        /// <summary>
        /// Returns the width of the maze </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            return this.width;
        }


        /// <summary>
        /// Gets the Row index of a node of the given Value </summary>
        /// <param name="val">Value of the node</param>
        /// <returns>Row index</returns>
        public int GetIndexRow(int val)
        {
            int ans = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (this.grid2D[i, j].GetValue() == val)
                    {
                        ans = i;
                    }
                }
            }
            return ans;
        }


        /// <summary>
        /// Gets the Col index of a node of the given value </summary>
        /// <param name="val">The value of the node</param>
        /// <returns>The Col index of the node</returns>
        public int GetIndexCol(int val)
        {
            int ans = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (this.grid2D[i, j].GetValue() == val)
                    {
                        ans = j;
                    }
                }
            }
            return ans;
        }
    }
}