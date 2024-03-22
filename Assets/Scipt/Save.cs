using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//序列化脚本

public class Save 
{
    public int CurLine;//剧情文本下标

    public float Alpha;//记录背景图片2的透明度

    public string TextInfor;//当前使用的剧情信息 Story01
    public int StoryID;//当前正在使用的剧情id信息 如果当前正在使用Story01 那么他的id就为1
    public string BG;//记录1号背景图片里面的图片名称
    public string BG2;//记录2号背景的图片名称

    public string SaveImg;//储存界面的图片
    public string SaveDes;//储存界面的文本

}
