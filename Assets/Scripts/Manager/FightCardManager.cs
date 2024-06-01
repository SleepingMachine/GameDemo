using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;     //抽牌堆
    public List<string> usedCardList; //弃牌堆 

    //初始化
    public void Init() 
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //定义临时牌堆并将角色的手牌储存到临时牌堆
        List<string> tempCardList = new List<string>();
        tempCardList.AddRange(RoleManager.Instance.cardList);

        while (tempCardList.Count > 0) 
        {
            int tempIndex = Random.Range(0, tempCardList.Count);//随机下标
            cardList.Add(tempCardList[tempIndex]);
            tempCardList.RemoveAt(tempIndex);
        }
    
    }

    //判断抽牌堆是否有卡牌
    public bool HasCard() 
    {
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard() 
    {
        string id = cardList[cardList.Count - 1];
        cardList.RemoveAt(cardList.Count - 1);
        return id; 
    }
}
