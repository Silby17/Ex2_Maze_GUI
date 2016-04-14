
namespace Ex1_Maze
{
    public interface ICreateable<T>
    {
        GeneralMaze<T> GetMaze();
        void MakeMazeString();
        int GetHeight();
        int GetWidth();
    }
}