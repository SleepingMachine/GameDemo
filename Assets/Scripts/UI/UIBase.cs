using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI�������
/// </summary>
public class UIBase : MonoBehaviour
{
    //ע���¼�
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }

    //��ʾUI����
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    //����UI����
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    //����UI����
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }
}

