using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerThink : MonoBehaviour {
    private static CustomerThink _instance = null;
    Transform Born;//想的图片生成位置
   public Transform Think;
    Transform Pa;//想的图片
    SpriteRenderer Simage;//顾客想的图片动态加载
    public  int Rad;
    GameObject _think;
   //public GameObject _timeCanvas;
   // public int Rad;
	void Start ()
    {
        Born = transform.Find("ThinkBorn");
        Invoke("RandomThink", 2f);
        Rad = Random.Range(0, 4);
        //Rad = 2;
        _think= Resources.Load("Think") as GameObject;
       
	}
    void Awake()
    {
        _instance = this;
    }
    public static CustomerThink Instance
    {
        get
        {
            return _instance;
        }
    }
	// Update is called once per frame
	void Update () 
    {
	}
   public void RandomThink()
    {       
        GameObject ThinkIns = Instantiate(_think, Born.transform.position, Born.transform.rotation);
        ThinkIns.transform.parent = Born.transform;
        Think = Born.Find("Think(Clone)");
        Pa = Think.Find("Pacture");
        Simage = Pa.GetComponent<SpriteRenderer>();
        switch (Rad)
        {         
            case 0:
               Texture2D img = Resources.Load("ThinkPa/004") as Texture2D;
              Sprite pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
              Simage.sprite = pic;
                break;
            case 1:
                Texture2D img1 = Resources.Load("ThinkPa/003") as Texture2D;
              Sprite pic1 = Sprite.Create(img1, new Rect(0, 0, img1.width, img1.height), new Vector2(0.5f, 0.5f));
              Simage.sprite = pic1;
                break;
            case 2:
                Texture2D img2 = Resources.Load("ThinkPa/001") as Texture2D;
                Sprite pic2 = Sprite.Create(img2, new Rect(0, 0, img2.width, img2.height), new Vector2(0.5f, 0.5f));
                Simage.sprite = pic2;
                break;
            case 3:
                 Texture2D img3 = Resources.Load("ThinkPa/002") as Texture2D;
                Sprite pic3 = Sprite.Create(img3, new Rect(0, 0, img3.width, img3.height), new Vector2(0.5f, 0.5f));
                Simage.sprite = pic3;
                break;
        }
        
        
    }
}
