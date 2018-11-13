using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuojiaTurn : MonoBehaviour {
    public Transform _huojia;
    public bool _isTurn;
    Transform _huojiaT;
    Transform _huojiaM;
    string _Floornam;
    int _floor;
    int isPutFloor;
    int isPutFloorTurn;
    GameObject _isFloorPutTurn;
    GameObject _isFloorPut;
    GameObject _UIRoot;
   Transform _shengcheng;
   Transform _FloorPac;
   Transform _FloorpacParent;
   Transform _Cubeqiang;
   Transform _cube1;
   Transform _cube2;
   Transform _cube3;
   Transform _cube4;
	// Use this for initialization
	void Start () 
    {
        _huojiaM = _huojia.Find("Move");
        _huojiaT = _huojia.Find("Turn");
        _FloorpacParent = _huojia.Find("Floorpcture");
        _FloorPac = _FloorpacParent.transform.Find("Floorpcture2");
        _UIRoot = GameObject.Find("SelectPanel");
        _shengcheng = _UIRoot.transform.Find("ShangCheng");
        //_Cubeqiang = _huojia.Find("Cubeqiang");
        //_cube1 = _Cubeqiang.Find("Cube (1)");
        //_cube2 = _Cubeqiang.Find("Cube (2)");
        //_cube3 = _Cubeqiang.Find("Cube (3)");
        //_cube4 = _Cubeqiang.Find("Cube (4)");
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        _Floornam = _huojia.transform.parent.name;
        _floor = int.Parse(_Floornam);
        isPutFloor = _floor + 10;
        isPutFloorTurn = _floor + 1;
        _isFloorPutTurn = GameObject.Find(isPutFloorTurn.ToString());
        _isFloorPut = GameObject.Find(isPutFloor.ToString());
        _huojia.GetComponent<HuojiaFollow>().enabled = true;
        _isTurn = !_isTurn;
        _shengcheng.gameObject.SetActive(false);
        _huojia.Find("Move").GetComponent<UIFollowNG>().hud.SetActive(false);
        transform.gameObject.SetActive(false);
        _huojia.GetComponent<HuojiaFollow>().huojiaUI = true;
        if (_isTurn)
        {
            if (_huojia.name == "面包货架2(Clone)")
            {
               // _huojia.GetComponent<SpriteRenderer>().flipX = true;
                _huojia.localRotation = Quaternion.Euler(0, 180, 0);
                _huojiaM.localPosition = new Vector3(-0.4f, 0.38f, 0);
                _huojiaT.localPosition = new Vector3(-0.4f, -0.31f, 0);
               // _huojia.GetComponent<NavMeshObstacle>().center = new Vector3(-0.32f, -0.3f, 0);
            }
            if (_huojia.name == "蛋糕货架2(Clone)")
            {
                //_huojia.GetComponent<SpriteRenderer>().flipX =true;
                _huojia.localRotation = Quaternion.Euler(0, 180, 0);
                _huojiaT.localPosition = new Vector3(-0.96f, -0.54f, 0);
                _huojiaM.localPosition = new Vector3(-0.96f, 0.13f, 0);
                //_huojia.GetComponent<NavMeshObstacle>().center = new Vector3(0.1f, -0.28f, 0);
            }
            if (_huojia.name == "甜点货架2(Clone)")
            {
                _huojia.localRotation = Quaternion.Euler(0, 180, 0);
                //_huojia.GetComponent<SpriteRenderer>().flipX = true;
                _huojiaT.localPosition = new Vector3(-0.6f, -0.02f, 0);
                _huojiaM.localPosition = new Vector3(-0.6f, 0.62f, 0);
               // _huojia.GetComponent<NavMeshObstacle>().center = new Vector3(-0.21f,-0.19f,0);
            }
            if (_huojia.name == "蔬菜货架2(Clone)")
            {
                _Cubeqiang = _huojia.Find("Cubeqiang");
                _cube1 = _Cubeqiang.Find("Cube (1)");
                _cube2 = _Cubeqiang.Find("Cube (2)");
                _cube3 = _Cubeqiang.Find("Cube (3)");
                _cube4 = _Cubeqiang.Find("Cube (4)");
                if (_isFloorPut.transform.childCount != 0)
                {
                    Debug.Log("1111" + _isFloorPut.transform);
                }
                else
                {
                 _huojia.GetComponent<SpriteRenderer>().flipX = true;
                 // _huojia.localRotation = Quaternion.Euler(0,180,0);
                _huojia.GetComponent<BoxCollider>().center = new Vector3(-0.56f,0.03f,-0.9f);
                _huojiaM.localPosition = new Vector3(0.55f,0.69f,0);
                _huojiaT.localPosition = new Vector3(0.55f,0.04f,0);
                _FloorPac.localPosition = new Vector3(-0.695f,-0.32f,0);
                _cube1.localPosition = new Vector3(-1.07f,0.12f,-0.26f);
                _cube1.localRotation = Quaternion.Euler(90,-113,0);
                _cube2.localPosition = new Vector3(-1.46f,-0.12f,0);
                _cube2.localRotation = Quaternion.Euler(90,-107,0);
                _cube3.localPosition = new Vector3(-0.6f,-0.09f,0.22f);
                _cube3.localRotation = Quaternion.Euler(90,-55,0);
                _cube4.localPosition = new Vector3(-1.91f,-0.03f,-0.37f);
                _cube4.localRotation = Quaternion.Euler(90,-65,0);
                }
            }       
        }
        else
        {
            if (_huojia.name == "面包货架2(Clone)")
            {
                //_huojia.GetComponent<SpriteRenderer>().flipX = false;
                _huojia.localRotation = Quaternion.Euler(0, 0, 0);
                _huojiaT.localPosition = new Vector3(1,-0.31f,0);
                _huojiaM.localPosition = new Vector3(1,0.38f,0);
               // _huojia.GetComponent<NavMeshObstacle>().center = new Vector3(0.3f,-0.3f, 0);
            }
            if (_huojia.name == "蛋糕货架2(Clone)")
            {
                _huojia.localRotation = Quaternion.Euler(0, 0, 0);
               //_huojia.GetComponent<SpriteRenderer>().flipX = false;
                _huojiaT.localPosition = new Vector3(0.6f,-0.22f,0);
                _huojiaM.localPosition = new Vector3(0.6f,0.45f,0);
                //_huojia.GetComponent<NavMeshObstacle>().center = new Vector3(0.9f, 0.65f, 0);
            }
            if (_huojia.name == "甜点货架2(Clone)")
            {
                _huojia.localRotation = Quaternion.Euler(0,0,0);
                _huojiaT.localPosition = new Vector3(0.9f,-0.48f,0);
                _huojiaM.localPosition = new Vector3(0.9f,0.15f,0);
                //_huojia.GetComponent<SpriteRenderer>().flipX = false;
                //_huojia.GetComponent<NavMeshObstacle>().center = new Vector3(0.21f,-0.1f, 0);
            }
            if (_huojia.name == "蔬菜货架2(Clone)")
            {
                if (_isFloorPutTurn.transform.childCount!=0)
                {
                    Debug.Log(_isFloorPut.transform.childCount);
                }
                else
                {
                _huojia.GetComponent<SpriteRenderer>().flipX = false;
                //_huojia.localRotation = Quaternion.Euler(0,0, 0);
                _huojia.GetComponent<BoxCollider>().center = new Vector3(0.53f, 0.03f, -0.9f);
                _huojiaM.localPosition = new Vector3(1.6f,0.1f,0);
                _huojiaT.localPosition = new Vector3(1.6f,-0.52f,0);
                _FloorPac.localPosition = new Vector3(0.695f, -0.32f, 0);
                _cube1.localPosition = new Vector3(-0.386f, 0.119f, -0.261f);
                _cube1.localRotation = Quaternion.Euler(90, -106, -41);
                _cube2.localPosition = new Vector3(0.08f, 0.12f, 0.11f);
                _cube2.localRotation = Quaternion.Euler(90, -64, 0);
                _cube3.localPosition = new Vector3(0.492f, -0.09f, -0.351f);
                _cube3.localRotation = Quaternion.Euler(90, 63, 0);
                _cube4.localPosition = new Vector3(-0.78f, -0.03f, 0.26f);
                _cube4.localRotation = Quaternion.Euler(90, 55, 0);
                }

            }
            //_shengcheng.gameObject.SetActive(true);
            //transform.Find("Move").GetComponent<UIFollowNG>().hud.SetActive(true);
            //transform.Find("Turn").GetComponent<UIFollowNG>().hud.SetActive(true);
        }
    }
}
