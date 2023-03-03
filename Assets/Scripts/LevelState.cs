using System.Collections.Generic;
using UnityEngine;

public class LevelState
{
    private int _height;
    private int _width;
    
    private Dictionary<Vector2Int, Block> _blocks;
    private bool[,] _field;

    private Vector2Int _startPosition;
    private Vector2Int _exitPosition;
    private int _sideOffset;

    public LevelState(LevelData data, int sideOffset)
    {
        _width = data.FieldWidth;
        _height = data.FieldHeight;
        _startPosition = data.StartPosition;
        _sideOffset = sideOffset;
        _field = new bool[_height, _width];
        _blocks = new Dictionary<Vector2Int, Block>();

        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _field[i, j] = data.Field[i, j] != 0;
            }
        }
    }

    public void OpenEntry()
    {
        for (int i = 0; i <= _startPosition.x; i++)
        {
            if (_blocks.ContainsKey(new Vector2Int(i, _startPosition.y)))
            {
                _blocks[new Vector2Int(i, _startPosition.y)].MoveDown();
                _field[_startPosition.y, i] = true;
            }
        }
    }

    public void CloseEntry()
    {
        for (int i = 0; i <= _startPosition.x; i++)
        {
            if (_blocks.ContainsKey(new Vector2Int(i, _startPosition.y)))
            {
                _blocks[new Vector2Int(i, _startPosition.y)].MoveUp();
                _field[_startPosition.y, i] = false;
            }
        }
    }

    public void OpenExit(Vector2Int exitPosition)
    {
        _exitPosition = exitPosition;

        for (int i = exitPosition.x; i < _height; i++)
        {
            if (_blocks.ContainsKey(new Vector2Int(i, exitPosition.y)))
            {
                _blocks[new Vector2Int(i, exitPosition.y)].MoveDown();
                _field[exitPosition.y, i] = true;
            }
        }
    }

    public bool IsValidForMove(int i, int j)
    {
        if (j < -_sideOffset || j > _width + _sideOffset)
        {
            return false;
        }

        return (j < 0 && i == _startPosition.y)
            || (j >= _width && i == _exitPosition.y)
            || _field[i, j];
    }

    public int CalculatePath(Vector2Int coordinates, Vector2Int direction)
    {
        int length = 0;

        int i = coordinates.y + direction.y;
        int j = coordinates.x + direction.x;

        while (IsValidForMove(i, j))
        {
            length++;
            i += direction.y;
            j += direction.x;
        }
        return length;
    }

    public void AddBlock(Vector2Int coordinates, Block block)
    {
        _blocks.Add(coordinates, block);
    }  
}
