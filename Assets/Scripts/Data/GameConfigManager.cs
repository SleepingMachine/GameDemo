using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    //���Ʊ�
    private GameConfigData cardData;     //���Ʊ�
    private GameConfigData cardTypeData; //��������
    private GameConfigData enemyData;    //���˱�
    private GameConfigData levelData;    //�ؿ���

    private TextAsset testAsset;

    //��ʼ��Txt�����ļ����������ڴ�
    public void Init() 
    {
        testAsset = Resources.Load<TextAsset>("Data/card");
        cardData  = new GameConfigData(testAsset.text);

        testAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(testAsset.text);

        testAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(testAsset.text);

        testAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(testAsset.text);
    }

    public List<Dictionary<string, string>> GetCardLine() 
    {
        return cardData.GetLine();
    }
    public List<Dictionary<string, string>> GetEnemyLine()
    {
        return enemyData.GetLine();
    }
    public List<Dictionary<string, string>> GetLevelLine()
    {
        return levelData.GetLine();
    }

    public Dictionary<string, string> GetCardById(string id) 
    {
        return cardData.GetOneById(id);
    }
    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    public Dictionary<string, string> GetCardTypeById(string id) 
    {
        return cardTypeData.GetOneById(id);
    }
}
