using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

//���˹�����
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//����ս���еĵ���

    /// <summary>
    /// ���ص�����Դ
    /// </summary>
    /// <param name="id">�ؿ�Id</param>
    public void LoadRes(string id) 
    {
        enemyList = new List<Enemy>();                 

        ///����ʾ��
        ///Id      Name    EnemyIds                Pos
        ///10003   3       10001 = 10002 = 10003   3,0,1 = 0,0,1 = -3,0,1

        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);

        //��ȡ���˵�Id��վλ
        string[] enemyIds = levelData["EnemyIds"].Split('=');
        string[] enemyPos = levelData["Pos"].Split('=');

        for (int i = 0; i < enemyIds.Length; i++) 
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');

            //��stringתΪint��ȡ����λ��
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //ʹ��Id��ȡ��������ģ�Ͳ��ڷ�
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;
            obj.layer = LayerMask.NameToLayer("Enemy");

            Enemy enemy = obj.AddComponent<Enemy>();//��ӵ��˽ű�
            
            enemy.Init(enemyData);//���������Ϣ

            //���浽�б�
            enemyList.Add(enemy);

            obj.transform.position = new Vector3(x, y, z);
        }
             
    }

    //�Ƴ�����
    public void DeleteEnemy(Enemy enemy) 
    {
        enemyList.Remove(enemy);

        //TODO:�ж��Ƿ��ɱ�����е���
        if (enemyList.Count == 0 ) 
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }

    //ִ�����л��ŵĹ�����ж�
    public IEnumerator DoAllEnemyAction() 
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //������Ϊ
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.PlayerTurn);
    }
}
