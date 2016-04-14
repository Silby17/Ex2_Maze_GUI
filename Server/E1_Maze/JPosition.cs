using Newtonsoft.Json;

namespace Ex1_Maze
{
    /// <summary>
    /// This class will Store the positions of the Starting and
    /// ending points in the maze for JSON formatting</summary>
    public class JPosition
    {   
            [JsonProperty]
            public int Row { get; set; }
            [JsonProperty]
            public int Col { get; set; }

        public JPosition(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
        public JPosition() { }
    }
}
