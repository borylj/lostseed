using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class LoadDiaText : MonoBehaviour
{
    public Slider sl;//获得滑动条
    public Scrollbar scr;//获得滚动条
    public List<Text> TextList;//文本的合集数组
    public AVGMachine machine;//获得AVG框架的逻辑脚本

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scr.value = sl.value;//把滑动条的值赋给滚动条的值   
    }

    public void ShowText()
    {
        //显示文本
        if (machine.curLine > TextList.Count)
        {
            //如果剧情下标大于 文本数组的长度
            for(int i = 0; i < TextList.Count; i++)
            {
                TextList[i].text = machine.data.dataList[machine.curLine - i].Dialogtext;//把文本赋值
            }

        }
        else if (machine.curLine < TextList.Count)
        {
            for(int i = 0; i < machine.curLine; i++)
            {
                TextList[machine.curLine - i-1].text = machine.data.dataList[i].Dialogtext;//把文本赋值
            }
        }
    }

}
