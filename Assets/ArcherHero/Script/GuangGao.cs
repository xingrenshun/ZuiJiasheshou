using System;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using TTSDK.UNBridgeLib.LitJson;

public class GuangGao : Singleton<GuangGao>
{
    private static string XPId = "14cf2i3bad187n8m2p";
    private static string BannerId = "ldmcb9hb3dh1feigv8";
    private static string rewardId = "3c60st6lpjt190nc7p";

    TTInterstitialAd m_InterAdIns = null;
    TTBannerAd m_bannerAdIns = null;
    TTRewardedVideoAd m_rewardAdIns = null;
    Action rewardAction;
    private float gameStartTime; // 游戏启动时间
    private float showXPTime;//插屏广告显示时间
    private float showRewardTime;//激励广告显示时间
    protected override void Awake()
    {
        base.Awake();
        gameStartTime = Time.time; // 记录游戏启动时间
        TT.InitSDK();
        if (XPId != null)
            CreateXP();
        if (BannerId != null)
            CreatBanner();
        if (rewardId != null)
            CreateReward();

    }
    /// <summary>
    /// 检查游戏启动是否已经超过30秒
    /// </summary>
    /// <returns>如果游戏启动时间超过30秒返回true，否则返回false</returns>
    public bool IsGameStartedOver30Seconds()
    {
        return Time.time - gameStartTime >= 30f;
    }
    public bool IsShowXPStartedOver60Seconds()
    {
        if (showXPTime == 0) return true;
        return Time.time - showXPTime >= 60f;
    }
    public bool IsShowRewardStartedOver60Seconds()
    {
        if (showRewardTime == 0) return true;
        return Time.time - showRewardTime >= 60;
    }
    public void CreateXP()
    {
        var param = new CreateInterstitialAdParam { InterstitialAdId = XPId };
        m_InterAdIns = TT.CreateInterstitialAd(param);
        m_InterAdIns.OnClose += () => Debug.Log("插屏广告关闭");
        m_InterAdIns.OnLoad += () => Debug.Log("插屏广告加载");
        m_InterAdIns.OnError += (code, message) => Debug.Log($"错误 ： {code}  {message}");
    }
    public IEnumerator WaitShowXP()
    {
        yield return new WaitForSeconds(0.5f);
        GuangGao.Instance.ShowXP();

    }
    public void ShowXP()
    {
        if (IsGameStartedOver30Seconds())
        {
            if (IsShowRewardStartedOver60Seconds())
            {
                if (IsShowXPStartedOver60Seconds())
                {
                    m_InterAdIns.Show();
                    showXPTime = Time.time;
                }
                else
                {
                    Debug.Log("离打开插屏广告没到60s");
                }
            }
            else
            {
                Debug.Log("离打开激励广告没到60s");
            }
        }
        else
        {
            Debug.Log("30s没到");
        }
    }

    public void CreatBanner()
    {
        var param = new CreateBannerAdParam { BannerAdId = BannerId };
        m_bannerAdIns = TT.CreateBannerAd(param);
    }
    public void ShowBanner()
    {
        m_bannerAdIns.Show();
    }
    public void CloseBanner()
    {
        m_bannerAdIns.Hide();
    }
    public void CreateReward()
    {
        var param = new CreateRewardedVideoAdParam { AdUnitId = rewardId };
        m_rewardAdIns = TT.CreateRewardedVideoAd(param);
        m_rewardAdIns.OnClose += (b, i) =>
        {
            if (b)
            {
                rewardAction?.Invoke();
            }
            else
            {
                //
            }
        };
        m_rewardAdIns.OnError += (b, i) =>
        {
            m_rewardAdIns.Show();

        };
    }
    public void ShowReward(Action action)
    {
        m_rewardAdIns.Load();
        m_rewardAdIns.Show();
        rewardAction = action;
        showRewardTime = Time.time;
    }
    /// <summary>
    /// 上传分数
    /// </summary>
    /// <param name="score"></param>
    /// <param name="zoneId"></param>
    public void UploadScore(int score, string zoneId = "default")
    {
        try
        {
            JsonData data = new JsonData()
            {
                ["dataType"] = 0,
                ["value"] = score.ToString(),
                ["priority"] = 0,
                ["zoneId"] = zoneId
            };
            CheckSession(true, () =>
            {
                TT.SetImRankData(data, (b, a) =>
                {
                    if (b)
                    {
                        Debug.Log("上传数据");
                    }
                    else
                    {
                        Debug.Log("上传数据");
                    }
                });
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"上传分数时发生错误：{e.Message}");
        }
    }
    /// <summary>
    /// 登录
    /// </summary>
    public void CheckSession(Boolean callShow = false, Action callback = null)
    {
        TT.CheckSession(() =>
        {
            if (callShow)
            {
                callback?.Invoke();
            }
        }, (err) =>
        {
            TT.Login((code, anonymousCode, isLogin) =>
            {
                if (callShow)
                {
                    callback?.Invoke();
                }
            },
        (msg) => { Debug.Log($"TestLogin: {msg}"); }, true);
        });
    }

}
