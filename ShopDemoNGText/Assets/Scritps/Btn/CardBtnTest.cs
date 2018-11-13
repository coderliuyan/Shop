using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBtnTest : MonoBehaviour {


    Vector3 screenposition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionWorld;
    BoxCollider _MyCollider;
    GameObject _canvans;
    Follow _ismove;
    //GameObject shafa;
    // Use this for initialization
    void Start()
    {
        _MyCollider = gameObject.GetComponent<BoxCollider>();
        _canvans = GameObject.Find("UI Root (1)");
    }

    // Update is called once per frame
    void Update()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
            }
    }
    void OnClick()
    {   
        //Debug.Log("OnClick");
        if (_MyCollider.tag == "changtougui")
        {

            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_huazhuangpin"), mousePositionWorld, Quaternion.identity);
                gameObject.SetActive(false);              
                //obj.GetComponent<Follow>().OnMouseDownTrue(); 
            //Debug.Log(_MyCollider.name);
          
            //GameObject UIhuojia = (GameObject)Instantiate(Resources.Load("huojia/huojiaUI"));
            //UIhuojia.transform.parent = _Longgucanvans.transform;
            //UIhuojia.GetComponent<HuojiaUI>()._UIHuojia = obj.transform;
        }
        if (_MyCollider.tag == "sofa")
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia__shechipin"), mousePositionWorld, Quaternion.identity);
            gameObject.SetActive(false);
           
            GameObject UIButton = (GameObject)Instantiate(Resources.Load("HuojiaButton/huo_shechipinButton"));
            UIButton.transform.parent = _canvans.transform;
           // UIButton.GetComponent<UIArmaMove>().UIPoint = obj.transform;
            obj.GetComponent<Follow>().OnMouseDownTrue();
        }
        if (_MyCollider.tag == "guizi")
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_choose"), mousePositionWorld, Quaternion.identity);
            gameObject.SetActive(false);
            obj.GetComponent<Follow>().OnMouseDownTrue();
            //obj.GetComponent<MeshRenderer>().enabled = false;
            //GameObject UIhuojia = (GameObject)Instantiate(Resources.Load("huojia/huojiaUI 3"));
            //UIhuojia.transform.parent = _Longgucanvans.transform;
            //UIhuojia.GetComponent<HuojiaUI>()._UIHuojia = obj.transform;

        }
    }
}
