using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家回合
public class Fight_PlayerTurn : FightUnit
{
    //重写初始化
    public override void Init()
    {
        Debug.Log("玩家回合开始");

        //若抽牌堆已空则重新初始化抽牌堆
        if (FightCardManager.Instance.HasCard() == false) 
        {
            FightCardManager.Instance.Init();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();//更新卡牌数量
        }

        FightManager.Instance.CurPowCount  = 3;
        FightManager.Instance.DefenseCount = 0;

        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate () 
            {
                //抽牌
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//抽2张牌
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                Debug.Log("抽牌");

                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();//更新卡牌数量
            }
        );
    }

    //重写每帧更新
    public override void OnUpdate()
    {

    }
}
