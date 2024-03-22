using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGround : MonoBehaviour
{
    // 管理按钮 并且判断 哪个面板
    public List<TabButtom> tabButtoms = new List<TabButtom>();//把按钮添加进入数组里面
    public TabButtom selectTab;//用来存储当前点击的是哪个按钮
    public List<GameObject> objectPanel;

    public TabButtom tab1;





    void Start()
    {
        tab1.background.sprite = tab1.tabActive;//修改图片成对应的激活图片
        OnTabSelected(tab1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTabSelected(TabButtom buttom)
    {
        //判断当前点击了哪个按钮，没有点击的按钮就切换成未激活的图片
        selectTab = buttom;//把点击的按钮 传递过去
        ResetTab();//调用 重置所有按钮图片的方法
        buttom.background.sprite = buttom.tabActive;//当前被点击的按钮的图片 切换成对应的激活的图片
        int index = buttom.transform.GetSiblingIndex();//获得当前物体在当前位置的排行下标 就是自己排行第几位 0开头
        for (int i =0; i < objectPanel.Count; i++)
        {
            if(i==index)//如果当前的i等于这个下标
            {
                objectPanel[i].SetActive(true);

            }
            else
            {
                objectPanel[i].SetActive(false);            }



        }

    }

    public void ResetTab()
    {
        //重置所有按钮图片
        foreach(TabButtom buttom in tabButtoms)
        {
            //循环便利 按钮数组
            if (selectTab!=null && buttom == selectTab)
            {
                continue;//跳过本次循环
            }
            buttom.background.sprite = buttom.tabldle;

        }
    }


}
