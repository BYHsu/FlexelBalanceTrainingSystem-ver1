using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using TMPro;

public class MainController : MonoBehaviour
{
    public GameObject ImageMain;
    public GameObject ImageSelect;
    public GameObject Image3;
    public GameObject ImageReady;
    public GameObject ImageStartrec;
    public GameObject ImageStop;
    public GameObject Rec;
    public GameObject Floor;
    public MainManager mainManager;
    public TextMeshProUGUI NumText;
    public TextMeshProUGUI DifficultyText;
    public TextMeshProUGUI NumText2;
    public TextMeshProUGUI DifficultyText2;
    public TextMeshProUGUI TimeText;
    public GameObject[] Background;
    public State state;
    float time = 0;
    bool nextStateLock;
    public string difficulty = "01_01";
    bool openBackground;
    int countnum;
    public enum State
    {
        None,
        Main,
        Select,
        Ready,
        Startrec,
        Stop
    }
    void Start()
    {
        state = State.Main;
        Application.targetFrameRate = 120;
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (state)
        {
            case State.None:
                break;
            case State.Main:
                Main();
                break;
            case State.Select:
                Select();
                break;
            case State.Ready:
                Ready();
                break;
            case State.Startrec:
                Startrec();
                break;
            case State.Stop:
                Stop();
                break;
            default:
                break;
        }
    }
    void Main()
    {
        if (time == 0)
        {
            Floor.SetActive(true);
            Image3.SetActive(false);
            string currentDate = DateTime.Now.ToString("yyyyMMdd"); //現在時間string
            string fileName = currentDate + "/" + "TrackXY_" + currentDate; //檔案名稱 = 現在時間/TrackXY_現在時間
            string count = GetComponent<ReadWriteFileData>().CheckCount(fileName).ToString("0000");
            NumText.text = count;
            DifficultyText.text = difficulty;
            AllOff();
            ImageMain.SetActive(true);
            Rec.SetActive(false);
        }
        time += Time.deltaTime;
    }
    void Select()
    {
        if (time == 0)
        {
            AllOff();
            ImageSelect.SetActive(true);
        }
        time += Time.deltaTime;
    }
    void Ready()
    {
        if (time == 0)
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string fileName = currentDate + "/" + "TrackXY_" + currentDate;
            countnum = GetComponent<ReadWriteFileData>().CheckCount(fileName);
            string count = countnum.ToString("0000");
            
            NumText2.text = count;
            DifficultyText2.text = difficulty;
            TimeText.text = "00:00";
            Image3.SetActive(true);
            AllOff();
            ImageReady.SetActive(true);
            Rec.SetActive(true);
        }
        time += Time.deltaTime;
    }
    void Startrec()
    {
        if (time == 0)
        {
            mainManager.startrec = true;
            AllOff();
            ImageStartrec.SetActive(true);
        }
        time += Time.deltaTime;
    }
    void Stop()
    {
        if (time == 0)
        {
            Floor.SetActive(false);
            mainManager.startrec = false;
            AllOff();
            ImageStop.SetActive(true);
        }
        time += Time.deltaTime;
    }
    void NextState(State _state)
    {
        //soundManager.UserVoice.PlayOneShot(soundManager.SwitchLevel, 1.5f);
        state = _state;
        time = 0;
        nextStateLock = false;
    }

    void AllOff()
    {
        ImageMain.SetActive(false);
        ImageSelect.SetActive(false);
        ImageReady.SetActive(false);
        ImageStartrec.SetActive(false);
        ImageStop.SetActive(false);
    }

    //按鈕觸發
    public void GoToNextState()
    {
        nextStateLock = true;
    }

    public void GoToMain()
    {
        time = 0;
        state = State.Main;
    }
    public void GoToSelect()
    {
        time = 0;
        state = State.Select;
    }
    public void GoToReady()
    {
        time = 0;
        state = State.Ready;
    }
    public void GoToStartrec()
    {
        time = 0;
        state = State.Startrec;
    }
    public void GoToStop()
    {
        time = 0;
        state = State.Stop;
    }
    public void Difficulty(string a)
    {
        difficulty = a;
    }
    public void OpenBackground() //按鈕觸發
    {
        openBackground = !openBackground;
        if (openBackground)
        {
            difficultyBack(difficulty);
            //string currentDate = DateTime.Now.ToString("yyyyMMdd");
            //string fileName = currentDate + "/" + "TrackXY_" + currentDate;
            //string countnum = GetComponent<ReadWriteFileData>().CheckCount(fileName).ToString("0000");
            GetComponent<ScreenShot>().CaptureScreenshot2(countnum);
        }
        else
        {
            for (int i = 0; i < Background.Length; i++)
            {
                Background[i].SetActive(false);
            }
        }
    }
    void difficultyBack(string _difficulty)
    {
        switch (_difficulty)
        {
            case "01_01":
                Background[0].SetActive(true);
                break;
            case "01_02":
                Background[1].SetActive(true);
                break;
            case "01_03":
                Background[2].SetActive(true);
                break;
            case "01_04":
                Background[3].SetActive(true);
                break;
            case "02_01":
                Background[4].SetActive(true);
                break;
            case "02_02":
                Background[5].SetActive(true);
                break;
            case "02_03":
                Background[6].SetActive(true);
                break;
            case "02_04":
                Background[7].SetActive(true);
                break;
            case "03_01":
                Background[8].SetActive(true);
                break;
            case "03_02":
                Background[9].SetActive(true);
                break;
            case "03_03":
                Background[10].SetActive(true);
                break;
            case "04_01":
                Background[11].SetActive(true);
                break;
            case "04_02":
                Background[12].SetActive(true);
                break;
            case "04_03":
                Background[13].SetActive(true);
                break;
            default:
                break;
        }
    }
}
