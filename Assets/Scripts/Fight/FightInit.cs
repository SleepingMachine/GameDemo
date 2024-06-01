using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

//ս����ʼ��
public class FightInit : FightUnit
{
    //��д��ʼ��
    public override void Init()
    {
        //����ս��BGM
        AudioManager.Instance.PlayBGM("battle");

        //��ʾս������
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //��ʼ��ս����ֵ
        FightManager.Instance.Init();

        //��ȡ�ؿ���Ϣ���ɶ�Ӧ�ؿ��ĵ���
        EnemyManager.Instance.LoadRes("10003");

        //��ʼ��ս������
        FightCardManager.Instance.Init();

        //��ʼ��һغ�
        FightManager.Instance.ChangeType(FightType.PlayerTurn);
    }

    //��дÿ֡����
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
