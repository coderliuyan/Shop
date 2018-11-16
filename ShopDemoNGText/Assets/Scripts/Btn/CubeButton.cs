using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    Transform _huojia;
    // Use this for initialization
    void Start()
    {
        _huojia = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&transform.tag == "CubeButton")
        {
            _huojia.transform.Rotate(new Vector3(0, 180, 0));
        }

    }
}
