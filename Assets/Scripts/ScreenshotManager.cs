using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenshotManager : MonoBehaviour {

    public static ScreenshotManager instance = null;


    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) TakeScreenShot();
    }

    public static string ScreenShotName()
    {
        return string.Format("{0}/../Resources/Screenshots/screenshot_{1}.png",
                             Application.dataPath,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeScreenShot() {
        ScreenCapture.CaptureScreenshot(ScreenshotManager.ScreenShotName());
    }

}
