using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�û���Ϣ������������ӵ�еĿ��ơ����ҵ���Ϣ
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//����ӵ�п��Ƶ�Id


    public void Init() 
    {
        cardList = new List<string>();
        //��ʼ��4�Ź�������4�ŷ�������2��Ч����
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
