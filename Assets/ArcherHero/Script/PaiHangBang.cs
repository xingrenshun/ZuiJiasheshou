using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
/// <summary>
/// 抖音排行榜功能实现类
/// 提供简单的分数上传和排行榜显示功能
/// </summary>
public class PaiHangBang : MonoBehaviour
{
    // 测试用的上传分数按钮
    public Button btn_UploadScore;
    // 排行榜面板
    public GameObject rankPanel;
    // 分享按钮
    public Button btn_Share;

    // 测试用的分数
    private int testScore = 100;
    private void Start()
    {            
        if (btn_UploadScore != null)
            btn_UploadScore.onClick.AddListener(() => UploadScore(testScore++));

        if (btn_Share != null)
            btn_Share.onClick.AddListener(ShareGame);
    }

    /// <summary>
    /// 打开排行榜面板
    /// </summary>
    public void OpenRankPanel()
    {
        //if (rankPanel != null)
        //    rankPanel.SetActive(true);
            
        // 显示排行榜数据
        ShowRankList();
    }



    public void CheckSession(bool callShow = false, Action callback = null)
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

    /// <summary>
    /// 上传分数到抖音排行榜
    /// </summary>
    /// <param name="score">要上传的分数</param>
    /// <param name="zoneId">排行榜分区，默认使用"default"</param>
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
    /// 显示排行榜数据
    /// </summary>
    /// <param name="zoneId">排行榜分区，默认使用"default"</param>
    /// <param name="rankType">排行榜类型，1表示全局榜，2表示好友榜，默认全局榜</param>
    public void ShowRankList(string zoneId = "default", int rankType = 1)
    {
        try
        {
            CheckSession(true, () =>
            {
                // 构建排行榜请求参数
                JsonData param = new JsonData()
                {
                    ["rankType"] = "all", // 时间范围：day、week、month、all
                    ["dataType"] = 0, // 0：数字类型 、 1：枚举类型
                    ["relationType"] = rankType == 1 ? "all" : "friend", // friend：好友榜，all: 总榜
                    ["pageNum"] = 1, // 页码，从1开始
                    ["pageSize"] = 20, // 每页长度，大于0小于40
                    ["zoneId"] = zoneId // 排行榜分区标识
                };
                //TT.GetImRankData(param, (ref TTRank.RankData data) =>
                //{
                //    Debug.Log(data.ToString());
                //    Debug.Log($"data: {data}");
                //    Debug.Log($"data: {data.Items.Count}");
                //    for (var i = 0; i < data.Items.Count; i++)
                //    {
                //        Debug.Log($"item-->nickName :{data.Items[i].Nickname}, openId:{data.Items[i].OpenId}");
                //    }
                //    Debug.Log("GetImRankDataNew true");
                //}, msg =>
                //{
                //    Debug.Log("GetImRankData " + msg);
                //});
                TT.GetImRankList(param);
            });
            
        }
        catch (Exception e)
        {
            Debug.LogError($"获取排行榜时发生错误：{e.Message}");
        }
    }

   
    /// <summary>
    /// 分享功能实现
    /// </summary>
    public void ShareGame()
    {
        try
        {
            // 构建分享参数
            JsonData shareData = new JsonData()
            {
                ["title"] = "快来试试这款有趣的狗狗拯救游戏！",
                ["content"] = "我在SaveMyPet中取得了高分，快来挑战我吧！",
                ["imageUrl"] = "", // 分享图片URL
                ["shareType"] = 0 // 分享类型：0-图文分享
            };
            
            Debug.Log("准备分享游戏");
            
            // 调用抖音分享API
            TT.ShareAppMessage(shareData);
        }
        catch (Exception e)
        {
            Debug.LogError("分享游戏时发生错误：" + e.Message);
        }
    }

    

}
