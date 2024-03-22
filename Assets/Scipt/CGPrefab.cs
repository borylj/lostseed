using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CGPrefab : MonoBehaviour, IPointerDownHandler
{
    public bool isCG;//判断是CG鉴赏还是剧情回放
                     //以下是剧情回放预制体的参数

    public int CurLine;//剧情下标
    public string StoryInter;//故事的信息
    public int StoryID;//故事的ID
    public Story01 data;//要回放的故事线数据
                          //===========================================
                          //以下是CG鉴赏的参数

    public List<Sprite> ImgList;//有时候会一张图有多张CG
    public int index;//图片下标
    //以下是公共参数
    public string IMGName;//图片的名称
    public bool isShow;//判断当前回放的预制体是否被激活

    private void Start()
    {
        
    }
    public void OnPointerDown(
        PointerEventData eventData)
    {//当鼠标点击了
        if (isShow)//如果当前的预制体被激活了 才能执行下面的方法
        {
            
            if (isCG)
            {
                //如果为true 代表当前的预制体是CG回放

                UIManager.ui.LoadCG(this);//调用CG方法

            }
            else
            {
                //如果为false 当前预制体是 剧情回放
                UIManager.ui.LoadPolt(this);//调用CG方法
            }
        }
        
    }

    public void LoadCG()
    {
        //加载CG
        UIManager.ui.ShowCG.SetActive(true);//显示CG面板
        UIManager.ui.ShowCG.GetComponent<ShowCGPanels>().cg = this;//把自身传递过去
    }

    public void LoadPlot()
    {
        //加载剧情
        UIManager.ui.PoltCanvas.GetComponent<Canvas>().enabled = true;//显示剧情
        UIManager.ui.UIBtn.SetActive(false);//关闭菜单界面按钮
        UIManager.ui.machine.curLine = CurLine;//把剧情下标赋值
        UIManager.ui.machine.TextInfor = StoryInter;//把故事的信息赋值
        UIManager.ui.machine.StoryCount = StoryID;//把故事的ID赋值
        UIManager.ui.machine.data = data;//把故事线的数据传递过去
        UIManager.ui.machine.GoToState(AVGMachine.STATE.TYPING);//当前状态切换成打字状态
        UIManager.ui.HideCGPanel();//隐藏自身的CG剧情回放面板
    }

        private void Update()
        {
            if (isShow)
            {
                //如果当前被激活了

                transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("CG/" + IMGName);//加载图片的名称

            }
        }
    public void ShowBlack()
    {
        //显示黑色图片
        //如果没有被激活
        transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("BG/" + "black");//当前预制体没有数据
    }
    }
