using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�з��غ�
public class Fight_EnemyTurn : FightUnit
{
    //��д��ʼ��
    public override void Init()
    {
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();//ɾ�����п���
        //��ʾ���˻غ�
        UIManager.Instance.ShowTip("�з��غ�", Color.red, delegate ()
        {
            Debug.Log("ִ�е����ж�");
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        }
        );
    }

    //��дÿ֡����
    public override void OnUpdate()
    {

    }
}
