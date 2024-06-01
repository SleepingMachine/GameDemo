using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌方回合
public class Fight_EnemyTurn : FightUnit
{
    //重写初始化
    public override void Init()
    {
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();//删除所有卡牌
        //显示敌人回合
        UIManager.Instance.ShowTip("敌方回合", Color.red, delegate ()
        {
            Debug.Log("执行敌人行动");
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        }
        );
    }

    //重写每帧更新
    public override void OnUpdate()
    {

    }
}
