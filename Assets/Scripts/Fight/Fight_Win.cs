using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// §¿˚
public class Fight_Win : FightUnit
{
    public override void Init() 
    {
        Debug.Log("Win!");
        UIManager.Instance.ShowTip("Win", Color.green);
        //FightManager.Instance.StopAllCoroutines();
        //TODO£∫Ω·À„“≥√Ê

        UIManager.Instance.ShowUI<SelectCardUI>("SelectCardUI");
    }
}
