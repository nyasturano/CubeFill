using UnityEngine;
using Newtonsoft.Json;

public class LevelData
{
    public LevelData(string json)
    {
        _data = JsonConvert.DeserializeObject<Data>(json);
        for (int i = 0; i < FieldHeight; i++)
        {
            for (int j = 0; j < FieldWidth; j++)
            {
                if (Field[i, j] == 2)
                {
                    StartPosition = new Vector2Int(j, i);
                }
            }
        }
    }

    private Data _data;
    
    public int[,] Field { get => _data.Field; private set { } }
    
    public Vector2Int StartPosition { get; private set; }
    public Vector2Int DefaultDirection { get => _data.DefaultDirection; private set { } }
    
    public int FieldHeight { get => _data.Field.GetUpperBound(0) + 1; private set { } }
    public int FieldWidth { get => _data.Field.GetUpperBound(1) + 1; private set { } }
    
    [System.Serializable]
    private class Data
    {
        public int[,] Field;

        public Vector2Int StartPosition;
        public Vector2Int DefaultDirection;
    }
}