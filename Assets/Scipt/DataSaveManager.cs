using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaveManager : MonoBehaviour
{
    // 用来存储和读取文件的管理器
    public static DataSaveManager data;//当前脚本的单例

    public bool isSave;//判断当前是存储还是读取

    public AVGMachine machine;//调用AVG框架逻辑的脚本
    public List<Saveslot> slotList = new List<Saveslot>();//获取自身子集下面的存档预制体的数组

    public Canvas PoltCanvas;//剧情画布面板

    private void Awake()
    {
        data = this;//制作单例
    }

    void Start()
    {
        
    }

    public void InitSlot(Saveslot slot)
    {
        //把存档预制体 添加进入 数组
        slotList.Add(slot);
    }
    public void LoadFileData()
    {
        //加载已经拥有的文件数据
        for (int i = 0; i < slotList.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + slotList[i].path))
            {
                //查询是否有文件 如果有的话
                //判断当前的路径是否有文件
                BinaryFormatter bf = new BinaryFormatter();//实例化
                FileStream fileStream = File.Open(Application.persistentDataPath + slotList[i].path, FileMode.Open);//打开数据文件
                Save save = (Save)bf.Deserialize(fileStream);//把二进制文件反序列化 然后再强制转换成Save形式
                slotList[i].image.sprite = Resources.Load<Sprite>("BG/" + save.SaveImg);//获取储存预制体上面的图片
                slotList[i].Des.text = save.SaveDes;//读取文本然后赋值
                
                fileStream.Close();//关闭
                
            }
            else
            {
                //如果没有查询到文件
                slotList[i].image.sprite = Resources.Load<Sprite>("BG/" + "black");//获取储存预制体上面的图片
                slotList[i].Des.text = "没有数据";//
            }
        }
    }
   

    public void SaveGame(string path,Saveslot slot)
    {
        //储存数据文件
        SaveByBin(path,slot);//二进制储存方法
    }
    public void LoadGame(string path)
    {
        //读取游戏数据文件
        LoadByBin(path);
    }

    public void SaveByBin(string path,Saveslot slot)
    {
        //二进制储存数据的方法
        Save save = CreateSaveData(slot);
        BinaryFormatter bf = new BinaryFormatter();//实例化
        FileStream fileStream = File.Create(Application.persistentDataPath+path);//创建文件
        bf.Serialize(fileStream, save);
        fileStream.Close();//关闭

    }
    public Save CreateSaveData(Saveslot slot)
    {
        //创建存储文件信息
        Save save = new Save();//实例化一下 序列化的脚本
        save.CurLine = machine.curLine;//把文本下标保存在save序列化脚本里面
        save.StoryID = machine.StoryCount;
        save.TextInfor = machine.TextInfor;

        save.Alpha = machine.ui.BG2Alpha;//把背景图片2的透明度保存在序列化脚本里面
        save.BG = machine.ui.BG.sprite.name;//获得 一号背景图片里面使用的图片名称
        save.BG2 = machine.ui.BG2.sprite.name;//获得 二号背景图片里面使用的图片名称

        save.SaveImg = slot.image.sprite.name;//获得存储预制体上的图片
        save.SaveDes = slot.Des.text;//获得存储预制体上的文本
        return save;//返回存储数据文件

    }
    public void LoadByBin(string path)
    {
        //读取二进制游戏文件
        if(File.Exists(Application.persistentDataPath + path))
        {
            //判断当前的路径是否有文件
            BinaryFormatter bf = new BinaryFormatter();//实例化
            FileStream fileStream = File.Open(Application.persistentDataPath + path, FileMode.Open);//打开数据文件
            Save save = (Save)bf.Deserialize(fileStream);//把二进制文件反序列化 然后再强制转换成Save形式
            fileStream.Close();//关闭
            SetGame(save);//吧反序列化出来的数据文件 传递给setgame
        }
    }
    public void SetGame(Save save)
    {
        //把读取出来的游戏数据 再赋值回去
        machine.curLine = save.CurLine;//把下标赋值
        machine.StoryCount = save.StoryID;
        machine.TextInfor = save.TextInfor;
        machine.LoadUpdateBG(save.BG, save.BG2, save.Alpha);//传递背景1 背景2 和背景2的透明值

        machine.GoToState(AVGMachine.STATE.TYPING);//把当前的状态切换成typing 打字状态
        PoltCanvas.enabled = true;//显示剧情面板
        gameObject.SetActive(false);//关闭自身面板
    }


}
