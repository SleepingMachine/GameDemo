using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//UI界面管理器
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTF;//画布的变换组件
    private List<UIBase> uiList;//已加载界面的集合

    public void Awake()
    {
        Instance = this;
        //查找世界画布
        canvasTF = GameObject.Find("Canvas").transform;
        uiList = new List<UIBase>();
    }

    //显示UI界面
    public UIBase ShowUI<T>(string uiName) where T : UIBase 
    {
        UIBase ui = Find(uiName);
        if (ui == null) //uiList中没有该界面，需从Resource/UI文件夹中获取并添加到uiList
        {
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;
            obj.name = uiName;
            ui = obj.AddComponent<T>();
            uiList.Add(ui);
        }
        else
        {
            ui.Show();
        }

        return ui;
    }

    //隐藏UI界面
    public void HideUI(string uiName) 
    {
        UIBase ui = Find(uiName);
        if (ui != null) 
        {
            ui.Hide();
        }
    }

    //销毁指定UI界面
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null) 
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    //销毁所有UI界面
    public void CloseAllUI() 
    {
        for (int i = uiList.Count-1; i >= 0; i--) 
        {
            Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }

    //从uiList中查找界面脚本
    public UIBase Find(string uiName)
    { 
        for (int i = 0; i < uiList.Count; i++) 
        {
            if (uiList[i].name == uiName)
            { 
                return uiList[i];
            }
        }
        return null;
    }

    //获取某个界面
    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui != null) 
        {
            return ui.GetComponent<T>();
        }
        return null;
    }

    //创建敌人行动图标
    public GameObject CreateActionIcon() 
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//设置在父级的第一位
        return obj;
    }

    //创建敌人血条
    public GameObject CreateHpItem() 
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//设置在父级的第一位
        return obj;
    }

    //回合提示界面
    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTF) as GameObject;
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;

        Tween scale1 = obj.transform.Find("bg").DOScale(1, 0.2f);
        Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.2f);

        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate () 
            {
                if (callback != null) { callback(); }
            }
        );
        MonoBehaviour.Destroy(obj, 1);
    }
}
