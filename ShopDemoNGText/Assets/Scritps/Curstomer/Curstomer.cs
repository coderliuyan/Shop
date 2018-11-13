using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curstomer : MonoBehaviour {
    public int _Like;
    public Image _ThinkGoods;//想买的物品
    public Image _FindGoods;//找到的物品
    private bool IsMouseDown = false;//判断鼠标点击
    private Vector3 lastMousePosition = Vector3.zero;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit2D Rayhit;
        Rayhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            if(Rayhit.collider.tag=="顾客")
            {
                   IsMouseDown = true;
            }       
        }
        else
        {
            IsMouseDown = false;
            lastMousePosition = Vector3.zero;
            CurstomerMove(15f);
        }
        //if (Input.GetMouseButtonUp(0))
        //{           
        //}
        if (IsMouseDown)//拖拽顾客
        {
            if (lastMousePosition != Vector3.zero)
            {
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                this.transform.position += offset;
            }
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
	}
   public void CurstomerMove(float Movetime)
    {
        Movetime -= 1;
        if (Movetime >= 10 || Movetime <= 7)
        {
            ThinkGoods();
        }
        if (Movetime <= 0)
        {
            //未找到想要的物品
            //离店。
        }
        else
        {
            //找到想要的东西
            //时间加十秒,查看物品。
            Movetime += 10f;
            if (_ThinkGoods.tag == _FindGoods.tag)//查看物品是否满意
            {
                _Like = 1;
            }
            if (_Like >= 1)
            {
                //走向收银台，自动寻路。
            }
        }
    }
    void ThinkGoods()
    { 
    }
}
