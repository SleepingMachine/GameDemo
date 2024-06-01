using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;     //���ƶ�
    public List<string> usedCardList; //���ƶ� 

    //��ʼ��
    public void Init() 
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //������ʱ�ƶѲ�����ɫ�����ƴ��浽��ʱ�ƶ�
        List<string> tempCardList = new List<string>();
        tempCardList.AddRange(RoleManager.Instance.cardList);

        while (tempCardList.Count > 0) 
        {
            int tempIndex = Random.Range(0, tempCardList.Count);//����±�
            cardList.Add(tempCardList[tempIndex]);
            tempCardList.RemoveAt(tempIndex);
        }
    
    }

    //�жϳ��ƶ��Ƿ��п���
    public bool HasCard() 
    {
        return cardList.Count > 0;
    }

    //�鿨
    public string DrawCard() 
    {
        string id = cardList[cardList.Count - 1];
        cardList.RemoveAt(cardList.Count - 1);
        return id; 
    }
}
