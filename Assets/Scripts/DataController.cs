using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
    public string portName;
    public string rawData;
    public string[] dataStream = new string[0];
    public GameObject[] floorCell = new GameObject[36];
    public int baudRate;
    public int rotation;
    public int[][] floor = new int[6][];
    public int maxNumber;

    //int[] data = new int[36];
    //int dataStreamValue = 0;
    bool isRun = true;
    bool reading = true;
    SerialPort sp;
    Thread receiveThread;
    bool once;
    //int[][] idxlut0 = new int[][]
    //{
    //    new int[] {0, 1, 4, 5, 8, 9},
    //    new int[] {2, 3, 6, 7, 10, 11},
    //    new int[] {12, 13, 16, 17, 20, 21},
    //    new int[] {14, 15, 18, 19, 22, 23},
    //    new int[] {24, 25, 28, 29, 32, 33},
    //    new int[] {26, 27, 30, 31, 34, 35}
    //};

    //0度位置，最多僅能連接36片地板模組({5, 5})
    int[][] rotation0 = new int[][]
    {
        new int[] {0, 0}, new int[] {0, 1}, new int[] {1, 0}, new int[] {1, 1},
        new int[] {0, 2}, new int[] {0, 3}, new int[] {1, 2}, new int[] {1, 3},
        new int[] {0, 4}, new int[] {0, 5}, new int[] {1, 4}, new int[] {1, 5},

        new int[] {2, 0}, new int[] {2, 1}, new int[] {3, 0}, new int[] {3, 1},
        new int[] {2, 2}, new int[] {2, 3}, new int[] {3, 2}, new int[] {3, 3},
        new int[] {2, 4}, new int[] {2, 5}, new int[] {3, 4}, new int[] {3, 5},

        new int[] {4, 0}, new int[] {4, 1}, new int[] {5, 0}, new int[] {5, 1},
        new int[] {4, 2}, new int[] {4, 3}, new int[] {5, 2}, new int[] {5, 3},
        new int[] {4, 4}, new int[] {4, 5}, new int[] {5, 4}, new int[] {5, 5},
    };

    //int[][] idxlut90 = new int[][]
    //{
    //    new int[] {26, 24, 14, 12, 2, 0},
    //    new int[] {27, 25, 15, 13, 3, 1},
    //    new int[] {30, 28, 18, 16, 6, 4},
    //    new int[] {31, 29, 19, 17, 7, 5},
    //    new int[] {34, 32, 22, 20, 10, 8},
    //    new int[] {35, 33, 23, 21, 11, 9}
    //};

    //90度位置
    int[][] rotation90 = new int[][]
    {
        new int[] {0, 5}, new int[] {1, 5}, new int[] {0, 4}, new int[] {1, 4},
        new int[] {2, 5}, new int[] {3, 5}, new int[] {2, 4}, new int[] {3, 4},
        new int[] {4, 5}, new int[] {5, 5}, new int[] {4, 4}, new int[] {5, 4},

        new int[] {0, 3}, new int[] {1, 3}, new int[] {0, 2}, new int[] {1, 2},
        new int[] {2, 3}, new int[] {3, 3}, new int[] {2, 2}, new int[] {3, 2},
        new int[] {4, 3}, new int[] {5, 3}, new int[] {4, 2}, new int[] {5, 2},

        new int[] {0, 1}, new int[] {1, 1}, new int[] {0, 0}, new int[] {1, 0},
        new int[] {2, 1}, new int[] {3, 1}, new int[] {2, 0}, new int[] {3, 0},
        new int[] {4, 1}, new int[] {5, 1}, new int[] {4, 0}, new int[] {5, 0},
    };

    //int[][] idxlut180 = new int[][]
    //{
    //    new int[] {35, 34, 31, 30, 27, 26},
    //    new int[] {33, 32, 29, 28, 25, 24},
    //    new int[] {23, 22, 19, 18, 15, 14},
    //    new int[] {21, 20, 17, 16, 13, 12},
    //    new int[] {11, 10, 7, 6, 3, 2},
    //    new int[] { 9, 8, 5, 4, 1, 0}
    //};

    //180度位置
    int[][] rotation180 = new int[][]
    {
        new int[] {5, 5}, new int[] {5, 4}, new int[] {4, 5}, new int[] {4, 4},
        new int[] {5, 3}, new int[] {5, 2}, new int[] {4, 3}, new int[] {4, 2},
        new int[] {5, 1}, new int[] {5, 0}, new int[] {4, 1}, new int[] {4, 0},
        new int[] {3, 5}, new int[] {3, 4}, new int[] {2, 5}, new int[] {2, 4},
        new int[] {3, 3}, new int[] {3, 2}, new int[] {2, 3}, new int[] {2, 2},
        new int[] {3, 1}, new int[] {3, 0}, new int[] {2, 1}, new int[] {2, 0},
        new int[] {1, 5}, new int[] {1, 4}, new int[] {0, 5}, new int[] {0, 4},
        new int[] {1, 3}, new int[] {1, 2}, new int[] {0, 3}, new int[] {0, 2},
        new int[] {1, 1}, new int[] {1, 0}, new int[] {0, 1}, new int[] {0, 0}
    };

    //int[][] idxlut270 = new int[][]
    //{
    //    new int[] {9, 11, 21, 23, 33, 35},
    //    new int[] {8, 10, 20, 22, 32, 34},
    //    new int[] {5, 7, 17, 19, 29, 31},
    //    new int[] {4, 6, 16, 18, 28, 30},
    //    new int[] {1, 3, 13, 15, 25, 27},
    //    new int[] {0, 2, 12, 14, 24, 26}
    //};

    //270度位置
    int[][] rotation270 = new int[][]
    {
        new int[] {5, 0}, new int[] {4, 0}, new int[] {5, 1}, new int[] {4, 1},
        new int[] {3, 0}, new int[] {2, 0}, new int[] {3, 1}, new int[] {2, 1},
        new int[] {1, 0}, new int[] {0, 0}, new int[] {1, 1}, new int[] {0, 1},

        new int[] {5, 2}, new int[] {4, 2}, new int[] {5, 3}, new int[] {4, 3},
        new int[] {3, 2}, new int[] {2, 2}, new int[] {3, 3}, new int[] {2, 3},
        new int[] {1, 2}, new int[] {0, 2}, new int[] {1, 3}, new int[] {0, 3},

        new int[] {5, 4}, new int[] {4, 4}, new int[] {5, 5}, new int[] {4, 5},
        new int[] {3, 4}, new int[] {2, 4}, new int[] {3, 5}, new int[] {2, 5},
        new int[] {1, 4}, new int[] {0, 4}, new int[] {1, 5}, new int[] {0, 5},
    };

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (!once)
        {
            sp = new SerialPort(portName, baudRate);
            isRun = true;

            //need to define the array scale before the thread executed
            for (int i = 0; i < floor.Length; i++)
            {
                floor[i] = new int[6];
            }

            if (SerialPort.GetPortNames().Any(x => x == portName))//設定COM
            {
                try
                {
                    sp.Open();
                    sp.ReadTimeout = 200;
                    receiveThread = new Thread(ReceiveThread);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    Debug.Log(portName + " is Open.");
                }
                catch
                {
                    Debug.Log(portName + " is already in use.");
                    isRun = false;
                    reading = false;
                }
            }
            else
            {
                Debug.Log(portName + " is not exist.");
                isRun = false;
                reading = false;
            }
            //floorCell[4].GetComponent<Image>().enabled= false;
            //floorCell[4].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            once = true;
        }

    }
    private void OnDisable()
    {
        once = false;
        sp.Close();
        isRun = false;
        Debug.Log(portName + " is closed");
    }
    // Update is called once per frame
    void Update()
    {
        for (int v = 0; v < 6; v++)
        {
            for (int u = 0; u < 6; u++)
            {
                int Value = floor[v][u];
                if (Value == 0)
                {
                    floorCell[v * 6 + u].GetComponent<Image>().color = new Color(1, 1, 1);
                }
                else
                {
                    byte newValue = (byte)Remap(floor[v][u], 0, maxNumber, 170, 255);
                    floorCell[v * 6 + u].GetComponent<Image>().color = Color.HSVToRGB((float)newValue / 255, 1, 1);
                }
            }
        }
        //COGOperation: portName, floor, dataStream
    }

    private void ReceiveThread()//Thread 內部執行的程序 = Thread裡的Update()
    {
        while (isRun)
        {
            if (this.sp != null && this.sp.IsOpen)
            {

                try
                {
                    string dataPiece;
                    int byteValue = sp.ReadByte();
                    //將sp.ReadByte()從byte轉換為可印出的值以利後續的資料處理
                    char character = Encoding.ASCII.GetChars(new byte[] { (byte)byteValue })[0];//將byte轉化為character
                    dataPiece = character.ToString();
                    //if (dataPiece.Contains('-'))//負數變0
                    //{
                    //    dataPiece = "0";
                    //}
                    rawData += dataPiece;//將datapiece更新到rawdata中
                    if (dataPiece == "\n")//當更新到"\n"時停止
                    {
                        MatchCollection match = Regex.Matches(rawData, ",");//計算逗號的數量
                        int count = match.Count;
                        if (count == 35)//代表36顆感測器數值都有抓到
                        {
                            string rawDataCorrect = rawData;
                            //Debug.Log(rawDataCorrect);
                            dataStream = rawDataCorrect.Trim().Split(',');

                            for (int i = 0; i < dataStream.Length; i++)
                            {
                                //Debug.Log(dataStream[i]);

                                if (rotation == 0)
                                {
                                    int value = int.Parse(dataStream[i]) - 100000;
                                    if (value <= 0)
                                    {
                                        value = 0;
                                    }
                                    floor[rotation0[i][0]][rotation0[i][1]] = value;
                                }
                                else if (rotation == 90)
                                {
                                    floor[rotation90[i][0]][rotation90[i][1]] = int.Parse(dataStream[i]);
                                }
                                else if (rotation == 180)
                                {
                                    floor[rotation180[i][0]][rotation180[i][1]] = int.Parse(dataStream[i]);
                                }
                                else if (rotation == 270)
                                {
                                    floor[rotation270[i][0]][rotation270[i][1]] = int.Parse(dataStream[i]);
                                }
                                else
                                {
                                    floor[rotation0[i][0]][rotation0[i][1]] = 0;
                                    Debug.Log("unknown rotation");
                                }

                            }
                        }
                        rawData = "";
                    }
                }
                catch
                {

                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        sp.Close();
        isRun = false;
        Debug.Log(portName + " is closed");
    }

    public static float Remap(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
}
