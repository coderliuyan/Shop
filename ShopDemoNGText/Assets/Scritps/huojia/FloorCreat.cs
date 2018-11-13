using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCreat : MonoBehaviour {

    public float iLing = -1.386f;
    public float iRow = -0.618f;
    public float jLing = 1.386f;
    public float jRow = -0.618f;
    GameObject par;
   public  int FloorHightLength;
   public  int FloorWidthLength;
    void Start()
    {
        par = GameObject.Find("地板空节点");
        FloorHightLength = 13;
        FloorWidthLength = 13;
        CreatFloor();
      
    }
    public void CreatFloor()
    {
        for (int i = 0; i < FloorHightLength; i++)
        {
            for (int j = 0; j < FloorWidthLength; j++)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("floor/New Sprite"));
                obj.transform.SetParent(par.transform);
                float posX = i * (iLing) + j * (jLing);
                float posY = i * (iRow) + j * (jRow);
                obj.transform.localPosition = new Vector3(posX, posY, 0);
                obj.transform.localRotation = Quaternion.identity;
                obj.name ="行"+(i+1)+"列"+(j+1) ;
                //SetFloorColor(obj,(byte)(Random.Range(0,6))  );
            }
        }
    }

}
