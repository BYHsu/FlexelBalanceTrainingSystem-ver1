using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using System;
using Defective.JSON;
using System.Xml.Linq;

public class ConfigTesting : MonoBehaviour
{
    public string[][] moduleLayout;
    public int[][] moduleDirection;
    //public string cameraWebcam;
    public List<string> output;
    public string recordType;
    public string recordDirectory;
    public string recordName;
    public int secondsPerFile;

    void Start()
    {

        // Define the file path and name
        string filePath = Application.dataPath + "/Config/configUnity.txt";

        // Read the text from the file
        string config = File.ReadAllText(filePath);
        Debug.Log("Text file content:\n" + config);//"config"

        var configJSON = new JSONObject(config);
        //Debug.Log(ModuleLayout(configJSON, 0, 0));//(JSON0, 0, 0) = COM5
        //JSONData jsonData = JsonUtility.FromJson<JSONData>(jsonContent);

        //"moduleLayout"
        int a = configJSON.list[0].count;
        int b = configJSON.list[0].list[0].count;
        moduleLayout = new string[a][];
        for (int i = 0; i < a; i++)//定義儲存空間
        {
            moduleLayout[i] = new string[b];
        }
        //ArrayList elements = (ArrayList)configJSON.list[j];
        for (int j = 0; j < a; j++)//將資料存入
        {
            for (int i = 0; i < b; i++)
            {
                moduleLayout[j][i] = ModuleLayout(configJSON, j, i);
                //Debug.Log(ModuleLayout(configJSON, j, i));
            }
        }
        for (int j = 0; j < a; j++)//將資料存入
        {
            for (int i = 0; i < b; i++)
            {
                Debug.Log(ModuleLayout(configJSON, j, i));
            }
        }
        //Debug.Log(moduleLayout);

        //"moduleDirection"
        int c = configJSON.list[1].count;
        int d = configJSON.list[1].list[0].count;
        moduleDirection = new int[c][];
        for (int i = 0; i < c; i++)//定義儲存空間
        {
            moduleDirection[i] = new int[d];
        }
        for (int j = 0; j < c; j++)//將資料存入
        {
            for (int i = 0; i < d; i++)
            {
                moduleDirection[j][i] = ModuleDirection(configJSON, j, i);
                //Debug.Log(ModuleDirection(configJSON, j, i));
            }
        }
        for (int j = 0; j < c; j++)//將資料存入
        {
            for (int i = 0; i < d; i++)
            {
                Debug.Log(ModuleDirection(configJSON, j, i));
            }
        }
        //Debug.Log(moduleDirection);

        //no "camera"
        //"output"
        int e = configJSON.list[3].count;
        int f = configJSON.list[3].list[0].count;
        output.Capacity = e;//e=2
        for (int i = 0; i < e; i++)//將資料存入
        {
            output.Add(Output(configJSON, i));
        }
        for (int i = 0; i < e; i++)//將資料存入
        {
            Debug.Log(output[i]);
        }
        //Debug.Log(output[0]);
        //Debug.Log(output[1]);

        //"recordType"
        recordType = RecordType(configJSON);
        Debug.Log(recordType);

        //"recordDirectory"
        recordDirectory = RecordDirectory(configJSON);
        Debug.Log(recordDirectory);

        //"recordName"
        recordName = RecordName(configJSON);
        Debug.Log(recordName);

        //"secondsPerFile"
        secondsPerFile = SecondsPerFile(configJSON);
        Debug.Log(secondsPerFile);

        // Display the file content
        //string lines = System.IO.File.ReadAllLines(@"configUnity.txt");
    }


    //"moduleLayout"://kind of ArrayList called "list"(key)
    //[
    //["COM5"]//kind of List called "keys"(value)
    //],

    public string ModuleLayout(JSONObject _configJSON, int row, int col)
    {
        string a = "ML";
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "moduleLayout")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value[row].list[col].stringValue;
            }
        }
        return a;
    }

    public int ModuleDirection(JSONObject _configJSON, int row, int col)
    {
        int a = 1;
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "moduleDirection")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value[row].list[col].intValue;
            }
        }

        return a;
    }

    public string Output(JSONObject _configJSON, int col)
    {
        string a = "OP";
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "output")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value.list[col].stringValue;
            }
        }

        return a;
    }

    public string RecordType(JSONObject _configJSON)
    {
        string a = "RT";
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "recordType")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value.stringValue;
            }
        }

        return a;
    }

    public string RecordDirectory(JSONObject _configJSON)
    {
        string a = "RD";
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "recordDirectory")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value.stringValue;
            }
        }

        return a;
    }

    public string RecordName(JSONObject _configJSON)
    {
        string a = "RN";
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "recordName")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value.stringValue;
            }
        }

        return a;
    }

    public int SecondsPerFile(JSONObject _configJSON)
    {
        int a = 7;
        for (var i = 0; i < _configJSON.list.Count; i++)//JSONObject.Type.Object
        {
            var key = _configJSON.keys[i];
            var value = _configJSON.list[i];
            //Debug.Log(key);
            if (key == "secondsPerFile")
            {
                //value[row] = _configJSON.list;
                //AccessData(value);
                a = value.intValue;
            }
        }

        return a;
    }

}
