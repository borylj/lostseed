using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowCGPanels : MonoBehaviour,IPointerDownHandler
{
    public CGPrefab cg;//预制体信息
    public int Index;//图片下标

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Index < cg.ImgList.Count)
        { //图片下标必须小于cg预制体里面的图片长度
          //把自身传递过去
            transform.GetComponentInChildren<Image>().sprite = cg.ImgList[Index];//读取预制体里面的图片数组
            Index++;
        }
        else
        {
            //如果为false 当前的预制体是剧情回放
            Index = 0;
            transform.gameObject.SetActive(false);//隐藏自身
            
        }
    }
}
