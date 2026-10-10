// 以下是数据模型类
using System;

[Serializable]
public class AdParamsData
{
    public LogExtra log_extra;
}

[Serializable]
public class LogExtra
{
    public string clickid;
}

[Serializable]
public class ConversionData
{
    public string event_type;
    public Context context;
    public long timestamp;
}

[Serializable]
public class Context
{
    public Ad ad;
}

[Serializable]
public class Ad
{
    public string callback;
}

[Serializable]
public class SystemInfoData
{
    public string system;
    public string platform;
    public string brand;
    public string model;
    public string version;
    public string appName;
    public string SDKVersion;
}

[Serializable]
public class LoginResult
{
    public string code;
    public string anonymousCode;
}

[Serializable]
public class UserInfo
{
    public string nickName;
}

[Serializable]
public class OpenIdResponse
{
    public OpenIdData d;
}

[Serializable]
public class OpenIdData
{
    public OpenIdUser user;
}

[Serializable]
public class OpenIdUser
{
    public string openid;
}

/// <summary>
/// 插屏广告错误码
/// </summary>
public enum TTErrorCode
{
    /// <summary>
    /// 小程序启动一定时间内不允许展示插屏广告
    /// </summary>
    FrequencyLimit_StartUp = 2001,

    /// <summary>
    /// 距离上次播放时间间隔不足，不允许展示插屏广告
    /// </summary>
    FrequencyLimit_Interval = 2002,

    /// <summary>
    /// 当前正在播放激励视频广告或者插屏广告，不允许再次展示插屏广告
    /// </summary>
    FrequencyLimit_Concurrent = 2003,

    /// <summary>
    /// 广告渲染失败（非开发者异常情况）
    /// </summary>
    RenderFailure = 2004,

    /// <summary>
    /// 插屏广告实例不允许跨页面调用
    /// </summary>
    InvalidInstanceUsage = 2005
}