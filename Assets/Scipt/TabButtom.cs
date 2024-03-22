using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class TabButtom : MonoBehaviour,IPointerDownHandler
{
    public Sprite tabldle;//未激活状态 待机图片
    public Sprite tabActive;//激活状态 激活图片
    public TabGround tabGround;//获得控制器按钮和面板的脚本
    public Image background;//获得自身上的IMAGE组件

    private void Awake()
    {
        tabGround = transform.GetComponentInParent<TabGround>();//获得父级身上的脚本
        background = GetComponent<Image>();
    }

     

    void Start()
    {
        tabGround.tabButtoms.Add(this);//把自身添加进入数组当中
     
        

    }

    public void OnPointerDown(PointerEventData evenData) //当我们鼠标点击了UI图片
    {
        //执行当前UI的逻辑 比如当前是1就显示1号面板 如果是2就显示2号面板
        tabGround.OnTabSelected(this);//把自身传递进去
    }



    // Update is called once per frame
    void Update()
    {
       


    }
}
