using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public CanvasGroup RoleA;//获得角色A号位置的画布组
    public CanvasGroup RoleB;//获得角色B号位置的画布组
    public CanvasGroup RoleC;//获得角色C号位置的画布组

    public Image DiaLogImg;//获得对话框的图片

    public Image BG;//获得背景图片 用来替换背景
    public Image BG2;//获得第二个背景图片

    public CanvasGroup BGGroup;//第一个背景画布
    public CanvasGroup BG2Group;//第二个背景画布

    public Canvas canvas;//获得自身上的这个canvas画布

    public Text TextList;//对话文本

    public Text RoleText;//用来显示人物名称的文本
    public Image RoleImg;//用来获得人物名称的图片

    public GameObject BtnA;//获得A号按钮
    public GameObject BtnB;//获得B号按钮
    public GameObject BtnC;//获得C号按钮

    public float TargerSpeed = 2f;//人物显示的渐变速度
    public float RoleAAlpha = 0;//A号人物的Alpha的值
    public float RoleBAlpha = 0;//A号人物的Alpha的值
    public float RoleCAlpha = 0;//A号人物的Alpha的值

    public float BG2Alpha = 0;//背景图片2的目标透明度

    // 角色移动位置, 正数为基于当前位置右移, 负数为基于当前位置左移
    public float movePostion = 0;
    public string moveRoleTag;
    public CanvasGroup realMoveRole = null;

    private float moveDuration = 5f;
    private float moveStartTag = -1f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("a"))
        {
            ShowRoleA(1);//判断是否要隐藏A号角色
            ShowRoleB(1);//判断是否要隐藏B号角色
            ShowRoleC(1);//判断是否要隐藏C号角色
        }
        if (Input.GetKeyUp("b"))
        {
            ShowRoleA(0);//判断是否要隐藏A号角色
            ShowRoleB(0);//判断是否要隐藏B号角色
            ShowRoleC(0);//判断是否要隐藏C号角色
        }

        if (Input.GetKeyUp("c"))
        {
            SetText("Hello Happy World!");
        }
        if (Input.GetKeyUp("d"))
        {
            SetRoleText(true, "不知道");
        }
        if (Input.GetKeyUp("e"))
        {
            SetRoleText(false, "尊嘟假嘟");
        }
        #region 角色A的透明渐变
        if (RoleA.alpha !=RoleAAlpha)//如果A号角色身上的画布透明度不等于我们手动设置的透明度的话
        {
            RoleA.alpha = Mathf.Lerp(RoleA.alpha, RoleAAlpha, TargerSpeed * Time.deltaTime);//角色A的透明值
                                                                                            //使用Lerp渐变到我们设置的alpha值
            if(Mathf.Abs(RoleA.alpha - RoleAAlpha) < 0.1f)
            {
                //如果玩家的透明度减去我们设置的透明度的绝对值 小于0.1的话
                RoleA.alpha = RoleAAlpha;//把目标的透明值设置给A号的透明值
            }
        }
        #endregion
        #region 角色B的透明渐变
        if (RoleB.alpha != RoleBAlpha)//如果A号角色身上的画布透明度不等于我们手动设置的透明度的话
        {
            RoleB.alpha = Mathf.Lerp(RoleB.alpha, RoleBAlpha, TargerSpeed * Time.deltaTime);//角色A的透明值
                                                                                            //使用Lerp渐变到我们设置的alpha值
            if (Mathf.Abs(RoleB.alpha - RoleBAlpha) < 0.1f)
            {
                //如果玩家的透明度减去我们设置的透明度的绝对值 小于0.1的话
                RoleB.alpha = RoleBAlpha;//把目标的透明值设置给A号的透明值
            }
        }
        #endregion
        #region 角色C的透明渐变
        if (RoleC.alpha != RoleCAlpha)//如果A号角色身上的画布透明度不等于我们手动设置的透明度的话
        {
            RoleC.alpha = Mathf.Lerp(RoleC.alpha, RoleCAlpha, TargerSpeed * Time.deltaTime);//角色A的透明值
                                                                                            //使用Lerp渐变到我们设置的alpha值
            if (Mathf.Abs(RoleC.alpha - RoleCAlpha) < 0.1f)
            {
                //如果玩家的透明度减去我们设置的透明度的绝对值 小于0.1的话
                RoleC.alpha = RoleCAlpha;//把目标的透明值设置给A号的透明值
            }
        }
        #endregion

        #region 背景图片2 渐变显示和隐藏
        if (BG2Group.alpha != BG2Alpha)
        {
            //如果背景图片2身上的画布透明度 不等于我们设置的目标透明度
            BG2Group.alpha=Mathf.Lerp(BG2Group.alpha, BG2Alpha, TargerSpeed * Time.deltaTime);//背景图片2的渐变值
            if (Mathf.Abs(BG2Group.alpha - BG2Alpha) < 0.1f)
            {
                BG2Group.alpha = BG2Alpha;//直接把透明度赋值给画布组上面的那个透明度值
            }
        }
        #endregion

        #region 更改角色位置

        float startPostion = realMoveRole.transform.localPosition.x;
        if (realMoveRole != null && movePostion != 0 && startPostion != movePostion)
        {
            if (moveStartTag < 0)
            {
                moveStartTag = Time.time;
            }

            float progress = (Time.time - moveStartTag) / moveDuration;
            if (progress > 1)
            {
                progress = 1;
            }

            float offset = startPostion + (movePostion - startPostion) * progress;
            Debug.Log("move " + movePostion + " -- " + startPostion + " -- " + offset + " --- " + progress);
            if (Mathf.Abs(movePostion - offset) < 50.0f)
            {
                realMoveRole.transform.localPosition = new Vector3(movePostion, 0, 0);
                moveStartTag = -1f;
            }
            else
            {
                realMoveRole.transform.localPosition = new Vector3(offset, 0, 0);
            }

        }
        #endregion
    }


    #region 判断ABC三个位置的角色 是否显示或者隐藏
    public void ShowRoleA(int value)//value只接受0或1
    {
        //判断是否要显示A号角色图片
        RoleAAlpha = value;//把接收进来的数值 复制给A号位置图片的 透明度
    }
    public void ShowRoleB(int value)//value只接受0或1
    {
        //判断是否要显示A号角色图片
        RoleBAlpha = value;//把接收进来的数值 复制给B号位置图片的 透明度
    }
    public void ShowRoleC(int value)//value只接受0或1
    {
        //判断是否要显示A号角色图片
        RoleCAlpha = value;//把接收进来的数值 复制给C号位置图片的 透明度
    }
    #endregion 

    public void ShowCanvas(bool value)
    {
        //判断是否显示 或者隐藏画布
        canvas.enabled = value;//把接收传递进来的值 赋值给当前剧情画布的enable

    }

    public void SetText(string value)
    {
        //给我们的对话框赋值
        TextList.text = value;//把接收进来的字符串传递给剧情文本

    }

    public void SetRoleText(bool isShow,string value)
    {
        //第一个布尔是判断是否要显示人物名称的背景图片 第二个字符串是要显示的人物名称
        //判断是否要显示人物的名称，如果要实现就设置人物的名称
        if(isShow)
        {
            //如果当前要显示人物名称
            RoleImg.gameObject.SetActive(true);//显示人物名称的背景图片
            RoleText.text = value;//把传递进来的人物名称赋值
        }
        else
        {
            //如果不显示人物的名称和背景图片
            RoleImg.gameObject.SetActive(false);//隐藏人物名称的背景图片

        }
    }

    public void ShowBtnList(bool value,bool A,bool B,bool C)
    {
        //显示或者隐藏按钮（多个按钮的情况）
        BtnA.SetActive(value);//判断当前按钮是否显示或者隐藏
        BtnB.SetActive(value);//判断当前按钮是否显示或者隐藏
        BtnC.SetActive(value);//判断当前按钮是否显示或者隐藏
        //===================================================
        BtnA.SetActive(A);//判断当前按钮在当前剧情中是否要显示或隐藏
        BtnB.SetActive(B);//判断当前按钮在当前剧情中是否要显示或隐藏
        BtnC.SetActive(C);//判断当前按钮在当前剧情中是否要显示或隐藏

    }
    public void ChangeRoleA(Sprite img)
    {
        //更换A号角色位置的图片
        RoleA.GetComponent<Image>().sprite = img;//把传递进来的图片添加到A号位置的图片
    }
    public void ChangeRoleB(Sprite img)
    {
        //更换B号角色位置的图片
        RoleB.GetComponent<Image>().sprite = img;//把传递进来的图片添加到A号位置的图片
    }
    public void ChangeRoleC(Sprite img)
    {
        //更换C号角色位置的图片
        RoleC.GetComponent<Image>().sprite = img;//把传递进来的图片添加到A号位置的图片
    }
    public void SetBtnTexName(string a,string b,string c)
    {
        //设置按钮里面的内容
        //BtnA.transform.GetChild(0).GetComponentInChildren<Text>().text = a;
        //BtnB.transform.GetChild(0).GetComponentInChildren<Text>().text = b;
        //BtnC.transform.GetChild(0).GetComponentInChildren<Text>().text = c;
        BtnA.GetComponentInChildren<Text>().text = a;//把接收进来的内容 赋值给按钮子集下的文本
        BtnB.GetComponentInChildren<Text>().text = b;//把接收进来的内容 赋值给按钮子集下的文本
        BtnC.GetComponentInChildren<Text>().text = c;//把接收进来的内容 赋值给按钮子集下的文本
    }
    public void SetBtnName(string a,string b,string c)
    {
        BtnA.name = a;//修改按钮的名称
        BtnB.name = b;//修改按钮的名称
        BtnC.name = c;//修改按钮的名称
    }
    public void ChangeBG(Sprite img,Sprite img2,int alpha)
    {
        //接收三个参数 第一个是图片类型的图片背景，第二个是第二个背景，第三个是第二个图片的alpha值
        BG.sprite = img;//把图片赋值给bg
        BG2.sprite = img2;//把图片赋值给bg2
        BG2Alpha = alpha;//把透明度赋值
    }

    public void moveRole(string role, int position)
    {
        if (role == null)
        {
            return;
        }
        print(role + " " + position);
        if (role.Equals("A"))
        {
            realMoveRole = RoleA;
        } else if (role.Equals("B"))
        {
            realMoveRole = RoleB;
        } else if (role.Equals("C"))
        {
            realMoveRole = RoleC;
        }
        // 转换position位置为实际需要移动到的坐标位置
        movePostion = 596 * position + realMoveRole.transform.localPosition.x;
    }

}
