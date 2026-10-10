using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Share : MonoBehaviour
{
    // 分享按钮点击事件
    public void OnShareClick()
    {
        if (AdManager.Instance != null)
        {
            AdManager.Instance.ShareGame((isSuccess) =>
            {
                if (isSuccess)
                {
                    Debug.Log("分享成功");
                }
                else
                {
                    Debug.Log("分享失败或取消");
                }
            });
        }
    }
}
