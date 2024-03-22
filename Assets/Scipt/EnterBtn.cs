using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterBtn : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler
{
    public AudioSource EnterMusic;//悬停音效
    public AudioSource DownMusic;//悬停音效

    private void Start()
    {
        EnterMusic = GameObject.FindGameObjectWithTag("EnterMusic").GetComponent<AudioSource>();//获得标签为EnterMusic的物体身上的音频播放组件
        DownMusic = GameObject.FindGameObjectWithTag("DownMusic").GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //当鼠标点击的时候
        DownMusic.Play();//播放点击的音效
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //当鼠标悬停的时候
        EnterMusic.Play();//播放悬停的音效
    }
}
