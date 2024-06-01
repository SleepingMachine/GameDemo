using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//ս��
public class Fight_Loss : FightUnit
{
    //��д��ʼ��
    public override void Init()
    {
        Debug.Log("Game Over");
        FightManager.Instance.StopAllCoroutines();

        UIManager.Instance.ShowTip("Game Over", Color.red);
        //TODO��ʧ�ܽ���
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
    }

    //��дÿ֡����
    public override void OnUpdate()
    {

    }
}