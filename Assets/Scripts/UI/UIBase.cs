using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI界面基类
/// </summary>
public class UIBase : MonoBehaviour
{
    //注册事件
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }

    //显示UI界面
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    //隐藏UI界面
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    //销毁UI界面
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }
}

