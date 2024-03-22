using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AVGMainAnim : MonoBehaviour
{
    public AVGMachine machine;//获得AVG框架
    public GameObject AnimCanvas;//获得标题的动画画布
    //===================================================
    public Image PoltBG;//当前正在看的背景
    public Image bg;//在Mask下面的BG
    public Image bgCanying;//在Mask下面的BG残影
    public Image Role;//角色的图片
    public Text MainText;//用来显示标题的文本

    public Canvas cans;//获得一个剧情画布
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayAnim();//调用方法
    }

    public void PlayAnim()
    {
        //判断是否达到播放动画的条件
        if (machine.curLine == 145 && machine.StoryCount == 1)
        {
            //如果当前的下标等于145（文档里是147行）并且当前的故事剧本为1号也就是Story 01
            //把第一个故事Story01切换成Story02第二个故事
            machine.isDown = false;//当前不可以跳转到下一段剧情
            machine.TextInfor = "Story02";//当前的信息为二号支线2号故事 Story02
            bgCanying.sprite = PoltBG.sprite = bg.sprite = Resources.Load<Sprite>("BG/zts");//也就是 读取图片然后赋值给mask下面的图片 然后再赋值给这个剧情背景图片
            AnimCanvas.SetActive(true);//显示剧情面板
            MainText.text = "第一章：二号剧情";//设置标题
        }
    }
}
