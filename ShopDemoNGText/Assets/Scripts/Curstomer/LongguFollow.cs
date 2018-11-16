using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongguFollow : MonoBehaviour {
    private static LongguFollow _instance = null;
    public GameObject _CustomerMov;
    // Use this for initialization
	void Start () 
    {
	}
    void Awake()
    {
        _instance = this;
    }
    public static LongguFollow Instance
    {
        get
        {
            return _instance;
        }
    }
	// Update is called once per frame
	void Update () 
    {
        if (_CustomerMov != null)
        {
        transform.position = _CustomerMov.transform.position;
        }       
	}
    public void DesLongGu(float _DesTime)
    {
        Destroy(gameObject,_DesTime);
    }
}
