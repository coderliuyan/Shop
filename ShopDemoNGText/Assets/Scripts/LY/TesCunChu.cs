using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using LitJson;
public class TesCunChu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Player.ShopStock.Add(1, 2);

        if (Player.ShopStock.ContainsKey(1))
        {
            Player.ShopStock.Remove(1);
            Player.ShopStock.Add(1, 3);
        }

        Player.ShopStock.Add(2, 30);

        if (IJson.WriteJsonToFile("PlayerShopStock", Player.ShopStock))
        {
            Debug.Log("save success");
        }
        else
        {
            Debug.Log("save fail");
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            JsonData data = IJson.LoadJsonWithPath("PlayerShopStock",true);
            foreach(var d in data.Keys)
            {
                Debug.Log(d);
                Debug.Log(data[d]);
            }
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            Player.ShopStock.Clear();
            Player.ShopStock = IJson.LoadJsonWithPath("PlayerShopStock");

            foreach(int a in Player.ShopStock.Keys)
            {
                Debug.Log(a);
                Debug.Log(Player.ShopStock[a]);
            }
        }


	}
}
