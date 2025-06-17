using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FitScreen : MonoBehaviour
{

    public float ScreenWidth;
    public float ScreenHeight;
    public float originSizeWidth = 1920;
    public float originSizeHeight = 1080;
    // Start is called before the first frame update
    void Start()
    {
        if ((float)Screen.width / (float)Screen.height < originSizeWidth / originSizeHeight)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = (float)Screen.height / 200 / (float)Screen.width * originSizeWidth;
            ScreenWidth = originSizeWidth / 100;
            ScreenHeight = (float)Screen.height / 100 / (float)Screen.width * originSizeWidth; ;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = originSizeHeight / 200;
            ScreenWidth = originSizeHeight / 100 / (float)Screen.height * (float)Screen.width;
            ScreenHeight = originSizeHeight / 100;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
