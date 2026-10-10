using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public float loadingTimer;

    protected float currentTimer;

    public Image loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0.0f;
        Application.targetFrameRate = 60;

        // 设置屏幕方向为横屏
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        loadingBar.fillAmount = (float)currentTimer / (float)loadingTimer;

        if (currentTimer >= loadingTimer)
        {
            SceneManager.LoadScene("Play", LoadSceneMode.Single);
        }
    }
}
