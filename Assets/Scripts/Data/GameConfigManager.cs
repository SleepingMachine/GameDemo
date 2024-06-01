using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    //卡牌表
    private GameConfigData cardData;     //卡牌表
    private GameConfigData cardTypeData; //卡牌类型
    private GameConfigData enemyData;    //敌人表
    private GameConfigData levelData;    //关卡表

    private TextAsset testAsset;

    //初始化Txt配置文件并保存至内存
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
