using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnterButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Text Des;//显示名称的文本
    public string BtnName;//按钮名称
    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标悬停
        Des.enabled = true;//显示文本
        Des.text = BtnName;//把按钮的名称赋值
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开
        Des.enabled = false;//关闭文本
    }

   
}
