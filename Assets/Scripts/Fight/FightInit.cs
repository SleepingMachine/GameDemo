using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

//战斗初始化
public class FightInit : FightUnit
{
    //重写初始化
    public override void Init()
    {
        //切入战斗BGM
        AudioManager.Instance.PlayBGM("battle");

        //显示战斗界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //初始化战斗数值
        FightManager.Instance.Init();

        //读取关卡信息生成对应关卡的敌人
        EnemyManager.Instance.LoadRes("10003");

        //初始化战斗卡牌
        FightCardManager.Instance.Init();

        //开始玩家回合
        FightManager.Instance.ChangeType(FightType.PlayerTurn);
    }

    //重写每帧更新
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
