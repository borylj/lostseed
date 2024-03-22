using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowIMG : MonoBehaviour
{
    public AVGMachine machine;
    public Image WinDownIMG;//窗口图片
    private void Start()
    {
        machine = GetComponent<AVGMachine>();
    }
    void Update()
    {
        ShowIMG();
    }

    public void ShowIMG()
    {
        //判断什么时候显示图片
        if(machine.StoryCount == 1 && machine.TextInfor == "Story01" && machine.curLine >=30 && machine.curLine <=35)
        {//如果当前的故事id =1 并且当前的故事信息为story01 
            //并且当前的下标大于等于30 且小于等于35
            WinDownIMG.gameObject.SetActive(true);
            WinDownIMG.sprite = Resources.Load<Sprite>("WindowIMG/道具");
        }
        else if(machine.StoryCount == 1 && machine.TextInfor == "Story01" && machine.curLine >= 40 && machine.curLine <= 45)
        {//如果当前的故事id =1 并且当前的故事信息为story01 
            //并且当前的下标大于等于40 且小于等于45
            WinDownIMG.gameObject.SetActive(true);
            WinDownIMG.sprite = Resources.Load<Sprite>("WindowIMG/道具2");
        }
        else
        {
            WinDownIMG.gameObject.SetActive(false);//隐藏窗口图片
        }
    }
}
