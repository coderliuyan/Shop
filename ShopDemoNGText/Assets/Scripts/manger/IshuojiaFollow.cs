using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IshuojiaFollow : MonoBehaviour {
    public bool _moving;
    private static IshuojiaFollow _instance = null;
    private void Awake()
    {
        _instance = this;
    }
    public static IshuojiaFollow Instance
    {
        get
        {
            return _instance;
        }
    }
  public List<GameObject> _huojia = new List<GameObject>();
    GameObject[] _findhuojia;
	void Start () 
    {
        _moving = true;
	}
	// Update is called once per frame
	void Update () 
    {

        IsFollowHuojia();       
}
    void IsFollowHuojia()
    {
        _findhuojia = GameObject.FindGameObjectsWithTag("huojia");
        if (_findhuojia != null)
        {
            foreach (var item in _findhuojia)
            {
                if (item.GetComponent<Follow>().isMove == true)
                {
                    //Debug.Log(item);
                    _moving = false;
                    //_huojia.Add(item);
                }
                else if (item.GetComponent<Follow>().isMove == false)
                {
                    _moving = true;
                    //item.GetComponent<Follow>().enabled = false;
                   // _huojia.Remove(item);
                }
            }
           // Debug.Log(_huojia.Count);
        }
    }
 }
