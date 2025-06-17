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

    void CreateScreenshotDirectory() //�Х߷�ѹ���ƾڸ�Ƨ�
    {
        string currentDate = DateTime.Now.ToString("yyyyMMdd"); //�{�b�ɶ�
        // ���o���㪺�ؿ����|
        string directoryPath = Application.persistentDataPath + "/" + currentDate;

        // �ˬd�ؿ��O�_�s�b�A�p�G���s�b�h�إ�
        if (!System.IO.Directory.Exists(directoryPath))
        {
            System.IO.Directory.CreateDirectory(directoryPath);
            Debug.Log("Screenshot directory created: " + directoryPath);
        }
    }

    public void CaptureScreenshot(int Count) //���߭y�����_
    {
        // ���o��e����ñN��榡�Ƭ��r��
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        //int Count = 0;
        string FileName = "���߭y�����_" + currentDate+ "_" + Count.ToString("0000") + ".png";
        string dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //string dataPath = Application.persistentDataPath + "/" + screenshotDirectory + "/" + currentDate + "���߭y�����.png";
        int width = Screen.width*2;
        int height = Screen.height*2;
        //while (File.Exists(dataPath))
        //{
        //    Count++;
        //    FileName = "���߭y�����" + "_" + Count.ToString("0000") + ".png";
        //    dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //}
        // �إ�RenderTexture���x�s�I��
        RenderTexture rt = new RenderTexture(width, height, 24);
        Camera.main.targetTexture = rt;

        // �Ы�2D���z�A�ñqRenderTexture��Ū���ƾ�
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // �ഫ���z��Ƭ��줸�ռƲ�
        byte[] bytes = screenShot.EncodeToPNG();

        // �x�s�v������
        System.IO.File.WriteAllBytes(dataPath, bytes);
        Debug.Log(width+"*"+height);
        Debug.Log(dataPath);
        //Debug.Log("Screenshot captured! Saved as: " + fileName);
    }

    public void CaptureScreenshot2(int Count) //���߭y�����(���ש���)
    {
        // ���o��e����ñN��榡�Ƭ��r��
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        //int Count = 0;
        string FileName = "���߭y�����(���ש���)" + currentDate + "_" + Count.ToString("0000") + ".png";
        string dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //string dataPath = Application.persistentDataPath + "/" + screenshotDirectory + "/" + currentDate + "���߭y�����.png";
        int width = Screen.width * 2;
        int height = Screen.height * 2;
        //while (File.Exists(dataPath))
        //{
        //    Count++;
        //    FileName = "���߭y�����(���ש���)" + "_" + Count.ToString("0000") + ".png";
        //    dataPath = Application.persistentDataPath + "/" + currentDate + "/" + FileName;
        //}
        // �إ�RenderTexture���x�s�I��
        RenderTexture rt = new RenderTexture(width, height, 24);
        Camera.main.targetTexture = rt;

        // �Ы�2D���z�A�ñqRenderTexture��Ū���ƾ�
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // �ഫ���z��Ƭ��줸�ռƲ�
        byte[] bytes = screenShot.EncodeToPNG();

        // �x�s�v������
        System.IO.File.WriteAllBytes(dataPath, bytes);
        Debug.Log(width + "*" + height);
        Debug.Log(dataPath);
        //Debug.Log("Screenshot captured! Saved as: " + fileName);
    }
}
