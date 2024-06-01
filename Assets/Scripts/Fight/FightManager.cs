using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս��������

//ս��״̬����ö��
public enum FightType 
{
    None,
    Init,
    PlayerTurn,
    EnemyTurn,
    Win,
    Loss
}
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit; //ս����Ԫ

    public int MaxHpCount;      //���Ѫ��
    public int CurHpCount;      //��ǰѪ��
    public int MaxPowCount;     //�������
    public int CurPowCount;     //��ǰ����
    public int DefenseCount;    //����ֵ

    private void Awake()
    {
        Instance = this;
    }

    //��ʼ��
    public void Init() 
    {
        MaxHpCount   = 20;
        CurHpCount   = 20;
        MaxPowCount  = 3;
        CurPowCount  = 3;
        DefenseCount = 0;
    }

    //�л�ս��״̬����
    public void ChangeType(FightType type) 
    {
        switch (type)
        {
            case FightType.None:
                break; 
            case FightType.Init:
                fightUnit = new FightInit(); 
                break;
            case FightType.PlayerTurn:
                fightUnit = new Fight_PlayerTurn();
                break; 
            case FightType.EnemyTurn:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fightUnit = new Fight_Loss();
                break;
        }
        fightUnit.Init();
    }

    //����ܻ�
    public void GetPlayerHit(int hit) 
    {
        if (DefenseCount >= hit)
        {
            DefenseCount -= hit;
        }
        else 
        {
            hit -= DefenseCount;
            DefenseCount = 0;
            CurHpCount -= hit;

            if (CurHpCount <= 0)
            {
                CurHpCount = 0;
                ChangeType(FightType.Loss);
            }
        }

        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }

    private void Update()
    {
        if (fightUnit != null) 
        {
            fightUnit.OnUpdate();
        }
    }
}
