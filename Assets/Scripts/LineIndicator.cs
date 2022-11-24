using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static LineIndicator Instance;

    private int[, ] _lineData = new int[9, 9];
    private int[,] _squareData = new int[9, 9];

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void InitValue()
    {
        var mark = -1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _lineData[i, j] = i * 9 + j;
               
                if (i % 3 == 0 && j == 0)
                {
                    _squareData[i, j] = i * 9;
                    mark = i;
                }
                else if (j % 9 < 3)
                {
                    _squareData[i, j] = _squareData[mark, 0] + i % 3 * 3 + j ;
                } 
                else 
                {
                    _squareData[i, j] = _squareData[i, j - 3] + 9;
                }
            }
        }
    }

    public (int, int) GetSquareIndex(int square_index)
    {
        var pos_row = -1;
        var pos_col = -1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j ++)
            {
                if (_lineData[i, j] == square_index)
                {
                    pos_row = i;
                    pos_col = j;
                }
            }
        }
        return (pos_row, pos_col);
    }

    public int[] GetHorizontalLine(int square_index)
    {
        var line = new int[9];
        var index = GetSquareIndex(square_index);
        var row = index.Item1;
        for (int j = 0; j < 9; j++)
        {
            line[j] = _lineData[row, j];
        }
        return line;
    }

    public int[] GetVerticalLine(int square_index)
    {
        var line = new int[9];
        var index = GetSquareIndex(square_index);
        var col = index.Item2;
        for (int i = 0; i < 9; i++)
        {
            line[i] = _lineData[i, col];
        }
        return line;
    }

    public int[] GetSquaresLine(int square_index)
    {
        var line = new int[9];
        var row = -1;
        for (int i = 0; i < 9; i ++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (_squareData[i, j] == square_index)
                {
                    row = i;
                }
            }
        }
        for (int i = 0; i < 9; i++)
        {
            line[i] = _squareData[row, i];
        }
        return line;
    }

    public int[] GetAllline()
    {
        var line = new int[81];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                line[i * 9 + j] = i * 9 + j;
            }
        }
        return line;
    }
}
