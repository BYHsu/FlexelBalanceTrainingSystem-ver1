using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateScreenshotDirectory();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    CaptureScreenshot();
        //}
    }

    void CreateScreenshotDirectory() //創立當天實驗數據資料夾
    {
        string currentDate = DateTime.Now.ToString("yyyyMMdd"); //現在時間
        // 取得完整的目錄路徑
        string directoryPath = Application.persistentDataPath + "/" + currentDate;

        // 檢查目錄是否存在，如果不存在則建立
        if (!System.IO.Directory.Exists(directoryPath))
        {
            System.IO.Directory.CreateDirectory(directoryPath);
            Debug.Log("Screenshot directory created: " + directoryPath);
        }
    }

    public void CaptureScreenshot(int Count) //重心軌跡紀錄_
    {
        // 取得當前日期並將其格式化為字串
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        //int Count = 0;
        string FileName = "重心軌跡紀錄_" + currentDate+ "_" + Count.ToString("0000") + ".png";
        string dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //string dataPath = Application.persistentDataPath + "/" + screenshotDirectory + "/" + currentDate + "重心軌跡紀錄.png";
        int width = Screen.width*2;
        int height = Screen.height*2;
        //while (File.Exists(dataPath))
        //{
        //    Count++;
        //    FileName = "重心軌跡紀錄" + "_" + Count.ToString("0000") + ".png";
        //    dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //}
        // 建立RenderTexture來儲存截圖
        RenderTexture rt = new RenderTexture(width, height, 24);
        Camera.main.targetTexture = rt;

        // 創建2D紋理，並從RenderTexture中讀取數據
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 轉換紋理資料為位元組數組
        byte[] bytes = screenShot.EncodeToPNG();

        // 儲存影像到文件
        System.IO.File.WriteAllBytes(dataPath, bytes);
        Debug.Log(width+"*"+height);
        Debug.Log(dataPath);
        //Debug.Log("Screenshot captured! Saved as: " + fileName);
    }

    public void CaptureScreenshot2(int Count) //重心軌跡紀錄(難度底圖)
    {
        // 取得當前日期並將其格式化為字串
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        //int Count = 0;
        string FileName = "重心軌跡紀錄(難度底圖)" + currentDate + "_" + Count.ToString("0000") + ".png";
        string dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //string dataPath = Application.persistentDataPath + "/" + screenshotDirectory + "/" + currentDate + "重心軌跡紀錄.png";
        int width = Screen.width * 2;
        int height = Screen.height * 2;
        //while (File.Exists(dataPath))
        //{
        //    Count++;
        //    FileName = "重心軌跡紀錄(難度底圖)" + "_" + Count.ToString("0000") + ".png";
        //    dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //}
        // 建立RenderTexture來儲存截圖
        RenderTexture rt = new RenderTexture(width, height, 24);
        Camera.main.targetTexture = rt;

        // 創建2D紋理，並從RenderTexture中讀取數據
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 轉換紋理資料為位元組數組
        byte[] bytes = screenShot.EncodeToPNG();

        // 儲存影像到文件
        System.IO.File.WriteAllBytes(dataPath, bytes);
        Debug.Log(width + "*" + height);
        Debug.Log(dataPath);
        //Debug.Log("Screenshot captured! Saved as: " + fileName);
    }
}
