using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadCGManager : MonoBehaviour
{
    public GameObject[] PoltPrefab;//剧情回放的数组
    public GameObject[] CGPrefab;//CG鉴赏的数组

    public AVGMachine machine;//调用AVG框架
    public static LoadCGManager loadCG;


    private void Awake()
    {
        loadCG = this;//制作单例
        PoltPrefab = GameObject.FindGameObjectsWithTag("Polt");//查找tag标签为Polt的物体
        CGPrefab = GameObject.FindGameObjectsWithTag("CG");//查找tag标签为CG的物体

    }
    private void Update()
    {
    }

    public void UpdateData()
    {
        //更新数据文件
        UpdateCG();//更新CG
        UpdatePolt();//更新剧情回放
    }



    void UpdateCG()
    {
        //更新CG的方法
        if (machine.StoryCount == 1 && machine.TextInfor == "Story01" && machine.curLine == 10)
        {
            //如果玩到第一个故事第一节10行 激活第一个CG预制体
            CGPrefab[0].GetComponent<CGPrefab>().isShow = true;//激活第一个CG鉴赏
        }
        if (machine.StoryCount == 1 && machine.TextInfor == "Story01" && machine.curLine == 12)
        {
            //如果玩到第一个故事第一节10行 激活第一个CG预制体
            CGPrefab[1].GetComponent<CGPrefab>().isShow = true;//激活第一个CG鉴赏
        }
        if (machine.StoryCount == 1 && machine.TextInfor == "Story01" && machine.curLine == 14)
        {
            //如果玩到第一个故事第一节10行 激活第一个CG预制体
            CGPrefab[2].GetComponent<CGPrefab>().isShow = true;//激活第一个CG鉴赏
        }

    }

    void UpdatePolt()
    {
        //更新剧情回放的方法
        if (machine.StoryCount == 1 && machine.TextInfor == "Story01" && machine.curLine == 10)
        {
            //如果玩到第一个故事第一节10行 激活第一个CG预制体
            PoltPrefab[0].GetComponent<CGPrefab>().isShow = true;//激活第一个CG鉴赏
        }
    }

    public void SaveData()
    {
        //存储游戏数据方法
        CGData cg = CreateSaveData();//创建存储文件

        BinaryFormatter bf = new BinaryFormatter();//实例化
        FileStream fileStream = File.Create(Application.persistentDataPath + "/CGData");//创建文件
        bf.Serialize(fileStream, cg);
        fileStream.Close();//关闭
    }
    public CGData CreateSaveData()
    {
        //创建存档文件数据
        CGData cg = new CGData();
        for (int i = 0; i < PoltPrefab.Length; i++)
        {
            cg.PoltBool[i] = PoltPrefab[i].GetComponent<CGPrefab>().isShow;//读取当前预制体的激活状态
        }
        for (int i = 0; i < CGPrefab.Length; i++)
        {
            cg.CGBool[i] = CGPrefab[i].GetComponent<CGPrefab>().isShow;//读取当前预制体的激活状态
        }
        return cg;
    }
    public void LoadData()
    {
        //读取数据的方法
        if (File.Exists(Application.persistentDataPath + "/CGData"))
        {
            //判断当前的路径是否有文件
            BinaryFormatter bf = new BinaryFormatter();//实例化
            FileStream fileStream = File.Open(Application.persistentDataPath + "/CGData", FileMode.Open);//打开数据文件
            CGData save = (CGData)bf.Deserialize(fileStream);//把二进制文件反序列化 然后再强制转换成Save形式
            fileStream.Close();//关闭
            SetGame(save);//吧反序列化出来的数据文件 传递给setgame
        }
        else
        {
            for (int i = 0; i < PoltPrefab.Length; i++)
            {
                PoltPrefab[i].GetComponent<CGPrefab>().isShow = false;//当前没有被激活
                PoltPrefab[i].GetComponent<CGPrefab>().ShowBlack();//调用 替换黑色图片的方法
            }
            for (int i = 0; i < CGPrefab.Length; i++)
            {
                CGPrefab[i].GetComponent<CGPrefab>().isShow = false;//当前没有被激活
                CGPrefab[i].GetComponent<CGPrefab>().ShowBlack();//调用 替换黑色图片的方法
            }


        }
    }
        public void SetGame(CGData cg)
    {
            for (int i = 0; i < PoltPrefab.Length; i++)
            {
                PoltPrefab[i].GetComponent<CGPrefab>().isShow = cg.PoltBool[i];//读取当前预制体的激活状态
                if (cg.PoltBool[i] == false)
                {
                    //如果等于false
                    PoltPrefab[i].GetComponent<CGPrefab>().ShowBlack();//调用 替换黑色图片的方法
                }
            }
        
        for (int i = 0; i < CGPrefab.Length; i++)
        {
            CGPrefab[i].GetComponent<CGPrefab>().isShow = cg.CGBool[i];//读取当前预制体的激活状态
            if (cg.CGBool[i] == false)
            {
                //如果等于false
                CGPrefab[i].GetComponent<CGPrefab>().ShowBlack();//调用 替换黑色图片的方法
            }
        }
    }
}



