using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//开始界面
public class LoginUI : UIBase
{
    private void Awake()
    {
        //开始游戏
        Register("bg/StartBtn").onClick = onStartGameBtn;
        //结束游戏
        Register("bg/ExitBtn").onClick = onExitGameBtn;

        AudioManager.Instance.PlayBGM("BGM1");
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData) 
    {
        //关闭login界面
        Close();

        //战斗启动
        FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onExitGameBtn(GameObject obj, PointerEventData pData)
    {
        //关闭游戏
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
