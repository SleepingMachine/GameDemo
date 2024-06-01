using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��һغ�
public class Fight_PlayerTurn : FightUnit
{
    //��д��ʼ��
    public override void Init()
    {
        Debug.Log("��һغϿ�ʼ");

        //�����ƶ��ѿ������³�ʼ�����ƶ�
        if (FightCardManager.Instance.HasCard() == false) 
        {
            FightCardManager.Instance.Init();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();//���¿�������
        }

        FightManager.Instance.CurPowCount  = 3;
        FightManager.Instance.DefenseCount = 0;

        UIManager.Instance.ShowTip("��һغ�", Color.green, delegate () 
            {
                //����
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//��2����
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                Debug.Log("����");

                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();//���¿�������
            }
        );
    }

    //��дÿ֡����
    public override void OnUpdate()
    {

    }
}
