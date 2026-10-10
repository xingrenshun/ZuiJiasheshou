using System.Collections;
using UnityEngine;

/// <summary>
/// 游戏暂停后延时3秒展示插屏广告
/// </summary>
public class DelayShowInterAd : MonoBehaviour
{
    public float delayTime = 3f;
    private Coroutine delay;

    private void OnEnable()
    {
        if (delay != null)
        {
            StopCoroutine(delay);
        }
        Debug.Log($"游戏结束，{delayTime}秒后展示插屏广告");
        delay = StartCoroutine(ShowInterAd());
    }

    private void OnDisable()
    {
        if (delay != null)
        {
            Debug.Log("游戏继续，取消插屏广告展示");
            StopCoroutine(delay);
            delay = null;
        }
    }

    private IEnumerator ShowInterAd()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        Debug.Log("计时结束，展示插屏广告");
        AdManager.Instance.ShowInterstitialAd();
    }
}
