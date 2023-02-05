using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SudokuEasyData : MonoBehaviour
{


#if UNITY_ANDROID && !UNITY_EDITOR
    private static string dir = Application.persistentDataPath;
#endif

#if UNITY_EDITOR
    private static string dir = Directory.GetCurrentDirectory();
#endif

    public static readonly string TextFile = dir + @"/Assets/Graphics/SudokuData/Easy.ini";
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

public class SudokuMediumData : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static string dir = Application.persistentDataPath;
#endif

#if UNITY_EDITOR
    private static string dir = Directory.GetCurrentDirectory();
#endif

    public static readonly string TextFile = dir + @"/Assets/Graphics/SudokuData/Medium.ini";
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
#if UNITY_ANDROID && !UNITY_EDITOR
    private static string dir = Application.persistentDataPath;
#endif

#if UNITY_EDITOR
    private static string dir = Directory.GetCurrentDirectory();
#endif
    public static readonly string TextFile = dir + @"/Assets/Graphics/SudokuData/Hard.ini";
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
#if UNITY_ANDROID && !UNITY_EDITOR
    private static string dir = Application.persistentDataPath;
#endif

#if UNITY_EDITOR
    private static string dir = Directory.GetCurrentDirectory();
#endif
    public static readonly string TextFile = dir + @"/Assets/Graphics/SudokuData/VeryHard.ini";
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
