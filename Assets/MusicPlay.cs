using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public List<AudioClip> AudioList;//音频播放的数组
    public AudioSource music;// 音频播放的组件
    

    public void PlayMusic(int id)
    {
        //播放音频
        
        music.clip = AudioList[id];//把要播放的音频传递给音频播放的组件
        music.Play();//播放音频
    }

    public void StopMusic()
    {
        //停止播放音频
        music.Stop();//停止播放
    }
}
