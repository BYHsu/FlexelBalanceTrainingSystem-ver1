using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class ReadWriteFileData : MonoBehaviour
{
    public int CheckCount(string FileName) //檔案數量/訓練次數
    {
        int Count = 0;
        string ReFileName = FileName + "_" + Count.ToString("0000");
        string dataPath = Application.persistentDataPath + "/" + ReFileName + ".txt"; //路徑 = 原檔案路徑/FileName_0000.txt

        while (File.Exists(dataPath)) //路徑存在
        {
            Count++; //次數增加
            ReFileName = FileName + "_" + Count.ToString("0000");
            dataPath = Application.persistentDataPath + "/" + ReFileName + ".txt";
        }
        return Count;
    }
    public void WriteToFile(string FileName, string args)
    {
        string ReFileName = FileName;
        string dataPath = Application.persistentDataPath + "/" + ReFileName + ".txt";

        File.AppendAllText(dataPath, args);
        // Debug.Log("Write File:\n"+dataPath +"\n"+ "Text:" + "\n" + args);
    }

    public void ReWriteToFile(string FileName, string args, bool OverRide)
    {
        int Count = 0;
        string ReFileName = FileName + "_" + Count;
        string dataPath = Application.persistentDataPath + "/" + ReFileName + ".txt";

        if (!OverRide)
        {
            while (File.Exists(dataPath))
            {
                Count++;
                ReFileName = FileName + "_" + Count;
                dataPath = Application.persistentDataPath + "/" + ReFileName + ".txt";
            }
        }
        File.WriteAllText(dataPath, args);
        Debug.Log("Write File:\n" + dataPath + "\n" + "Text:" + "\n" + args);
    }

    public void ReWriteFile(string FileName, string args)
    {
        string dataPath = Application.persistentDataPath + "/" + FileName + ".txt";

        File.WriteAllText(dataPath, args);
        Debug.Log("Write File: " + dataPath + "\n" + "Text: " + args);
    }

    public int ReadFile(string FileName)
    {
        string inString;
        if (File.Exists(Application.persistentDataPath + "/" + FileName + ".txt"))
        {
            string path = Application.persistentDataPath + "/" + FileName + ".txt";

            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);

            inString = (reader.ReadToEnd());
            reader.Close();
            Debug.Log("Read File: " + path + "\n" + "Text: " + inString);
        }
        else
        {
            Debug.Log("Not Exist");
            inString = "0";
        }
        return int.Parse(inString);
    }

    public bool FileExistCheck(string FileName)
    {
        bool Exist;
        if (File.Exists(Application.persistentDataPath + "/" + FileName + ".txt"))
        {
            Exist = true;
        }
        else
        {
            Exist = false;
        }
        return Exist;
    }


}

