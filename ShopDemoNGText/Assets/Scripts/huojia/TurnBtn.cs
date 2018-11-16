using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBtn : MonoBehaviour {
  public  Transform _huojia;
  public bool _isTurn;
  Transform _moveBtn;
  Transform _turnBtn;
  string _Floornam;
  int floor;
  int isPutFloor;
  GameObject _isFloorPut;
	void Start () 
    {
        _moveBtn = _huojia.GetChild(0);
        _turnBtn = _huojia.GetChild(1);
    }		
	void Update () 
    {
	}
    void OnClick()
    {     
       _Floornam = _huojia.transform.parent.parent.name;
       floor = int.Parse(_Floornam);
       isPutFloor = floor + 1;
       _isFloorPut = GameObject.Find(isPutFloor.ToString());
      _isTurn=!_isTurn;
        //旋转后
        if(_isTurn)
        {                      
            if (_huojia.transform.parent.name == "huojia_twoge(Clone)")
            {
                if (_isFloorPut.transform.childCount != 0)
                {
                    Debug.Log("111111111");
                    Debug.Log(_isFloorPut.transform);
                }
                else
                { 
                    _huojia.transform.Rotate(new Vector3(0, 180, 0));
                    _huojia.transform.localPosition = new Vector3(0.42f, 0.15f, 1.06f);
                    _moveBtn.transform.localPosition = new Vector3(1.027f, 0.394f, -0.727f);
                    _turnBtn.transform.localPosition = new Vector3(1.04f, -0.99f, 1.04f);
                }
             }
                                 
            if (_huojia.transform.parent.name == "huojia__shechipin(Clone)")
            {
                _huojia.transform.Rotate(new Vector3(0, 180, 0));
                _huojia.transform.localPosition = new Vector3(0.33f,0.353f,0.08f);
                _moveBtn.transform.localPosition = new Vector3(0.68f,0.12f,0.68f);
                _turnBtn.transform.localPosition = new Vector3(0.77f,-0.55f,0.77f);
            }
            if (_huojia.transform.parent.name == "huojia_huazhuangpin(Clone)")
            {
                _huojia.transform.Rotate(new Vector3(0, 180, 0));
                _moveBtn.transform.localPosition = new Vector3(0.594f, 0.526f, -0.594f);
                _turnBtn.transform.localPosition = new Vector3(0.59f, -0.112f, -0.59f);  
            }
            if (_huojia.transform.parent.name == "huojia_choose(Clone)")
            {
                _huojia.transform.Rotate(new Vector3(0, 180, 0));
                _huojia.transform.localPosition = new Vector3(0.14f,0,0.37f);
                _moveBtn.transform.localPosition = new Vector3(0.612f,0.425f,-0.612f);
                _turnBtn.transform.localPosition = new Vector3(0.627f,-0.237f,-0.627f);
            }
        }
        //没旋转之前
        if (_isTurn == false)
        {
            if (_huojia.transform.parent.name == "huojia_twoge(Clone)")
            {
                if (_isFloorPut.transform.childCount>1)
                {
                    Debug.Log(222222222);
                    Debug.Log(_isFloorPut.transform.childCount);
                    //_huojia.transform.Rotate(new Vector3(0, 180, 0));
                    //_huojia.transform.localPosition = new Vector3(1.1f, 0.15f, 0.44f);
                    //_moveBtn.transform.localPosition = new Vector3(-1.02f, 0.31f, -1.02f);
                    //_turnBtn.transform.localPosition = new Vector3(-1.1f, -0.34f, -1.1f);
                }
                else
                {
                    Debug.Log(333333);
                    Debug.Log(_isFloorPut.transform.childCount);
                    _huojia.transform.Rotate(new Vector3(0, 180, 0));
                    _huojia.transform.localPosition = new Vector3(1.1f, 0.15f, 0.44f);
                    _moveBtn.transform.localPosition = new Vector3(-1.02f, 0.31f, -1.02f);
                    _turnBtn.transform.localPosition = new Vector3(-1.1f, -0.34f, -1.1f);
 
                }
               
            }
            if (_huojia.transform.parent.name == "huojia__shechipin(Clone)")
            {
                _huojia.transform.Rotate(new Vector3(0, 180, 0));
                _huojia.transform.localPosition = new Vector3(0.13f,0.353f,0.35f);
                _moveBtn.transform.localPosition = new Vector3(-0.76f,0.58f,0.76f);
                _turnBtn.transform.localPosition = new Vector3(-0.78f,0.01f,0.78f);
            }
            if (_huojia.transform.parent.name == "huojia_huazhuangpin(Clone)")
            {
                _huojia.transform.Rotate(new Vector3(0, 180, 0));
                _huojia.transform.localPosition = new Vector3(0.6f,0.34f,0.44f);
                _moveBtn.transform.localPosition = new Vector3(-0.58f, 0.2f, -0.43f);
                _turnBtn.transform.localPosition = new Vector3(-0.61f, -0.44f, -0.5f);
            }
            if (_huojia.transform.parent.name == "huojia_choose(Clone)")
            {
                _huojia.transform.Rotate(new Vector3(0, 180, 0));
                _huojia.transform.localPosition = new Vector3(0.37f,0,0.1f);
                _moveBtn.transform.localPosition = new Vector3(-0.76f,0.16f,-0.76f);
                _turnBtn.transform.localPosition = new Vector3(-0.8f,-0.55f,-0.8f);
            }
        }
    }
}
