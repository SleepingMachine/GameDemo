using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʤ��
public class Fight_Win : FightUnit
{
    public override void Init() 
    {
        Debug.Log("Win!");
        UIManager.Instance.ShowTip("Win", Color.green);
        //FightManager.Instance.StopAllCoroutines();
        //TODO������ҳ��

        UIManager.Instance.ShowUI<SelectCardUI>("SelectCardUI");
    }
}
