using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SudokuGrid : MonoBehaviour
{
    public int Columns = 0;
    public int Rows = 0;
    public float SquareOffset = 0.0f; // khoang cach giua cac object grid square
    public GameObject GridSquare;
    public Vector2 StartPosition = new Vector2(0.0f, 0.0f);
    public float SquareScale = 1.0f;
    public int _slectedData = -1;
    public Color HighlightColor = Color.red;

    [SerializeField] float SquareSpan;

    public readonly List<GameObject> GridSquares = new List<GameObject>();
    protected virtual void Awake()
    {
        if (GridSquare.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("This Game Object need to have GridSquare script attached!");
        }
        CreateGrid();
    }


    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        int squareIndex = 0;
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                GridSquares.Add(Instantiate(GridSquare) as GameObject);
                GridSquares[GridSquares.Count - 1].GetComponent<GridSquare>().SetNumberIndex(squareIndex);
                GridSquares[GridSquares.Count - 1].transform.parent = this.transform; // instantiate this game object as a child of the object holding this scipts
                GridSquares[GridSquares.Count - 1].transform.localScale = new Vector3(SquareScale, SquareScale, SquareScale);
                squareIndex++;
            }
        }
    }

    private void SetSquaresPosition()
    {
        var squareRect = GridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 squareSpanCount = new Vector2(0.0f, 0.0f);
        bool rowNext = false;

        offset.x = squareRect.rect.width * squareRect.transform.localScale.x + SquareOffset;
        offset.y = squareRect.rect.height * squareRect.transform.localScale.y + SquareOffset;

        int columnNumber = 0;
        int rowNumber = 0;
        foreach (GameObject square in GridSquares)
        {
            if (columnNumber + 1 > Columns)
            {
                rowNumber++;
                columnNumber = 0;
                squareSpanCount.x = 0;
                rowNext = true;
            }

            var posXOffset = offset.x * columnNumber + squareSpanCount.x * SquareSpan;
            var posYoffet = offset.y * rowNumber + squareSpanCount.y * SquareSpan;

            if (columnNumber > 0 && columnNumber % 3 == 0)
            {
                squareSpanCount.x++;
                posXOffset += SquareSpan;
            }

            if (rowNumber > 0 && rowNumber % 3 == 0 && rowNext == true)
            {
                rowNext = false;
                squareSpanCount.y++;
                posYoffet += SquareSpan;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(StartPosition.x + posXOffset, StartPosition.y - posYoffet);
            columnNumber++;
        }
    }

    public abstract void SetGridNumber(string level);
    public abstract void OnSquareSelected(int squareIndex);
    public abstract void CheckBoardCompleted(int number);
    public abstract void TryToStartThisGame();


    public void SetGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int index = 0; index < GridSquares.Count; index++)
        {
            if (data.UnsolvedData[index] > 0)
            {
                GridSquares[index].GetComponent<GridSquare>().SetEnableChange(false);
            }
            GridSquares[index].GetComponent<GridSquare>().SetNumber(data.UnsolvedData[index]);
            GridSquares[index].GetComponent<GridSquare>().SetCorrectNumber(data.SolvedData[index]);
        }
    }


    public void SetColorSquare(int[] data, Color col)
    {
        foreach (var dt in data)
        {
            var tmp = GridSquares[dt].GetComponent<GridSquare>();
            if (tmp.IsSelected() == false || data.Length == 81)
            {
                if (tmp.HasWrongNumber == false)
                    tmp.SetColorSquare(col);
            }
        }
    }

    public void SolveSudoku()
    {
        foreach(var square in GridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNumber();
        }

        CheckBoardCompleted(0);
    }


}
