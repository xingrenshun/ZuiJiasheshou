using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 激励广告弹窗
/// </summary>
public class AdConfirm : MonoBehaviour
{
    private static AdConfirm instance;

    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnConfirm;
    private Action onCompleted;

    public static void Show(Action callBack)
    {
        if (instance == null)
        {
            // 加载 AdConfirm 预制体
            GameObject prefab = Resources.Load<GameObject>("AdConfirm");
            if (prefab == null)
            {
                Debug.LogError("AdConfirm 预制体未找到，请确保其位于 Resources 文件夹中。");
                return;
            }

            // 实例化 AdConfirm
            GameObject adConfirmInstance = Instantiate(prefab);
            instance = adConfirmInstance.GetComponent<AdConfirm>();

            if (instance == null)
            {
                Debug.LogError("AdConfirm 脚本未附加到预制体上，请检查预制体设置。");
                Destroy(adConfirmInstance);
                return;
            }
        }

        // 设置回调并显示弹窗
        instance.onCompleted = callBack;
        instance.gameObject.SetActive(true);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        gameObject.SetActive(false);
        btnClose?.onClick.AddListener(this.OnClose);
        btnConfirm?.onClick.AddListener(this.OnConfirm);
    }


    private void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    void OnConfirm()
    {
#if UNITY_EDITOR
        // 编辑器模式下直接触发成功回调（方便测试）
        onCompleted?.Invoke();
        this.OnClose();
#else
    // 调用AdManager的激励广告方法，传入观看成功和失败的回调
    AdManager.Instance.ShowRewardedVideoAd(OnAdWatched, OnAdError); 
#endif
    }


    private void OnAdWatched()
    {
        Debug.Log("广告观看完成");
        onCompleted?.Invoke();
        OnClose();
    }

    private void OnAdError(string errorMessage)
    {
        Debug.LogError($"广告播放失败: {errorMessage}");
        // 加载 AdError 预制体
        GameObject adErrorPrefab = Resources.Load<GameObject>("AdError");
        if (adErrorPrefab != null)
        {
            // 实例化 AdError
            GameObject adErrorInstance = Instantiate(adErrorPrefab, transform.position, Quaternion.identity);
            // 1 秒后销毁实例
            Destroy(adErrorInstance, 1.5f);
        }
        else
        {
            Debug.LogError("AdError 预制体未找到，请确保其位于 Resources 文件夹中。");
        }
    }
}
