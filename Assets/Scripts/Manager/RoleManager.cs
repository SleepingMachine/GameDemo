using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用户信息管理器，管理拥有的卡牌、货币等信息
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//储存拥有卡牌的Id


    public void Init() 
    {
        cardList = new List<string>();
        //初始化4张攻击卡、4张防御卡和2张效果卡
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");

        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1000");

        cardList.Add("1002");
        cardList.Add("1002");
    }
}
