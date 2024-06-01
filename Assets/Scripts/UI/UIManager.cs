using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//UI���������
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTF;//�����ı任���
    private List<UIBase> uiList;//�Ѽ��ؽ���ļ���

    public void Awake()
    {
        Instance = this;
        //�������续��
        canvasTF = GameObject.Find("Canvas").transform;
        uiList = new List<UIBase>();
    }

    //��ʾUI����
    public UIBase ShowUI<T>(string uiName) where T : UIBase 
    {
        UIBase ui = Find(uiName);
        if (ui == null) //uiList��û�иý��棬���Resource/UI�ļ����л�ȡ����ӵ�uiList
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

    //����UI����
    public void HideUI(string uiName) 
    {
        UIBase ui = Find(uiName);
        if (ui != null) 
        {
            ui.Hide();
        }
    }

    //����ָ��UI����
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null) 
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    //��������UI����
    public void CloseAllUI() 
    {
        for (int i = uiList.Count-1; i >= 0; i--) 
        {
            Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }

    //��uiList�в��ҽ���ű�
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

    //��ȡĳ������
    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui != null) 
        {
            return ui.GetComponent<T>();
        }
        return null;
    }

    //���������ж�ͼ��
    public GameObject CreateActionIcon() 
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//�����ڸ����ĵ�һλ
        return obj;
    }

    //��������Ѫ��
    public GameObject CreateHpItem() 
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//�����ڸ����ĵ�һλ
        return obj;
    }

    //�غ���ʾ����
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
