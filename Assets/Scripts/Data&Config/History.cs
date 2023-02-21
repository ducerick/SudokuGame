using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class History : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static string dir = Application.persistentDataPath;
#endif

#if UNITY_EDITOR
    private static string dir = Directory.GetCurrentDirectory();
#endif

    public static string hs1 = "hs1.txt";
    public static string hs2 = "hs2.txt";
    public static string hs3 = "hs3.txt";
    public static string hs4 = "hs4.txt";
    public static string hs5 = "hs5.txt";

    public List<GameObject> list = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetUpFile()
    {
        CopyFile(dir + hs4, dir + hs5);
        CopyFile(dir + hs3, dir + hs4);
        CopyFile(dir + hs2, dir + hs3);
        CopyFile(dir + hs1, dir + hs2);
    }

    private static void CopyFile(string source, string dest)
    {
        string content = File.ReadAllText(source);
        File.AppendAllText(dest, content);
    }

}
