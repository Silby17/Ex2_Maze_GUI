using System;

namespace Ex1_Maze
{
    public class Node<T>
    {
        private int value;
        private int i;  
        private int j;
        private int cost;
        private Node<T> parent;


        /// <summary>
        /// Construcotr that Receives row, col and Parent
        /// </summary>
        /// <param name="i">Row index</param>
        /// <param name="j">Col index</param>
        /// <param name="parent">Node Parent</param>
        public Node(int i, int j, Node<T> parent)
        {
            this.i = i;
            this.j = j;
            this.parent = parent;
            this.cost = new Random().Next(0, 20) ;
            this.value = 0;
        }


        /// <summary>
        /// Constructor that receives a Row, Col,Value and a cost
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Col index</param>
        /// <param name="value">Node Value</param>
        /// <param name="cost">Node Cost</param>
        public Node(int row, int col, int value, int cost)
        {
            this.i = row;
            this.j = col;
            this.cost = cost;
            this.value = value;
        }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Node()
        {
             this.cost = new Random().Next(0, 20);
        }


        /// <summary>
        /// Constructor method that receives a Row, Col and value
        /// and sets the cost to a random number</summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        public Node(int row, int col, int value)
        {
            this.i = row;
            this.j = col;
            this.value = value;
            this.cost = new Random().Next(0, 20);
        }


        /// <summary>
        /// Gets the value of the Current Node</summary>
        /// <returns>Node Value</returns>
        public int GetValue()
        {
            return this.value;
        }

        
        /// <summary>
        /// Returns the Cost of the current Node</summary>
        /// <returns>Node Cost</returns>
        public int GetCost()
        {
            return this.cost;
        }

        /// <summary>
        /// Returns the Row index</summary>
        /// <returns>Row index</returns>
        public int GetRow()
        { return this.i; }


        /// <summary>
        /// Returns the Col index </summary>
        /// <returns>Returns the Col index</returns>
        public int GetCol()
        { return this.j; }


        /// <summary>
        /// Returns the Parent Node</summary>
        /// <returns>Parent Node</returns>
        public Node<T> GetParent()
        { return this.parent; }


        /// <summary>
        /// Sets the Value of this node</summary>
        /// <param name="v">Value to be set</param>
        public void SetValue(int v)
        { this.value = v; }


        /// <summary>
        /// Sets the Parent Node </summary>
        /// <param name="s">The Parent node to be set</param>
        public void SetParent(Node<T> s)
        { this.parent = s; }


        /// <summary>
        /// Sets the cost of the Current Node </summary>
        /// <param name="cost">Cost to be set</param>
        public void SetCost(int cost)
        {this.cost = cost;}
    }
}