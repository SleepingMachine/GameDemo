using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//声音管理器
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;//播放声音文件

    private void Awake()
    {
        Instance = this;
    }

    //初始化
    public void Init() 
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
    }

    //播放BGM
    public void PlayBGM(string name, bool isloop = true) 
    {
        //载入BGM切片
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
        //播放设置
        bgmSource.clip = clip;
        bgmSource.loop = isloop;
        bgmSource.Play();
    }

    //播放音效
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}   
