using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class Config : MonoBehaviour
{
    private static string dir;
    private void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
    dir = Application.persistentDataPath;
    #endif

    #if UNITY_EDITOR
        dir = Directory.GetCurrentDirectory();
    #endif
}

    private static string file = "/Assets/Resources/board_data.txt";
    private static string path = dir + file;

    public static void SetPath(string _file) => path = dir + _file;
    public static void SetDefaultPath() => path = dir + file;

    public static void DeleteDataFile()
    {
        File.Delete(path);
    }

    public static bool GameDataFileExist() => File.Exists(path);

    public static void SaveBoardData(SudokuData.SudokuBoardData boardData, string level, int boardIndex, int errorNumber, Dictionary<string, List<string>> gridNote, int score)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
        string current_time = "#time:" + Clock.GetCurrentTimeString();
        string level_string = "#level:" + level;
        string error_number = "#errors:" + errorNumber;
        string board_index = "#board_index:" + boardIndex.ToString();
        string unsolved_string = "#unsolved:";
        string solved_string = "#solved:";
        string score_string = "#score:" + score.ToString();

        foreach (var unsolved_data in boardData.UnsolvedData)
        {
            unsolved_string += unsolved_data.ToString() + ",";
        }

        foreach (var solved_data in boardData.SolvedData)
        {
            solved_string += solved_data.ToString() + ",";
        }

        writer.WriteLine(current_time);
        writer.WriteLine(level_string);
        writer.WriteLine(error_number);
        writer.WriteLine(board_index);
        writer.WriteLine(unsolved_string);
        writer.WriteLine(solved_string);
        writer.WriteLine(score_string);

        foreach (var square in gridNote)
        {
            string square_string = "#" + square.Key + ":";
            bool save = false;

            foreach (var note in square.Value)
            {
                if (note != " ")
                {
                    square_string += note + ",";
                    save = true;
                }
            }

            if (save)
            {
                writer.WriteLine(square_string);
            }
        }

        writer.Close();
    }

    public static Dictionary<int, List<int>> GetGridNotes()
    {
        Dictionary<int, List<int>> grid_notes = new Dictionary<int, List<int>>();

        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        for(int index = 0; index < lines.Length -1; index ++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#square_note")
            {
                int square_index = -1;
                List<int> notes = new List<int>();
                int.TryParse(word[1], out square_index);

                string[] substring = Regex.Split(word[2], ",");

                foreach (var note in substring)
                {
                    int note_number = -1;
                    int.TryParse(note, out note_number);
                    if (note_number > 0)
                    {
                        notes.Add(note_number);
                    }
                }
                grid_notes.Add(square_index, notes);
            }
        }

        return grid_notes;

    }

    public static string ReadBoradLevel()
    {
        string level = "";
        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        for (int index = 0; index < lines.Length - 1; index++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#level")
            {
                level = word[1];
            }
        }
        return level;
    }

    public static SudokuData.SudokuBoardData ReadGridData()
    {
        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        int[] unsolved_data = new int[81];
        int[] solved_data = new int[81];

        int unsolved_index = 0;
        int solved_index = 0;

        for (int index = 0; index < lines.Length - 1; index++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#unsolved")
            {
                string[] substrings = Regex.Split(word[1], ",");

                foreach (var value in substrings)
                {
                    int square_number = -1;
                    if (int.TryParse(value, out square_number))
                    {
                        unsolved_data[unsolved_index] = square_number;
                        unsolved_index++;
                    }
                }
            }

            if (word[0] == "#solved")
            {
                string[] substrings = Regex.Split(word[1], ",");

                foreach (var value in substrings)
                {
                    int square_number = -1;
                    if (int.TryParse(value, out square_number))
                    {
                        solved_data[solved_index] = square_number;
                        solved_index++;
                    }
                }
            }

        }
        return new SudokuData.SudokuBoardData(unsolved_data, solved_data);
    }

    public static int ReadGameBoardLevel()
    {
        int level = -1;
        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        for (int index = 0; index < lines.Length - 1; index++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#board_index")
            {
                int.TryParse(word[1], out level);
            }
        }
        return level;
    }

    public static int ReadGameScore()
    {
        int score = -1;
        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        for (int index = 0; index < lines.Length - 1; index++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#board_index")
            {
                int.TryParse(word[1], out score);
            }
        }
        return score;
    }

    public static float ReadGameTime()
    {
        float time = -1f;
        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        for (int index = 0; index < lines.Length - 1; index++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#time")
            {
                float.TryParse(word[1], out time);
            }
        }
        return time;
    }

    public static int ErrorNumber()
    {
        int errors = 0;
        var rawData = Resources.Load<TextAsset>("board_data");
        string str = rawData.text;
        string[] lines = str.Split("\r\n");

        for (int index = 0; index < lines.Length - 1; index++)
        {
            string[] word = lines[index].Split(':');
            if (word[0] == "#errors")
            {
                int.TryParse(word[1], out errors);
            }
        }
        return errors;
    }

}
