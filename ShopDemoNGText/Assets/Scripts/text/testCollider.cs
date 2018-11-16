using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("触发触发触发"+collider);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("碰撞碰撞，碰撞"+collision);
    }
}
