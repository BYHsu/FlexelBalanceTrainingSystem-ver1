using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MainManager : MonoBehaviour
{
    public DataController dataController1;
    public DataController dataController2;
    public DataController dataController3;
    public DataController dataController4;
    public DataController dataController5;
    public DataController dataController6;
    public DataController dataController7;
    public DataController dataController8;
    public DataController dataController9;
    public DataController dataController10;
    //public GameObject dotCOG;
    public RectTransform rectTransformDotCOG;
    public LineRenderer lineRenderer;
    public MainController mainController;
    public ReadWriteFileData file;
    public ScreenShot screenShot;
    public int floorAmount;
    public int width;
    public int height;
    public float scale;
    public float cogX;
    public float cogY;
    //public float cogXmm = 0;
    //public float cogYmm = 0;

    //從floor[][]轉換
    int[][] floorData = new int[12][];
    //int[] averageArray = new int[6];
    //int initialTotalWeight = 0;
    int totalWeight = 0;
    int MAX = 0;
    int MIN = 0;
    int SUM = 0;
    int average = 0;
    //int average0 = 0;
    int averageMAX = 0;
    int averageMIN = 0;
    int amount = 0;
    int averageAVER = 0;
    int averageSUM = 0;
    float time = 0f;
    float timer = 0f;
    public float floorXmm = 0;
    public float floorYmm = 0;
    public float lastfloorXmm = 0;
    public float lastfloorYmm = 0;
    string currentDate;
    string fileName;
    string fileName2;
    float rectimer = 0f;
    int count = 0;
    public bool startrec = false;
    bool stoprec = true;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < floorData.Length; i++)
        //{
        //    floorData[i] = new int[6];
        //}
        //initialTotalWeight = InitialTotalWeight();
        for (int i = 0; i < 12; i++)
        {
            floorData[i] = new int[30];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i][j] = dataController1.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i + 6][j] = dataController2.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i][j + 6] = dataController3.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i + 6][j + 6] = dataController4.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i][j + 12] = dataController5.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i + 6][j + 12] = dataController6.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i][j + 18] = dataController7.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i + 6][j + 18] = dataController8.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i][j + 24] = dataController9.floor[i][j];
            }
        }
        for (int i = 0; i < 6; i++)//重心計算
        {
            for (int j = 0; j < 6; j++)
            {
                floorData[i + 6][j + 24] = dataController10.floor[i][j];
            }
        }

        //totalWeight = TotalWeight();
        int sum = 0;
        for (int m = 0; m < 12; m++)
        {
            for (int n = 0; n < 30; n++)
            {
                sum += floorData[m][n];
            }
        }


        if (sum == 0) sum = 1;
        totalWeight = sum;
        //initialTotalWeight = InitialTotalWeight();
        cogX = 0;
        cogY = 0;
        //SUM = 0;

        for (int i = 0; i < 12; i++)//重心計算
        {
            for (int j = 0; j < 30; j++)
            {
                //if (dataController1.floor[i][j] != 0)
                //{
                //    if (dataController1.floor[i][j] > MAX)
                //    {
                //        MAX = dataController1.floor[i][j];
                //    }
                //    if (dataController1.floor[i][j] < MIN)
                //    {
                //        MIN = dataController1.floor[i][j];
                //    }
                //}

                //SUM += dataController1.floor[i][j];

                //重心公式1
                cogX += j * (float)floorData[i][j] / totalWeight;
                cogY += i * (float)floorData[i][j] / totalWeight;
            }
        }
        //Debug.Log("Amount:\t" + amount + "\tDataMAX:\t" + MAX + "\tDataMIN:\t" + MIN + "\tDataAverage:\t" + average + "\tDataAverAver:\t" + averageAVER + "\tAverageMAX:\t" + averageMAX + "\tAverageMIN:\t" + averageMIN);

        //Debug.Log(cogX + "," + cogY+"," + totalWeight);
        //cogXmm = FloorXmm(cogX);
        //cogYmm = FloorYmm(cogY);
        //更新至cog球物件之位置
        //rectTransformDotCOG.anchoredPosition = new Vector2(cogX * scale, cogY * -scale);
        floorXmm = FloorXmm(cogX);
        floorYmm = FloorYmm(cogY);
        if (startrec)
        {
            if (rectimer == 0)
            {
                stoprec = false;
                currentDate = DateTime.Now.ToString("yyyyMMdd");
                fileName = currentDate + "/" + "TrackXY_" + currentDate;
                fileName2 = currentDate + "/" + "RawData_" + currentDate;
                count = file.CheckCount(fileName);
                Debug.Log("StartRec");
                string text = "StartTime:" + "\t" + DateTime.Now.ToString() + "\t" + "Difficulty:" + mainController.difficulty + "\n";
                text += "Time" + "\t" + "floorXmm" + "\t" + "floorYmm" + "\n";
                file.WriteToFile(fileName + "_" + count.ToString("0000"), text);
                string text2 = "StartTime:" + "\t" + DateTime.Now.ToString() + "\t" + "Difficulty:" + mainController.difficulty + "\n";
                file.WriteToFile(fileName2 + "_" + count.ToString("0000"), text2);
            }
            rectimer += Time.deltaTime;
            file.WriteToFile(fileName + "_" + count.ToString("0000"), rectimer + "\t" + floorXmm + "\t" + floorYmm + "\n");
            mainController.TimeText.text = (rectimer/60).ToString("00")+":"+ (rectimer % 60).ToString("00");
            string text3 = "";
            for (int m = 0; m < 12; m++)
            {
                for (int n = 0; n < 30; n++)
                {
                    text3 += floorData[m][n].ToString() + "\t";
                }
            }
            text3 += "\n";
            file.WriteToFile(fileName2 + "_" + count.ToString("0000"), text3);
            if (cogX != 0 && cogY != 0)
            {


                if (Vector2.Distance(new Vector2(floorXmm, floorYmm), new Vector2(lastfloorXmm, lastfloorYmm)) <= 1500 || lastfloorXmm == 0)
                {
                    if (FloorXmm(cogX) != 0 && FloorYmm(cogY) != 0 && FloorXmm(cogX) <= 3000 && FloorYmm(cogY) <= 1500 && floorXmm != lastfloorXmm && floorYmm != lastfloorYmm)
                    {
                        lineRenderer.positionCount = amount + 1;
                        lineRenderer.SetPosition(amount, new Vector3(FloorXmm(cogX) / 2433.07f * 1450, FloorYmm(cogY) / 951.84f * 550 * -1, 0));
                        amount++;
                    }
                    lastfloorXmm = floorXmm;
                    lastfloorYmm = floorYmm;
                }

            }
        }
        else
        {
            if (!stoprec)
            {
                Debug.Log("StopRec");
                string text = "StopTime:" + "\t" + DateTime.Now.ToString() + "\t" + "Difficulty:" + mainController.difficulty + "SpendTime:" + rectimer + "秒" + "\n";
                file.WriteToFile(fileName + "_" + count.ToString("0000"), text);
                string text2 = "StopTime:" + "\t" + DateTime.Now.ToString() + "\t" + "Difficulty:" + mainController.difficulty + "SpendTime:" + rectimer + "秒" + "\n";
                file.WriteToFile(fileName2 + "_" + count.ToString("0000"), text2);
                screenShot.CaptureScreenshot(count);
                stoprec = true;
            }
        }
    }
    float FloorXmm(float _cogX)
    {
        float _cogXmm = 0;
        float XA = 113.83f, XB = 51.83f;
        //if (_cogX > 0 && _cogX <= 1)//0
        //{
        //    _cogXmm = _cogX * XA;
        //}
        //if (_cogX > 1 && _cogX <= 2)
        //{
        //    _cogXmm = XA + (_cogX - 1) * XB;
        //}
        //if (_cogX > 2 && _cogX <= 3)//0
        //{
        //    _cogXmm = XA + XB + (_cogX - 2) * XA;
        //}
        //if (_cogX > 3 && _cogX <= 4)
        //{
        //    _cogXmm = XA + XB + XA + (_cogX - 3) * XB;
        //}
        //if (_cogX > 4 && _cogX <= 5)//0
        //{
        //    _cogXmm = XA*2 + XB*2 + (_cogX - 4) * XA;
        //}
        if ((int)(_cogX % 2) == 0)
        {
            _cogXmm = (float)(XA * (int)(_cogX / 2)) + (float)(XB * (int)(_cogX / 2)) + (float)(_cogX % 1) * XA;
        }
        else
        {
            _cogXmm = (float)(XA * ((int)(_cogX / 2) + 1)) + (float)(XB * (int)(_cogX / 2)) + (float)(_cogX % 1) * XB;
        }


        return _cogXmm;
    }
    float FloorYmm(float _cogY)
    {
        float _cogYmm = 0;
        float YA = 119.14f, YB = 47.4f;
        //if (_cogY > 0 && _cogY <= 1)
        //{
        //    _cogYmm = _cogY * YA;
        //}
        //if (_cogY > 1 && _cogY <= 2)
        //{
        //    _cogYmm = YA + (_cogY - 1) * YB;
        //}
        //if (_cogY > 2 && _cogY <= 3)
        //{
        //    _cogYmm = YA + YB + (_cogY - 2) * YA;
        //}
        //if (_cogY > 3 && _cogY <= 4)
        //{
        //    _cogYmm = YA + YB + YA + (_cogY - 3) * YB;
        //}
        //if (_cogY > 4 && _cogY <= 5)
        //{
        //    _cogYmm = YA + YB + YA + YB + (_cogY - 4) * YA;
        //}
        if ((int)(_cogY % 2) == 0)
        {
            _cogYmm = (YA * (int)(_cogY / 2)) + (YB * (int)(_cogY / 2)) + (_cogY % 1) * YA;
        }
        else
        {
            _cogYmm = (YA * ((int)(_cogY / 2) + 1)) + (YB * (int)(_cogY / 2)) + (_cogY % 1) * YB;
        }
        return _cogYmm;
    }
    private void OnEnable()
    {
        rectimer = 0;
    }
    private void OnDisable()
    {
        rectimer = 0;
        lineRenderer.positionCount = 0;
        totalWeight = 0;
        MAX = 0;
        MIN = 0;
        SUM = 0;
        average = 0;
        averageMAX = 0;
        averageMIN = 0;
        amount = 0;
        averageAVER = 0;
        averageSUM = 0;
        floorXmm = 0;
        floorYmm = 0;
        lastfloorXmm = 0f;
        lastfloorYmm = 0f;
    }
    //int InitialTotalWeight()//f=0時總重量
    //{
    //    int sum = 0;
    //    if (time == 0)
    //    {
    //        for (int m = 0; m < 6; m++)
    //        {
    //            for (int n = 0; n < 6; n++)
    //            {
    //                sum += dataController1.floor[m][n];
    //            }
    //        }
    //    }
    //    time += Time.deltaTime;
    //    return sum;
    //}

    int TotalWeight()//總重量
    {
        int sum = 0;
        for (int m = 0; m < 6; m++)
        {
            for (int n = 0; n < 6; n++)
            {
                sum += dataController1.floor[m][n];
            }
        }
        return sum;
    }


    //int[] DataValue()//依序將二維矩陣floor[][]值丟入一維矩陣內
    //{
    //    int[] dataValue = new int[36];
    //    int dataAmount = 0;
    //    for (int m = 0; m < 6; m++)
    //    {
    //        for (int n = 0; n < 6; n++)
    //        {
    //            dataValue[dataAmount] = dataController1.floor[m][n];
    //            dataAmount++;
    //        }
    //    }
    //    return dataValue;
    //}
}
