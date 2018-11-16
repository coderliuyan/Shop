using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManger : MonoBehaviour {

	// Use this for initialization
    private static GameDataManger _instance = null;
    public Dictionary<int,GameObject> floorInit = new Dictionary<int,GameObject>();
    public Dictionary<int, GameObject> floorSencond = new Dictionary<int, GameObject>();
    public Dictionary<int,GameObject> floor=new Dictionary<int,GameObject>();
    public Dictionary<int,GameObject> huojia=new Dictionary<int,GameObject>();
    public Dictionary<int,GameObject> huojiaNullObj=new Dictionary<int,GameObject>();
    public Dictionary<int, GameObject> LujingPoint=new Dictionary<int, GameObject>();
    public List<int> _LongguCustomerId = new List<int>();
    public List<int> _huojiaUIId = new List<int>();
	void Start () 
    {
        _LongguCustomerId.Add(3001);
        
	}
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    public static GameDataManger Instance
    {
        get
        {
            return _instance;
        }
    }
    public void SetLujing(int _selfFloorId,GameObject _objFloor)
    {
    if (!GameDataManger.Instance.LujingPoint.ContainsKey(_selfFloorId))
        {
        GameDataManger.Instance.LujingPoint.Add(_selfFloorId, _objFloor);
        }
    }
    public void RemoveLujing(int _Floorid)
    {
    if (GameDataManger.Instance.LujingPoint.ContainsKey(_Floorid))
        {
        GameDataManger.Instance.LujingPoint.Remove(_Floorid);
        }
    }
}
