using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//��ʼ����
public class LoginUI : UIBase
{
    private void Awake()
    {
        //��ʼ��Ϸ
        Register("bg/StartBtn").onClick = onStartGameBtn;
        //������Ϸ
        Register("bg/ExitBtn").onClick = onExitGameBtn;

        AudioManager.Instance.PlayBGM("BGM1");
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData) 
    {
        //�ر�login����
        Close();

        //ս������
        FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onExitGameBtn(GameObject obj, PointerEventData pData)
    {
        //�ر���Ϸ
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
