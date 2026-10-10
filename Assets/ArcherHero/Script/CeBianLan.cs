using System.Collections;
using System.Collections.Generic;
using TTSDK;
using TTSDK.UNBridgeLib.LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CeBianLan : MonoBehaviour
{
    public bool IsDeleteData;
    //前往按钮
    public Button btn_Go;
    //侧边栏
    public GameObject ceBianLan;
    //侧边栏支持状态
    private bool isSideBarSupported = false;
#if UNITY_EDITOR

    private void Awake()
    {
        if (IsDeleteData)
        {
            TT.PlayerPrefs.DeleteAll();
            TT.PlayerPrefs.Save();
        }
    }
#endif
    private void Start()
    {
        //检查是否支持侧边栏功能
        //CheckSideBarSupport();
        btn_Go.onClick.AddListener(NavigateToSideBar);
    }

    /// <summary>
    /// 跳转到侧边栏
    /// </summary>
    public void NavigateToSideBar()
    {
        try
        {
            var data = new JsonData
            {
                ["scene"] = "sidebar",
            };
            TT.NavigateToScene(data, () =>
            {
                Debug.Log("navigate to scene success");
            }, () =>
            {
                Debug.Log("navigate to scene complete");
            }, (errCode, errMsg) =>
            {
                Debug.Log($"navigate to scene error, errCode:{errCode}, errMsg:{errMsg}");
            });
        }
        catch (System.Exception e)
        {
            Debug.LogError("跳转到侧边栏异常: " + e.Message);
        }
    }

}
