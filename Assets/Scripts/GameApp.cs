using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϸ��ڽű�
public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ�����ñ�
        GameConfigManager.Instance.Init();

        //��ʼ����Ƶ������
        AudioManager.Instance.Init();
        //��ʼ����BGM
        //AudioManager.Instance.PlayBGM("BGM1");

        //��ʾLogin UI
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //��ʼ���û���Ϣ
        RoleManager.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
