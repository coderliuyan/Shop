using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreat : MonoBehaviour
{
   // GameObject sprit;
    public GameObject par;
    private static TestCreat _instance = null;
   // bool _CFloor;
    // Use this for initialization
    GameObject _floorUIborn;
   public int i;
   public int j;
    void Awake()
    {
        _instance = this;
        par = GameObject.Find("Floor");
        _floorUIborn = GameObject.Find("FloorBorn");
        //_CFloor = CardBtn.Instance._Creatfloor;
        ////sprit = GameObject.Find("cube");    
        //if (_CFloor)
        //{
        //    CreatSomeThing();
        //}

    }
    public static TestCreat Instance
    {
        get
        {
            return _instance;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    CreatSomeThing();
        //}
    }
  public  void CreatSomeThing()
    {
       
        for ( i = 0; i < 6; i++)
        {
            for (j = 0; j < 6; j++)
            {
                GameObject obj =  (GameObject)Instantiate(Resources.Load("Bool"));               
                obj.name = i+""+j;
                //_floorUIborn.GetComponent<FloorUI>().FloorBornPos = par.transform;
                obj.transform.SetParent(par.transform);
                obj.transform.localPosition = new Vector3((i * 5.7f) + 0.1f * (i - 1), 0, (j * 5.7f) + 0.1f * (j - 1));
                //GameObject _floorUI = (GameObject)Instantiate(Resources.Load("FloorFirst"));
                //// Debug.Log("Flooor"+_floorUI);
                //_floorUI.GetComponent<UIFloorCreat>().SetUIFloor(obj);                     
            }
        }
    }


}
