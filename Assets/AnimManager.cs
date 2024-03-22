using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimManager : MonoBehaviour
{

    public AVGMachine machine;//引用AVG框架  
    public Animator anim;//动画控制器
    public Canvas canvas;//剧情的画布
    public GameObject BlackBG;//黑色背景图片
 public void ChangeBG()
    {
        //更换背景的图片
        machine.curLine += 1;
        machine.UserClicked();//加载下一句剧情
        machine.justEnter = true;//设置为true
        machine.ui.SetText(null);//清除脏数据
    }

public void QuitBlackBG()
    {
        //退出黑屏状态 关闭黑屏
        machine.isDown = true;//可以点击下一句
        transform.gameObject.SetActive(false);//隐藏自身
    }
 public void ShowAnimPart() {
        //
        switch (machine.StoryCount)
        {
            case 1:
                //循环便利一下AVG框架逻辑里面的剧情id 判断当前 正在播放 哪个 剧情线路
                machine.data = Resources.Load<Story01>("MyStory/Story02");//把当前正在播放的故事线路切换成Story02 故事线2
                machine.StoryCount = 2;//当前的故事id 为2
                machine.curLine = 0;//下标为0
                break;
        }
 

  }

public void EnterDown()
    {
        //如果当标题动画播放完成之后 点击Down 按钮
        anim = transform.GetComponent<Animator>();
        anim.SetTrigger("Quit");//调用一下Quit
    }
public void ChangePolt()
    {
        //更换剧情故事 更换Story
        canvas.enabled = true;//显示剧情界面
        BlackBG.SetActive(true);//显示黑色的背景 显示的时候自动播放渐变消失的动画
        transform.gameObject.SetActive(false);//隐藏自身
    }
public void UpdateBG()
    {
        //更新剧情里面的背景图片
        machine.UserClicked();//更新 把当前的状态切换成typing状态
        machine.ui.SetText(null);//清空脏数据
    }
public void StartGame()
    {
        //开始游戏
        UIManager.ui.StartGame();//调用开始游戏方法
    }
public void StartAVG()
    {
        //avg开始运行
        UIManager.ui.machine.StartAVG();
    }

public void HideBlack()
    {
        //关闭黑屏
        gameObject.SetActive(false);
    }
public void HideMainBtn()
    {
        //关闭菜单按钮
        UIManager.ui.UIBtn.SetActive(false);//关闭菜单界面的按钮
    }
}
