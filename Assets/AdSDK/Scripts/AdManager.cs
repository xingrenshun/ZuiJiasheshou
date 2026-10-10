using System;
using System.Collections.Generic;
using UnityEngine;

// 广告平台类型枚举
public enum AdPlatformType
{
    [InspectorName("抖音广告")]
    TickTok,
    [InspectorName("TopOn广告")]
    TopOn,
    [InspectorName("微信广告")]
    WebChat,
}


/// <summary>
/// 广告入口
/// </summary>
public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    [Header("平台")]
    public AdPlatformType adPlatformType = AdPlatformType.TickTok;
    [Header("小游戏ID")]
    public string AppId;
    [Header("激励广告位")]
    public List<string> videoAdIdList = new();
    [Header("插屏广告位")]

    public List<string> interAdIdList = new();
    private IAdPlatform platFormIns = null;
    private float lastAdClickTime;

     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (this.AppId == "")
        {
            Debug.LogWarning("请填写APPID");
        }

        if (this.videoAdIdList == null || this.videoAdIdList.Count == 0)
        {
            Debug.LogWarning("激励广告位ID列表为空, 将不会弹出任何激励广告");
        }

        if (this.interAdIdList == null || this.interAdIdList.Count == 0)
        {
            Debug.LogWarning("插屏广告位ID列表为空, 将不会弹出任何插屏广告");
        }

        switch (adPlatformType)
        {
            case AdPlatformType.TickTok:
#if TickTok
                platFormIns = new TikTokPlatform(this.AppId, this.GetRandVideoAdId(), this.GetRandScreenAdId());
#endif
                break;
            case AdPlatformType.TopOn:

                break;
        }
        this.lastAdClickTime = Time.time;
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    public void ShowInterstitialAd()
    {
        this.platFormIns?.ShowInterstitialAd(this.GetRandScreenAdId());
    }

    /// <summary>
    /// 唤起侧边栏
    /// </summary>
    /// <param name="onSuccess">成功回调</param>
    /// <param name="onFail">失败回调</param>
    public void NavigateToScene(Action onSuccess = null, Action<string> onFail = null)
    {
        this.platFormIns?.NavigateToScene(onSuccess, onFail);
    }

    //分享游戏
    public void ShareGame(Action<bool> callBack = null)
    {
        this.platFormIns?.ShareGame(callBack);
    }

    /// <summary>
    /// 激励视频广告
    /// </summary>
    /// <param name="onSuccess">成功回调</param>
    /// <param name="onFail">失败回调</param>
    public void ShowRewardedVideoAd(Action onSuccess, Action<string> onFail)
    {
        if (!this.CheckTimeLimit())
            return;
        this.platFormIns?.ShowRewardedVideoAd(this.GetRandVideoAdId(), onSuccess, onFail);
    }


    // 获取随机激励广告ID
    private string GetRandVideoAdId()
    {
        if (this.videoAdIdList.Count == 0)
            return "";
        int index = UnityEngine.Random.Range(0, this.videoAdIdList.Count);
        return this.videoAdIdList[index];
    }

    // 获取随机的插屏广告ID
    private string GetRandScreenAdId()
    {
        if (this.interAdIdList.Count == 0)
            return "";
        int index = UnityEngine.Random.Range(0, this.interAdIdList.Count);
        return this.interAdIdList[index];
    }

    // 限制x秒内广告只允许点击一次
    private bool CheckTimeLimit()
    {
        if (Time.time - lastAdClickTime < 1f)
        {
            Debug.LogWarning("请勿频繁点击广告，请稍后再试。");
            return false;
        }
        lastAdClickTime = Time.time;
        return true;
    }
}
