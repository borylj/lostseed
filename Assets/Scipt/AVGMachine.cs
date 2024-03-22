using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class AVGMachine : MonoBehaviour
{
    //public List<string> Contents;//用来储存我们的对话内容
    public int curLine;//对话内容的下标
    public UIPanel ui;
    public Story01 data;
    public AVGAssetConfig assetConfig;//切换图片的逻辑

    public GameObject BlackObject;//黑屏物体

    public string targetString;//用来储存剧情文本的字符串
    public float timerValue;//文本的下标
    public float TextSpeed;//打字机显示速度

    public float ASpeedText;//自动模式下的文字速度
    public float SpeedText;//手动模式下的文字显示速度

    public float TargetSpeed = 0.8f;//音频渐变消失的速度

    public AudioSource audioSource;//背景音效的组件
    public AudioSource RoleSource;//人物语音播放的组件
    public AudioSource EffectSource;//效果音效播放的组件

    public SettingPanel setting;//调用设定面板
    public float BGMusicValue = 0;//定义背景音效大小的目标值

    public bool isNext;//判断当前是否自动跳转到下一行
    public bool isUpdate;//判断是否快速跳过剧情

    public int StoryCount;//当前正在应用的故事ID 1
    public string TextInfor;//当前使用的故事名称 Story01

    public bool isDown=true;//判断当前是否可以点击进行下一段话


    public Animator anim;//动画管理器

    private Animator roleAnim; 

    public Sprite SaveBG;//存储预制体需要用到的图片


    /// </summary>

    public enum STATE
    {
        OFF,//处于停止状态
        TYPING,//处于打字状态
        PAUSED,//处于暂停状态
        CHOICE//处于按钮状态
    }
    public STATE state;//引用STATE枚举
    public bool justEnter = false;//判断是否是第一次进入
    void Start()
    {
        justEnter = true;
        Init();//调用初始UI的方法
        LoadCharaSprite(assetConfig.charaA,assetConfig.charaB,assetConfig.charaC);
        state = STATE.OFF;

        data = Resources.Load<Story01>("MyStory/Story01");
        TextInfor = "Story01";
        StoryCount = 1;//设置id和信息
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case STATE.OFF:
                if (justEnter)//justEnter 之后就不再执行了
                {
                    assetConfig = Resources.Load<AVGAssetConfig>("Role/" + data.dataList[curLine].Role);//更换角色图片的参数
                    justEnter = false;//为假 之后就不能再执行了
                    ui.ShowBtnList(data.dataList[curLine].Ischoice, data.dataList[curLine].Isa,
                       data.dataList[curLine].Isb, data.dataList[curLine].Isc);//调用显示按钮的方法
                    HideUI();//调用隐藏UI界面的方法
                    LoadContent(data.dataList[curLine].Dialogtext,
                        data.dataList[curLine].Adisplay,
                        data.dataList[curLine].Bdisplay,
                        data.dataList[curLine].Cdisplay);//加载剧情文本和三名玩家的透明度
                    ShowRoleName(data.dataList[curLine].Isrole,data.dataList[curLine].Roletext);
                }
                break;
            case STATE.TYPING:
                if(justEnter)//justEnter 之后就不再执行了
                {
                    
                    assetConfig = Resources.Load<AVGAssetConfig>("Role/" + data.dataList[curLine].Role);//更换角色图片的参数
                    justEnter = false;//为假 之后就不能再执行了
                    LoadCharaSprite(assetConfig.charaA, assetConfig.charaB, assetConfig.charaC);//实时调用更新角色图片的方法
                    timerValue = 0;

                    isJitter(data.dataList[curLine].Jitter);//判断是否要抖动屏幕
                    isBlack();//判断当前是否要黑屏
                    moveRole(data.dataList[curLine].Moverole, data.dataList[curLine].Moveposition);
                    
                    ChangeRoleMusic(data.dataList[curLine].Isshowrolemusic,data.
                        dataList[curLine].Rolemusic, data.dataList[curLine].Iseffectmusic,
                        data.dataList[curLine].Effectmusic);//更换人物语音和效果音效
                    ChangeMusic(data.dataList[curLine].Isshowmusic, data.dataList[curLine].Music);//更换背景音效
                    ui.ShowBtnList(false, false, false,false);//隐藏全部选项按钮
                    ShowLoadBG(data.dataList[curLine].Bg2alpha);//更换背景图片 加载Alpha
                    LoadContent(data.dataList[curLine].Dialogtext,//读取剧情文本内容和ABC三个角色的显示和隐藏
                        data.dataList[curLine].Adisplay,
                        data.dataList[curLine].Bdisplay,
                        data.dataList[curLine].Cdisplay);//加载剧情文本和三名玩家的透明度
                    ShowRoleName(data.dataList[curLine].Isrole,data.dataList[curLine].Roletext);
                    LoadCGManager.loadCG.UpdateData();//调用更新数据的方法
                    //调用储存CG面板的数据方法
                    LoadCGManager.loadCG.SaveData();//存储数据文件
                }
                CheckTypingFinished();//判断当前对话是否播放完成,如果完成就转换成paused
                UpdateContentString();//更新字符串方法

                break;
            case STATE.PAUSED:
                if (justEnter)//justEnter 之后就不再执行了
                {

                    justEnter = false;//为假 之后就不能再执行了
                }
                break;
            case STATE.CHOICE://当前属于按钮状态
                if (justEnter)//justEnter 之后就不再执行了
                {
                    justEnter = false;//为假 之后就不能再执行了
                    //调用显示按钮的方法逻辑
                    ui.ShowBtnList(data.dataList[curLine].Ischoice,data.dataList[curLine].Isa,
                       data.dataList[curLine].Isb,
                       data.dataList[curLine].Isc);//调用显示按钮的方法
                    SetBtnName(data.dataList[curLine].Btnaname,data.dataList[curLine].Btnbname,data.dataList[curLine].Btncname);//设置按钮的名称
                    SetBtnTexName(data.dataList[curLine].Btnatext,
                        data.dataList[curLine].Btnbtext,
                        data.dataList[curLine].Btnctext);//设置按钮里面的内容
                }
                break;
        }


        if (curLine >= data.dataList.Count)
        {

        }
        else
        {
            UpdateBGValue();
        }

        isNextPlot();//更新文字显示的速度 并且判断 如果 isNext为true就跳转到下一行代码


      
        

        if (isUpdate)
        {
            //如果当前处于快速跳过模式
            UpdateNextPolt();//快速跳过模式
        }


    }
    
    public void CheckTypingFinished()
    {
        //检查是否打印完成
        if (state == STATE.TYPING)
        {
            if ((int)Mathf.Floor(timerValue * TextSpeed) >= targetString.Length)//如果打印机打印的速度大于等于当前文本长度
            {
                if (data.dataList[curLine].Ischoice)//如果当前巨清楚ischoice为true的话 代表要显示按钮
                {
                    GoToState(STATE.CHOICE);//当前状态切换成按钮状态
                }
                else //如果为false 代表不显示按钮
                {
                    GoToState(STATE.PAUSED);//把当前状态切换成暂停状态
                }
                
            }
        }
    }


    public void isBlack()
    {
        //是否黑屏
        if (data.dataList[curLine].Black)
        {
            //如果当前黑屏的话
            isDown = false;//当前不可以点击
            BlackObject.SetActive(true);
        }
    }

    public void UpdateContentString()
    {
        //把文字像打字机一样显示出来
        if (isDown)
        {
            timerValue += Time.deltaTime;//文字下标自增
            string tempString = targetString.Substring(0,
                Mathf.Min((int)Mathf.Floor(timerValue * TextSpeed), targetString.Length));
            ui.SetText(tempString);//把临时变量的字符串传递过去

        }
    }
    public void isNextPlot()
    {
        //更换文字显示的速度 如果当前处于自动更新的状态 那么文字的速度就要等于自动更新的文字速度
        if (isNext)
        {
            //如果可以自动跳转到下一句
            NextPolt();
            TextSpeed = ASpeedText;//自动跳转模式下的文字显示速度
        }
        else
        {
            //如果不可以跳转
            TextSpeed = SpeedText;//手动跳转模式下的文字显示速度
        }
    }
    public void NextPolt()
    {
        
        if (isDown)
        {//为True才可以执行下面的逻辑
            //自动跳转到下一行的逻辑
            switch (state)
            {
                case STATE.PAUSED://判断是否当前处于暂停状态
                    NextLine();//下标加一
                    Thread.Sleep(1500);
                    if (curLine >= data.dataList.Count)//放置数值下标越界
                    {

                    }
                    else
                    {
                        GoToState(STATE.TYPING);//状态切换成Typing 播放下一句话
                    }
                    break;

            }
        }
    }

    public void UpdateNextPolt()
    {
        //快速跳过剧情  
        if (isDown)
        {
            timerValue += Time.deltaTime;//时间自增
            if (timerValue >= 0.3f)
            {
                timerValue = 0;//时间清零
                UserClicked();//调用跳转下一行剧情的方法
            }
        }
        

    }


    public void StartAVG()
    {
        //开始游戏
        GoToState(STATE.TYPING);//开始读取第一段话

    }

    public void UserClicked()
    {
        if (isDown)
        {
            //当鼠标点击了 跳转下一句

            if (state == STATE.TYPING)
            {
                //如果当前处于播放的状态
                if ((int)Mathf.Floor(timerValue * TextSpeed) < targetString.Length)
                {
                    //如果当前的文本 小于这段剧情的文本长度 也就是说打字机状态 没有打完全部的字
                    timerValue = targetString.Length + 1;//文字下标等于这段文字剧情的 总长度 +1
                    string tempString = targetString.Substring(0,
                Mathf.Min((int)Mathf.Floor(timerValue * TextSpeed), targetString.Length));
                    ui.SetText(tempString);//把临时变量的字符串传递过去
                }
            }
        }

            if (state == STATE.PAUSED)
            {
                //如果当前状态等于pause的话
                if (curLine >= data.dataList.Count)
                {
                    //判断当前的下标是否大于等于整个剧情文本的长度
                    //调用结束界面
                }
                else
                {
                    if (isDown)
                    {
                        NextLine();//下标自增
                    }
                    GoToState(STATE.TYPING);//切换成打字状态加载下一句话

                }
            }

    }
    public void GoToState(STATE next)
    {
        //状态切换
        state = next;//把接收进来的值付给state
        justEnter = true;
    }
    public void ShowLoadBG(int i)
    {
        //显示更换背景
        if (data.dataArray[curLine].Ischangebg)
        {
            //判断当前是否要更换背景图片
            Sprite tex = Resources.Load<Sprite>("BG/" + data.dataList[curLine].BG);//加载2号背景图片
            Sprite tex2 = Resources.Load<Sprite>("BG/" + data.dataList[curLine].BG2);//加载2号背景图片
            LoadBG(tex, tex2, i);//传递1号和2号的图片，然后再传递alpha值

            switch(data.dataList[curLine].Bg2alpha)//循环便利一下背景图片2的透明度
            {
                case 0://如果透明度为0 代表当前显示的事背景图片1
                    SaveBG = Resources.Load<Sprite>("BG/" + data.dataList[curLine].BG);//加载1号图片的背景
                    break;
                case 1://如果透明度为01 代表当前显示的事背景图片2
                    SaveBG = Resources.Load<Sprite>("BG/" + data.dataList[curLine].BG2);//加载2号图片的背景
                    break;
            }
        }
    }

    public void LoadBG(Sprite tex,Sprite tex2,int alpha)
    {
        //接收更换背景图片
        ui.ChangeBG(tex, tex2,alpha);
    }


    public void Init()
    {
        //初始化UI
        HideUI();//调用隐藏UI的方法
        curLine = 0;//文本下标清零
        LoadContent("",0,0,0);//清空剧情内文本内容
    }
    public void ShowUI()
    {
        ui.ShowCanvas(true); //显示UI
    }
    public void HideUI()
    {
        //隐藏UI
        ui.ShowCanvas(false);//隐藏UI
    }
    public void NextLine()
    {
        //增加 读取对话内容数组的下标
        curLine++;//增加下标
    }

    public void LoadContent(string value, int charaADisplay, int charaBDisplay, int charaCDisplay) 
    {
        //加载对话数据
        targetString = value;//把字符串剧情赋值
        ui.ShowRoleA(charaADisplay);//调用显示A号角色位置的方法
        ui.ShowRoleB(charaBDisplay);//调用显示B号角色位置的方法
        ui.ShowRoleC(charaCDisplay);//调用显示C号角色位置的方法 
    }
    
    public void LoadCharaSprite(Sprite charaATex, Sprite charaBTex, Sprite charaCTex)
    {
        //更换ABC三个位置图片的方法
        //调用UIPanel里面的切换图片方法
        ui.ChangeRoleA(charaATex);//更换A号角色图片
        ui.ChangeRoleB(charaBTex);//更换B号角色图片
        ui.ChangeRoleC(charaCTex);//更换C号角色图片
    }
    public void isJitter(bool isjitter)
    {
        //是否抖动
        if(anim == null)
        {
            anim = GameObject.FindGameObjectWithTag("AVGPolt").GetComponent<Animator>();
            //获得身上tag为AVGPolt的动画控制器
        }
        if (isjitter)
        {
            //如果true 代表要抖动
            anim.SetTrigger("jitter");//播放抖动动画
        }
        
    }

    public void moveRole(string moveRole, int movePosition)
    {
        //GameObject.FindGameObjectWithTag("Role1").transform.Translate(-600, 0, 0);
        ui.moveRole(moveRole, movePosition);
    }

    public void ShowRoleName(bool value, string name)
    {
        //判断是否显示人物的名称和背景图片
        ui.SetRoleText(value, name);
    }
    public void SetBtnTexName(string a,string b,string c)
    {
        //设置按钮里面的内容
        ui.SetBtnTexName(a,b,c);
    }
    public void SetBtnName(string a,string b,string c)
    {
        ui.SetBtnName(a,b,c);
    }
    public void ProcessBtnMSG(GameObject obj)
    { //接收按钮的button调用
        switch (obj.name)//循环便利传递进来的物品的名称
        {
            case "1"://如果等于1
                //更换故事支线
                print("我们点击了1号按钮");
                data = Resources.Load<Story01>("MyStory/Story02");//加载故事数据
                StoryCount = 2;//故事id设置为2
                TextInfor = "Story02";//故事信息为当前故事的名称
                Init();//调用初始化方法
                ShowUI();//显示UI
                justEnter = false;
                GoToState(STATE.TYPING);//状态切换成typing打字机状态
                break;
            case "2"://如果等于2
                //调用增加好感度 或者什么数值之类的
                print("我们点击了2号按钮");
                NextLine();
                GoToState(STATE.TYPING);//当前状态切换成typing状态 播放下一句话
                break;

        }
        
    }

    int i = 0;//临时变量 用来储存当前播放的音效名称
    public void ChangeMusic(bool isShow,int id)
    {
        //判断是否要显示音效 第二个是音效的名称
        if (isShow)
        {
            SettingBGMusic();//如果当前要更换背景音效的话，就执行这个方法 更新背景音效的大小
            //判断当前是否要显示音效
            if(id != i)
            {
                //如果id不等于i的话 代表更换音效了
                //再播放音频
                audioSource.clip = Resources.Load<AudioClip>("Music/" + id);//读取音频
                audioSource.Play();//播放音效
                i = id;//把id赋值给i
            }
        else
         {
                //如果当前不播放音效了 就渐渐把音效静音 不能直接更换音效
                BGMusicValue = 0;//把目标值设置为0 开始渐变消失 隐藏音频

         }
        }
    }

    int j = 0;
    //用来存储播放的人物语音
    int k = 0;
    //用来储存播放的人物语音
    public void ChangeRoleMusic(bool isShow,int id,bool isShowEffect,int EffectID)
    {
        //更换人物音效和播放效果音效
        if (isShow)
        {
            //如果当前要播放人物的语音
            RoleSource.clip = Resources.Load<AudioClip>("RoleMusic/" + id);//读取人物的语音
            RoleSource.Play();//播放人物语音
            j = id;//把id赋值放置重复播放
        }
        if (isShowEffect)
        {
            //如果当前要播放特效的语音
            EffectSource.clip = Resources.Load<AudioClip>("EffectMusic/" + EffectID);//读取特效的语音
            EffectSource.Play();//播放特效语音
            k = EffectID;//把id赋值放置重复播放
        }
    }
    public void UpdateBGValue()
    {
        //更新背景音效值的大小变化
        if (audioSource.volume != BGMusicValue)//如果 播放背景音效的组件上音频的大小跟我们设置的音频大小不一样
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, BGMusicValue, TargetSpeed * Time.deltaTime);
            if (Mathf.Abs(audioSource.volume - BGMusicValue) < 0.1f)
            {
                audioSource.volume = BGMusicValue;//把值赋给音频组件的大小
            }
        }
    }
    public void SettingBGMusic()
    {
        BGMusicValue = setting.BGMusic * setting.zongMusic;//背景音量大小的目标值 等于 设置面板里面的总音量大小 乘以 北京音量大小

    }

    public void LoadUpdateBG(string bg1,string bg2,float alpha)
    {
        //更新背景图片 在读取存档之后
        ui.BG.sprite = Resources.Load<Sprite>("BG/" + bg1);
        ui.BG2.sprite = Resources.Load<Sprite>("BG/" + bg2);

        ui.BG2Alpha = alpha;//把透明度赋值给背景2 的透明度
    }
   
}
