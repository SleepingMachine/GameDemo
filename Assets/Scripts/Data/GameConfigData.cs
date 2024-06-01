using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏配置引用表，为每个对象生成一个Txt配置表
public class GameConfigData
{
    private List<Dictionary<string, string>> dataDic;//储存配置表中的所有数据

    public GameConfigData(string str)
    { 
        dataDic = new List<Dictionary<string, string>>();

        //换行符切割
        string[] lines = str.Split('\n');
        //水平制表符切割
        string[] title = lines[0].Split('\t');

        //第二行是备注，故从第三行（index=2）开始遍历数据
        for (int i = 2; i < lines.Length; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] tempArr = lines[i].Trim().Split('\t');

            for (int j = 0; j < tempArr.Length; j++) 
            {
                dic.Add(title[j], tempArr[j]);
            }
            dataDic.Add(dic);
        }
    }

    public List<Dictionary<string, string>> GetLine() 
    {
        return dataDic;
    }

    public Dictionary<string, string> GetOneById(string id) 
    {
        for (int i = 0; i < dataDic.Count; i++) 
        {
            Dictionary<string, string> dic = dataDic[i];
            if (dic["Id"] == id) 
            {
                return dic;
            }
        }
        return null;
    }
}

