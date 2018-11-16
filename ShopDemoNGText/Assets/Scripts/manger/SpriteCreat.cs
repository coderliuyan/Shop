using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCreat : MonoBehaviour {

	// Use this for initialization
    public GameObject sprite;
    Transform spriteParent;

	void Start () 
    {
        spriteParent = transform.Find("shadeParent");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            Test();
        }
	}
    void Test()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject obj = Instantiate(sprite);
                obj.transform.parent = spriteParent;
                obj.transform.localPosition = new Vector3(i * 20 + j * 5, j * 20 + i * 5, 0);
                
            }
        }
    }
}
