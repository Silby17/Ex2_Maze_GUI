using System.Collections.Generic;


namespace Ex1_Maze
{
    public interface ISearchable<T>
    {
        List<Node<T>> getAllPossibleStates(Node<T> n);
        GeneralMaze<T> GetMaze();
        void MakeMazeString();     
    }
}