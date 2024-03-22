using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SettingPanel : MonoBehaviour
{
    public AudioSource[] BGMusicList;//背景音效
    public AudioSource[] EffectMusicList;//播放效果音效的组件
    public AudioSource[] RoleMusicValue;//播放人物语音的组件

    public CanvasGroup dialog;//对话框的透明度

    public float SpeedText = 7;//文本显示速度
    public float ASpeedText = 10;//文本自动跳转下一句的速度
    public float WinAlpha = 0.9F;//对话框文本的透明度
    public float zongMusic = 1f;//总音量
    public float BGMusic = 1f;//背景音效
    public float RoleMusic = 1f;//人物说话的音量
    public float EffectMusic = 1f;//效果音像的声音

    public List<Slider> SliderList;//获得滚动条合集

    public AVGMachine machine;//调用一下AVG游戏逻辑脚本

    public List<Sprite> imglist;//图片
    public Image FullScreenIMG;//全屏图片按钮
    public Image WindowIMG;//窗口模式图片按钮

    public void Start()
    {
        FullScreen();
    }

    private void Update()
    {
        UpdateValue();
        
                                      //设置自动状态下的打字机速度
        //machine.SettingBGMusic();//更新 AVGMachine 脚本里面的BGMusic 参数的数值
        UpdateSlider();//把滚动条里的参数赋值给当前脚本上面的参数
    }
    public void UpdateValue()
    {
        //更新数据
        for (int i = 0; i < BGMusicList.Length; i++)
        {
            //循环便利 背景音效的数组
            BGMusicList[i].volume = BGMusic * zongMusic;//背景音效的大小等于我们设置的背景音量大小乘以总音效
        }
        for (int i = 0; i < EffectMusicList.Length; i++)
        {
            //循环便利 背景音效的数组
            EffectMusicList[i].volume = EffectMusic * zongMusic;//效果音效的大小等于我们设置的背景音量大小乘以总音效
        }
        for (int i = 0; i < RoleMusicValue.Length; i++)
        {
            //循环便利 背景音效的数组
            RoleMusicValue[i].volume = RoleMusic * zongMusic;//人物语音音效的大小等于我们设置的背景音量大小乘以总音效
        }

        dialog.alpha = WinAlpha;//把对话框文本的透明度赋值

        machine.TextSpeed = SpeedText;//把速度赋值过去
    }
    public void UpdateSlider()
    {
        //把滚动条里面的参数赋值给当前脚本上面的 参数
        SpeedText = SliderList[0].value;//赋值给文字速度
        ASpeedText = SliderList[1].value;//赋值给自动速度
        WinAlpha = SliderList[2].value;
        zongMusic = SliderList[3].value;
        BGMusic = SliderList[4].value;
        EffectMusic = SliderList[5].value;
        RoleMusic = SliderList[6].value;
    }
    public void Awake()
    {
       //判断当前是否有设置文件 如果有就读取设置文件里面的参数
       if(File.Exists(Application.persistentDataPath +"/Setting"))//判断当前文件路径是否有setting的文件
        {
            //如果有就执行读取文件，并解析文件里面的数据内容
            BinaryFormatter bf = new BinaryFormatter();//实例化
            FileStream fileStream = File.Open(Application.persistentDataPath + "/Setting", FileMode.Open);//打开文件
            SettingData save = (SettingData)bf.Deserialize(fileStream);//反序列化数据
            fileStream.Close();//关闭

            SetGameData(save);

        }
        else
        {
            //如果没有文件
            //SaveValue 储存文件的方法
            SaveValue();

        }
        SetValue();//把数据赋值给滚动条的Value值
    }

    public void SetValue()//把数据赋值给滚动条的Value值
    {
        SliderList[0].value = SpeedText;//把文字打印的速度 赋值给第一个滚动条
        SliderList[1].value = ASpeedText;
        SliderList[2].value = WinAlpha;
        SliderList[3].value = zongMusic;
        SliderList[4].value = BGMusic;
        SliderList[5].value = EffectMusic;
        SliderList[6].value = RoleMusic;

    }
    public void SaveValue()
    {
        //存储数据的方法
        SettingData save = CreateSaveGo();//接收存档数据信息
        BinaryFormatter bf = new BinaryFormatter();//实例化
        FileStream fileStream = File.Create(Application.persistentDataPath + "/Setting");//创建一个名为setting的文件
        bf.Serialize(fileStream, save);//序列化
        fileStream.Close();//关闭
    }
    public SettingData CreateSaveGo()
    {
        //创建存档数据信息
        SettingData save = new SettingData();//实例化一个 储存数据
        save.SpeedText = SpeedText;//把当前要储存的数据赋值给要序列化的脚本
        save.ASpeedText = ASpeedText;//把当前要储存的数据赋值给要序列化的脚本
        save.WinAlpha = WinAlpha;//把当前要储存的数据赋值给要序列化的脚本
        save.zongMusic = zongMusic;//把当前要储存的数据赋值给要序列化的脚本
        save.BGMusic= BGMusic;//把当前要储存的数据赋值给要序列化的脚本
        save.EffectMusic = EffectMusic;//把当前要储存的数据赋值给要序列化的脚本
        save.RoleMusic = RoleMusic;//把当前要储存的数据赋值给要序列化的脚本
        return save;

    }
    public void SetGameData(SettingData save)
    {
        SpeedText = save.SpeedText;//把当前要储存的数据赋值给要序列化的脚本
        ASpeedText = save.ASpeedText;//把当前要储存的数据赋值给要序列化的脚本
        WinAlpha = save.WinAlpha;//把当前要储存的数据赋值给要序列化的脚本
        zongMusic = save.zongMusic;//把当前要储存的数据赋值给要序列化的脚本
        BGMusic = save.BGMusic;//把当前要储存的数据赋值给要序列化的脚本
        EffectMusic = save.EffectMusic;//把当前要储存的数据赋值给要序列化的脚本
        RoleMusic = save.RoleMusic;//把当前要储存的数据赋值给要序列化的脚本
        UpdateValue();//音频数据更新
    }
    public void AgainSetValue(int a1,int a2,float a3,float a4,float a5,float a6,float a7)
    {
        //重置所有选项的按钮
        SliderList[0].value = a1;
        SliderList[1].value = a2;
        SliderList[2].value = a3;
        SliderList[3].value = a4;
        SliderList[4].value = a5;
        SliderList[5].value = a6;
        SliderList[6].value = a7;
    }
    public void AgainMusic()
    {
        //重置音量
        AgainSetValue(7, 10, 0.9f, 0.8f, 1, 1, 1);
    }
    
    public void Window()
    {
        //窗口化模式
        ResImg();
        Screen.SetResolution(1920, 1080, false);//为false就窗口化
        WindowIMG.sprite = imglist[3];//把数组里面的第4章图片赋值,激活后的窗口按钮
    }
    public void FullScreen() 
    {
        //窗口化模式
        ResImg();
        Screen.SetResolution(1920, 1080, true);//全屏
        FullScreenIMG.sprite = imglist[2];//把数组里面的第三章图片赋值
    }

    public void ResImg()
    {
        //取消按钮的激活状态
        FullScreenIMG.sprite = imglist[0];//未激活的窗口化按钮
        WindowIMG.sprite = imglist[1];//未激活的窗口化按钮
    }
}
