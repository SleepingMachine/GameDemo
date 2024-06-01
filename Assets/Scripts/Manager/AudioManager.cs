using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����������
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;//���������ļ�

    private void Awake()
    {
        Instance = this;
    }

    //��ʼ��
    public void Init() 
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
    }

    //����BGM
    public void PlayBGM(string name, bool isloop = true) 
    {
        //����BGM��Ƭ
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
        //��������
        bgmSource.clip = clip;
        bgmSource.loop = isloop;
        bgmSource.Play();
    }

    //������Ч
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}   
