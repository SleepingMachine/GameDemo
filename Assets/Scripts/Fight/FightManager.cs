using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗管理器

//战斗状态类型枚举
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

    public FightUnit fightUnit; //战斗单元

    public int MaxHpCount;      //最大血量
    public int CurHpCount;      //当前血量
    public int MaxPowCount;     //最大能量
    public int CurPowCount;     //当前能量
    public int DefenseCount;    //防御值

    private void Awake()
    {
        Instance = this;
    }

    //初始化
    public void Init() 
    {
        MaxHpCount   = 20;
        CurHpCount   = 20;
        MaxPowCount  = 3;
        CurPowCount  = 3;
        DefenseCount = 0;
    }

    //切换战斗状态类型
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

    //玩家受击
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
