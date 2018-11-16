using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiangGreat : MonoBehaviour {

    public float Xright;
    public float Yright;
    public float Xleft;
    public float Yleft;
    public int QiangRNum;
    public int QiangLNum;
    GameObject qiangLmanger;
	void Start () 
    {
        qiangLmanger = GameObject.Find("qiangLManger ");
        Xright = 0.68f;
        Yright = -0.305f;
        Xleft = -0.68f;
        Yleft = -0.305f;
        QiangRNum = 6;
        QiangLNum = 6;
        GreatQiang();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void GreatQiang()
    {
        for (int i = 0; i < QiangRNum; i++)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("qiang/qiangR"));
            obj.transform.SetParent(transform);
            float posRX= i * Xright;
            float posRY = i * Yright;
            obj.transform.localPosition = new Vector3(posRX, posRY, 0);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.name = "qiangR" + (i+1);
            obj.GetComponent<SpriteRenderer>().sortingOrder = i+1;
        }
        for (int j = 0; j < QiangLNum; j++)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("qiang/qiangL"));
            obj.transform.SetParent(qiangLmanger.transform);
            float posLX = j * Xleft;
            float posLY = j * Yleft;
            obj.transform.localPosition = new Vector3(posLX,posLY,0);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.name="qiangL"+(j+1);
            obj.GetComponent<SpriteRenderer>().sortingOrder = j + 1;
        }
    }
}
