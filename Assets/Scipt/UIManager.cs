using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject SavePanel;//存储面板
    public GameObject SettingPanel;//设置面板
    public GameObject UIMain;//主界面
    public GameObject LoadTextPanel;//回忆面板
    public CanvasGroup LoadCGPoltPanel;//加载 CG回放 剧情回放 以及音乐鉴赏的面板

    public GameObject PoltBtn;//剧情下面的按钮
    public GameObject ContentBG;//对话框背景
    public GameObject RoleImg;//人名背景
    public GameObject Dialogtext;//对话框文字
    public GameObject UIBtn;//菜单界面的UI按钮
    public Canvas PoltCanvas;//剧情UI界面
    public GameObject ShowCG;//用来显示CG图片的物体

    public DataSaveManager data;//获得 存储管理器

    public Sprite LoadImg;//读取界面的LOGO
    public Sprite SaveImg;//存储界面的LOGO
    public Image LOGO;//存储界面的LOGO

    public Button Yes;
    public Button No;//确认和取消两个按钮

    public GameObject EnterObject;//获得确认面板
    public Text T;//这个是确认面板上面的文本

    public int id;//这个id是判断现在正执行什么事情

    public Saveslot slot;//存储预制体
    public CGPrefab cg;//CG回放预制体

    public static UIManager ui;

    public bool isNext;//判断是否开启了自动跳转功能
    public bool isNext2;//判断是否开启了快速跳转功能

    public Sprite NextIdle;//自动跳转的待机图片
    public Sprite NextActive;//自动跳转的激活图片
    public Sprite UpdateIdle;//快速跳转的待机图片
    public Sprite UpdateActive;//快速跳转的激活图片

    public Image NextIMG;//获得自动跳转按钮身上的图片组件
    public Image UpdateIMG;//获得快速跳转按钮身上的图片组件

    public AVGMachine machine;//调用avg框架
    public AudioSource MainMusic;//背景音效
    public Image WinDownIMG;//窗口图片

    public GameObject BlackObject;//开始游戏时的黑屏界面
    

    private void Awake()
    {
       ui = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SavePanel.SetActive(false);//开局隐藏 存储界面
        SettingPanel.SetActive(false);//开局隐藏 设置界面
        Yes.onClick.AddListener(YesEvent);//取消按钮 引用YesEvent
        No.onClick.AddListener(NoEvent);//取消按钮 引用NoEvent

        HideCGPanel();//隐藏回放界面
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0.1f)
        {
            //如果我们的鼠标滚轮向上滑动的话
            machine.isDown = false;//当前不可点击下一段剧情
            LoadTextPanel.SetActive(true);//显示回忆文本面板
            //显示回忆面板之后 调用show Text方法读取我们已经看过的十行剧情文本，如果我们看的剧本少于十行 则调用所有看的文本
            LoadTextPanel.GetComponent<LoadDiaText>().ShowText();//显示文本剧情
        }
        if (Input.GetAxis("Mouse ScrollWheel") == -0.1f)
        {
            //如果我们的鼠标滚轮向上滑动的话
            LoadTextPanel.SetActive(false);//显示回忆文本面板
            
        }

    }
    public void ShowSavePanel()
    {
        //显示存储界面
        data.isSave = true;//当前处于存储状态
        SavePanel.SetActive(true);//显示存储界面
        LOGO.sprite = SaveImg;//把存储的logo赋值
        DataSaveManager.data.LoadFileData();//读取数据文件
    }
    public void ShowLoadPanel()
    {
        //显示存储界面
        data.isSave = false;//当前处于读取状态
        SavePanel.SetActive(true);//显示存储界面
        LOGO.sprite = LoadImg;//把读取的logo赋值
        DataSaveManager.data.LoadFileData();//读取数据文件
    }

    public void HideSavePanel()
    {
        //隐藏存储或读档界面
        SavePanel.SetActive(false);//隐藏存储界面
    }
    public void YesEvent()
    {
        //按下确认按钮之后
        switch (id)
        {
            //循环便利一下id
            case 1:
                break;
            case 2:
                //如果为2代表当前要处理退出事件
                Application.Quit();//退出代码
                break;
            case 3:
                //如果为3代表当前要回到标题
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加载当前场景
                break;
            case 5:
                //调用存储文件预制体的方法
                slot.SaveFile();
                break;
            case 6:
                //调用读取文件预制体的方法
                //隐藏加载界面
                UIMain.SetActive(false);//隐藏加载面板
                slot.LoadFile();
                break;
            case 7:
                //为7 是剧情回放
                cg.LoadPlot();//调用加载剧情的方法
                break;
            case 8:
                //为8 是CG鉴赏
                cg.LoadCG();//调用加载cg的方法
                break;
        }
        EnterObject.SetActive(false);//隐藏确认面板
    }
    public void NoEvent()
    {
        //按下取消按钮之后
        EnterObject.SetActive(false);//隐藏确认面板
    }

    public void QuitGame()
    {
        //退出游戏的方法
        id = 2;//id等于2 当前处于退出游戏时间
        EnterObject.SetActive(true);//显示确认面板
        T.text = "是否退出游戏";//显示文本
    }
    public void GoMenu()
    {
        //回到标题的方法
        id = 3;//id等于3 当前处于回到标题
        EnterObject.SetActive(true);//显示确认面板
        T.text = "是否回到标题";//显示文本
    }
    public void isSave(Saveslot slot)
    {
        //是否存储数据文件
        id = 5;//id 等于 5 处理储存 数据 事件
        this.slot = slot;//把存档预制体赋值
        EnterObject.SetActive(true);//显示确认界面
        T.text = "是否覆盖存档";
    }
    public void isLoad(Saveslot slot)
    {
        //是否读取数据文件
        id = 6;//id 等于 6 处理读取 数据 事件
        this.slot = slot;//把存档预制体赋值
        EnterObject.SetActive(true);//显示确认界面
        T.text = "是否加载存档";
    }

    public void ShowSettingPanel()
    {
        //显示设置见面
        SettingPanel.SetActive(true);//显示设置界面
    }
    public void HideSettingPanel()
    {
        //隐藏设置见面
        SettingPanel.GetComponent<SettingPanel>().SaveValue();//调用存储数据方法
        SettingPanel.SetActive(false);//隐藏设置界面
    }
    public void HideLoadPanel()
    {
        //关闭回忆文本界面
        machine.isDown = true;//可以点击下一句
        LoadTextPanel.SetActive(false);//关闭
    }

    public void NextPolt()
    {
        //自动跳转下一行
        if (isNext)
        {
            //如果开启了自动跳转
            isNext = false;//关闭自动跳转下一行的功能
            NextIMG.sprite = NextIdle;//图片切换成待机图片
            machine.isNext = false;//当前不可以自动跳转到下一句
        }
        else
        {
            //如果为false 代表没有开启
            isNext = true;//true为开启
            NextIMG.sprite = NextActive;//图片切换成激活图片
            //执行 自动跳转下一行的功能
            machine.isNext = true;//当前可以跳转到下一句
        }
    }
    public void UpdatePolt()
    {
        //快速跳转剧情
        if (isNext2)
        {
            //如果开启了自动跳转
            isNext2 = false;//关闭快速跳转下一行的功能
            UpdateIMG.sprite = UpdateIdle;//图片切换成待机图片
            machine.isUpdate = false ;//当前不可以跳转到下一句
        }
        else
        {
            if(isNext)
            {
                //如果开启了自动跳转
            isNext = false;//关闭自动跳转下一行的功能
            NextIMG.sprite = NextIdle;//图片切换成待机图片
            machine.isNext = false;//当前不可以自动跳转到下一句

            }
            //如果为false 代表没有开启
            isNext2 = true;//true为开启
            UpdateIMG.sprite = UpdateActive;//图片切换成激活图片
            //执行 快速跳转下一行的功能
            machine.isUpdate = true;//当前可以跳转到下一句
        }
    }

    public void ShowCGPanel()
    {
        //显示CG面板
        LoadCGPoltPanel.alpha = 1;//透明度为1 显示
        LoadCGPoltPanel.blocksRaycasts = true;//显示
        LoadCGPoltPanel.interactable = true;
    }
    public void HideCGPanel()
    {
        //隐藏CG面板
        LoadCGManager.loadCG.LoadData();//调用读取数据方法
        LoadCGPoltPanel.alpha = 0;//透明度为0 隐藏
        LoadCGPoltPanel.blocksRaycasts = false;//隐藏
        LoadCGPoltPanel.interactable = false;
    }
    public void ShowPoltBtn()
    {
        //显示剧情面板的按钮
        PoltBtn.SetActive(true);
        ContentBG.SetActive(true);
        Dialogtext.SetActive(true);
        RoleImg.SetActive(true);
    }
    public void HidePoltBtn()
    {
        //隐藏剧情面板的按钮
        PoltBtn.SetActive(false);
        ContentBG.SetActive(false);
        Dialogtext.SetActive(false);
        RoleImg.SetActive(false);
    }

    public void LoadPolt(CGPrefab cg)
    {
        //加载剧情回放
        id = 7;//id 等于 7 是否加载剧情回放
        this.cg = cg;//把存档预制体赋值
        
            EnterObject.SetActive(true);//显示确认界面
            T.text = "是否加载剧情回放";//
    }
    public void LoadCG(CGPrefab cg)
    {
        //加载CG回放
        id = 8;//id 等于 8 是否加载CG回放
        this.cg = cg;//把存档预制体赋值

        EnterObject.SetActive(true);//显示确认界面
        T.text = "是否加载CG鉴赏";//
    }
    public void StartGame()
    {
        //开始游戏
        MainMusic.enabled = false;//关闭主菜单BGM
        PoltCanvas.enabled = true;//显示剧情界面UI
        
        
    }
    public void ShowBlack()
    {
        //显示黑屏
        BlackObject.SetActive(true);
    }
    public void HideMusic()
    {
        //关闭主菜单的背景音效
        MainMusic.enabled = false;//关闭背景音效
    }
    public void ShowMusic()
    {
        //关闭主菜单的背景音效
        MainMusic.enabled = true;//开启背景音效
    }
}
