
namespace Ex1_Maze
{
    interface IMaze<T> : ISearchable<T>, ICreateable<T>
    {
        Node<T> GetStartPoint();
        Node<T> GetEndPoint();
    }
}  