using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class Saveslot : MonoBehaviour,IPointerDownHandler
{
    public int ID;//当前存档的ID
    public Image image;//获得图片

    public Text IDdes;//当前的ID 用来显示当前存档的ID
    public Text Des;//当前储存的剧情 文本

    public string path;//文件地址
    public DataSaveManager data;//

    void Awake()
    {
        path = "/save" + ID + ".save";//设置存储文件的储存地址
        transform.GetComponentInParent<DataSaveManager>().InitSlot(this);//获得自身父级的脚本 把自身传递进去
    }

    // Start is called before the first frame update
    void Start()
    {
        IDdes.text = "存档" + ID;//打印输出当前存储存档的ID值
        data = DataSaveManager.data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadFile()
    {
        //加载文件 读取文件的方法
        data.LoadGame(path);//调用读取数据的方法 把地址传递过去
    }
    public void SaveFile()
    {
        //存储数据文件
        image.sprite = UIManager.ui.machine.SaveBG;//储存预制体身上的图片
        Des.text = UIManager.ui.machine.data.dataList[UIManager.ui.machine.curLine].Dialogtext;//读取 剧情文本然后赋值给存储预制体里的介绍文本
        data.SaveGame(path,this);//调用存储数据的方法
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //当我们鼠标点击了
       
        if (data.isSave)
        {
            //如果当前处于储存状态的话
            if(File.Exists(Application.persistentDataPath + path))
            {
                //判断当前地址是否有文件 如果有
                //就调用一个确认面板 判断是否 要覆盖存储
                UIManager.ui.isSave(this);//调用存储 把自身传递进去
            }
            else
            {
                SaveFile();//储存文件的方法
            }
        }
        else
        {
            //如果当前处于读取状态的话
            if(File.Exists(Application.persistentDataPath + path))//判断当前文件是否存在
            {
                UIManager.ui.isLoad(this);//调用读取文件的方法
            }
        }
    }
}
