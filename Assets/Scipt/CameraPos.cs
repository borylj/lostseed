using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public float X;

    public float speed;//速度 鼠标移动

    public float rotateX;


        
 

    
    void Update()
    {
        X = Input.GetAxis("Mouse X");//获得鼠标左右旋转的值

        rotateX += X * speed;

        X = Mathf.Clamp(rotateX, -2, 2);

        transform.eulerAngles = new Vector3(0,X,0);


    }
}
