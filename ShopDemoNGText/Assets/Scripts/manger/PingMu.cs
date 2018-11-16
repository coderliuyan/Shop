using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingMu : MonoBehaviour {
   float distance = 0;//触控缩放的距离
    private float lastDist = 0;//用于计算触控缩放
    private float curDist = 0;//用于计算触控缩放
    int t;//判断缩放触控
    Camera cam;
    float _camSize = 21;
    void Start()
    {
        cam = Camera.main;
        //CameraSize.Instance.ChangeText(cam.GetComponent<Camera>().orthographicSize.ToString());

    }
	
    void Update ()
    {
        //两点以上触控，且触控点发生移动
        if ((Input.touchCount > 1) && (Input.GetTouch(0).phase == TouchPhase.Moved
            || Input.GetTouch(1).phase == TouchPhase.Moved))
        {
            var touch1 = Input.GetTouch(0); //第一根手指
            var touch2 = Input.GetTouch(1); //第二根手指
            curDist = Vector2.Distance(touch1.position, touch2.position);//两指间距
             //当手指移动时，重置起始距离为当前距离
            if (t == 0)
            {
                lastDist = curDist;
                t = 1;
            }
            distance = curDist - lastDist;
            ChangCamSize();
            //this.gameObject.transform.localScale += Vector3.one * distance * Time.deltaTime;
            lastDist = curDist;
        }
        //没有触控事件
        if (Input.touchCount == 0)
            t = 0;
    }
    void ChangCamSize()
    {  
        _camSize -= 0.5f* distance * Time.deltaTime;
        if (_camSize >= 21)
        {
            _camSize = 21;
        }
        if (_camSize <= 8)
        {
            _camSize = 8;
        }
        cam.GetComponent<Camera>().orthographicSize=_camSize;
        Debug.Log(_camSize);
        //CameraSize.Instance.ChangeText(_camSize.ToString());
        
    }


}
