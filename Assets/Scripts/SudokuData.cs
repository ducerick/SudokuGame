using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SudokuEasyData : MonoBehaviour
{
    public static readonly string TextFile = @"D:\BuiMinhDucIntern\SodukuGame\SodukuGame\Assets\Graphics\SudokuData\Easy.txt";
    public static List<SudokuData.SudokuBoardData> GetData()
    {
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        string[] lines = File.ReadAllLines(TextFile);
        for(int index = 0; index < lines.Length; index += 2)
        {
            int[] unsolved = lines[index].Select(ch => ch - '0').ToArray();
            int[] solved = lines[index + 1].Select(ch => ch - '0').ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        return data;
    }
}

public class SudokuMediumData : MonoBehaviour
{
    public static readonly string TextFile = @"D:\BuiMinhDucIntern\SodukuGame\SodukuGame\Assets\Graphics\SudokuData\Medium.txt";
    public static List<SudokuData.SudokuBoardData> GetData()
    {
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        string[] lines = File.ReadAllLines(TextFile);
        for (int index = 0; index < lines.Length; index += 2)
        {
            int[] unsolved = lines[index].Select(ch => ch - '0').ToArray();
            int[] solved = lines[index + 1].Select(ch => ch - '0').ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        return data;
    }
}

public class SudokuHardData : MonoBehaviour
{
    public static readonly string TextFile = @"D:\BuiMinhDucIntern\SodukuGame\SodukuGame\Assets\Graphics\SudokuData\Hard.txt";
    public static List<SudokuData.SudokuBoardData> GetData()
    {
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        string[] lines = File.ReadAllLines(TextFile);
        for (int index = 0; index < lines.Length; index += 2)
        {
            int[] unsolved = lines[index].Select(ch => ch - '0').ToArray();
            int[] solved = lines[index + 1].Select(ch => ch - '0').ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        return data;
    }
}

public class SudokuVeryHardData : MonoBehaviour
{
    public static readonly string TextFile = @"D:\BuiMinhDucIntern\SodukuGame\SodukuGame\Assets\Graphics\SudokuData\VeryHard.txt";
    public static List<SudokuData.SudokuBoardData> GetData()
    {
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        string[] lines = File.ReadAllLines(TextFile);
        for (int index = 0; index < lines.Length; index += 2)
        {
            int[] unsolved = lines[index].Select(ch => ch - '0').ToArray();
            int[] solved = lines[index + 1].Select(ch => ch - '0').ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        return data;
    }
}
public class SudokuData : MonoBehaviour
{
    public static SudokuData Instance;  // Singleton Object

    public struct SudokuBoardData
    {
        public int[] UnsolvedData;
        public int[] SolvedData;

        public SudokuBoardData(int[] unsolved, int[] solved) 
        {
            UnsolvedData = unsolved;
            SolvedData = solved;
        }
    }

    public Dictionary<string, List<SudokuBoardData>> SudokuGame = new Dictionary<string, List<SudokuBoardData>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    void Start()
    {
        SudokuGame.Add("Easy", SudokuEasyData.GetData());
        SudokuGame.Add("Medium", SudokuMediumData.GetData());
        SudokuGame.Add("Hard", SudokuHardData.GetData());
        SudokuGame.Add("VeryHard", SudokuVeryHardData.GetData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
