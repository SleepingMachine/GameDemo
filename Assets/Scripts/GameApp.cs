using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏入口脚本
public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化配置表
        GameConfigManager.Instance.Init();

        //初始化音频管理器
        AudioManager.Instance.Init();
        //开始播放BGM
        //AudioManager.Instance.PlayBGM("BGM1");

        //显示Login UI
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //初始化用户信息
        RoleManager.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
