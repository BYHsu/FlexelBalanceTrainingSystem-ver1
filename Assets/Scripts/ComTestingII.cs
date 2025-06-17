using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.Linq;
using System.IO;

public class ComTestingII : MonoBehaviour
{
    bool isRun = true;
    bool reading = true;
    //float x=0;
    public string rawData="";
    public string portName="";
    SerialPort sp;
    //public SerialPort sp = new SerialPort("COM5", 115200);
    private Thread receiveThread;
    byte[] cmdtmp = { };

    // Start is called before the first frame update
    void Start()
    {
        sp = new SerialPort(portName, 115200);
        isRun= true;
        
        if (SerialPort.GetPortNames().Any(x => x == portName))//設定COM
        {
            try
            {
                //sp.Close();
                sp.Open();
                sp.ReadTimeout = 200; // Maximum time waiting for connention
                receiveThread = new Thread(ReceiveThread);
                receiveThread.IsBackground = true; // will terminate when the application's main (foreground) thread exits
                receiveThread.Start(); // initiates the execution of the thread
                Debug.Log(portName+" is Open.");
            }
            catch
            {
                // Handle exception
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
    }

    // Update is called once per frame
    void Update()
    {
        if (reading)
        {
            if (rawData != "" && rawData != null)//get raw data
            {
                //rawData = sp.ReadByte().ToString();
                rawData = sp.ReadTo("\n");
                //整理rawData的 function
                Debug.Log("Received "+ portName +" Data: " + rawData);
                //Debug.Log("Received sp2 Data: " + rawData2);
            }
            rawData = null;
        }
    }
    private void OnApplicationQuit()
    {
        sp.Close();
        isRun = false;
        Debug.Log(portName+" is closed");
    }

    private void ReceiveThread()
    {
        while (isRun)
        {

            if (this.sp != null && this.sp.IsOpen)
            {
                try
                {
                    rawData = sp.ReadByte().ToString();
                    //rawData = sp.ReadLine();
                    //Debug.Log(rawData);
                }
                catch
                {

                }
            }
        }
    }

    public void StartRead()
    {
        reading = true;
    }
    public void StopRead()
    {
        reading = false;
    }
    public void controllerswitchon()
    {
        cmdtmp = new byte[] { 0x01 };
        sp.Write(cmdtmp, 0, cmdtmp.Length);
    }
    public void controllerswitchoff()
    {
        cmdtmp = new byte[] { 0x02 };
        sp.Write(cmdtmp, 0, cmdtmp.Length);
    }
}
