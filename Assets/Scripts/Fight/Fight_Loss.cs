using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//战败
public class Fight_Loss : FightUnit
{
    //重写初始化
    public override void Init()
    {
        Debug.Log("Game Over");
        FightManager.Instance.StopAllCoroutines();

        UIManager.Instance.ShowTip("Game Over", Color.red);
        //TODO：失败界面
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
    }

    //重写每帧更新
    public override void OnUpdate()
    {

    }
}