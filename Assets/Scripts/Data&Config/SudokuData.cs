using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public struct SudokuEasyData
{
    public static List<SudokuData.SudokuBoardData> GetData(string dir)
    {
        //string TextFile = dir + "/Assets/Resources/Easy.txt";
        //if (!File.Exists(TextFile))
        //{
        //    using (var fs = File.Create(TextFile)) { }
        //}
        
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        var rawData = Resources.Load<TextAsset>("Easy");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");
        for (int index = 0; index < lines.Length -1; index += 2)
        {
            int[] unsolved = lines[index].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            int[] solved = lines[index + 1].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        Debug.Log(data);
        return data;
    }
}

public struct SudokuMediumData 
{
    public static List<SudokuData.SudokuBoardData> GetData(string dir)
    {

        //string TextFile = dir + "/Assets/Resources/Medium.txt";
        //if (!File.Exists(TextFile))
        //{
        //    using (var fs = File.Create(TextFile)) { }
        //}
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        var rawData = Resources.Load<TextAsset>("Medium");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");
        for (int index = 0; index < lines.Length - 1; index += 2)
        {
            int[] unsolved = lines[index].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            int[] solved = lines[index + 1].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        Debug.Log(data);
        return data;
    }
}

public struct SudokuHardData 
{
    public static List<SudokuData.SudokuBoardData> GetData(string dir)
    {

        //string TextFile = dir + "/Assets/Resources/Hard.txt";
        //if (!File.Exists(TextFile))
        //{
        //    using (var fs = File.Create(TextFile)) { }
        //}
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        var rawData = Resources.Load<TextAsset>("Hard");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");
        for (int index = 0; index < lines.Length - 1; index += 2)
        {
            int[] unsolved = lines[index].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            int[] solved = lines[index + 1].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        Debug.Log(data);
        return data;
    }
}

public struct SudokuVeryHardData
{
    
    public static List<SudokuData.SudokuBoardData> GetData(string dir)
    {

        //string TextFile = dir + "/Assets/Resources/VeryHard.txt";
        //if (!File.Exists(TextFile))
        //{
        //    using (var fs = File.Create(TextFile)) { }
        //}
        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();
        var rawData = Resources.Load<TextAsset>("VeryHard");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");
        for (int index = 0; index < lines.Length - 1; index += 2)
        {
            int[] unsolved = lines[index].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            int[] solved = lines[index + 1].Select(x => Convert.ToInt32(x.ToString())).ToArray();
            data.Add(new SudokuData.SudokuBoardData(unsolved, solved));
        }
        Debug.Log(data);
        return data;
    }
}
public class SudokuData : MonoBehaviour
{
    public static SudokuData Instance;  // Singleton Object
    private static string dir;

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
        
        #if UNITY_ANDROID && !UNITY_EDITOR
            dir = Application.persistentDataPath ;
        #endif

        #if UNITY_EDITOR
            dir = Directory.GetCurrentDirectory();
#endif

        if (Instance == null) Instance = this;
        else Destroy(this);
    }


void Start()
    {
        Debug.Log(dir);
        SudokuGame.Add("Easy", SudokuEasyData.GetData(dir));
        SudokuGame.Add("Medium", SudokuMediumData.GetData(dir));
        SudokuGame.Add("Hard", SudokuHardData.GetData(dir));
        SudokuGame.Add("VeryHard", SudokuVeryHardData.GetData(dir));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
