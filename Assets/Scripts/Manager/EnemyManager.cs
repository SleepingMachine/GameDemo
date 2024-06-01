using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

//敌人管理器
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//储存战斗中的敌人

    /// <summary>
    /// 加载敌人资源
    /// </summary>
    /// <param name="id">关卡Id</param>
    public void LoadRes(string id) 
    {
        enemyList = new List<Enemy>();                 

        ///数据示例
        ///Id      Name    EnemyIds                Pos
        ///10003   3       10001 = 10002 = 10003   3,0,1 = 0,0,1 = -3,0,1

        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);

        //获取敌人的Id和站位
        string[] enemyIds = levelData["EnemyIds"].Split('=');
        string[] enemyPos = levelData["Pos"].Split('=');

        for (int i = 0; i < enemyIds.Length; i++) 
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');

            //将string转为int获取敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //使用Id获取单个敌人模型并摆放
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;
            obj.layer = LayerMask.NameToLayer("Enemy");

            Enemy enemy = obj.AddComponent<Enemy>();//添加敌人脚本
            
            enemy.Init(enemyData);//储存敌人信息

            //储存到列表
            enemyList.Add(enemy);

            obj.transform.position = new Vector3(x, y, z);
        }
             
    }

    //移除敌人
    public void DeleteEnemy(Enemy enemy) 
    {
        enemyList.Remove(enemy);

        //TODO:判断是否击杀了所有敌人
        if (enemyList.Count == 0 ) 
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }

    //执行所有活着的怪物的行动
    public IEnumerator DoAllEnemyAction() 
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //更新行为
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.PlayerTurn);
    }
}
