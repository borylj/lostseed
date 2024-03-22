using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AVGData : ScriptableObject
{
    public List<DialogContent> contents;//用来储存我们的对话内容
    //  public List<string> Contents;//用来储存我们的对话内容

    //public List<bool> charaADisplay;//判断A角色是否要隐藏
    //public List<bool> charaBDisplay;//判断B角色是否要隐藏
    //public List<bool> charaCDisplay;//判断C角色是否要隐藏
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    [System.Serializable]//序列化
    public class DialogContent
    {
        //管理 对话内容和三个角色是否显示的布尔
        public string dialogText;//剧情内容

        public int charaADisplay;//A号角色
        public int charaBDisplay;//B号角色
        public int charaCDisplay;//C号角色
    }
}
