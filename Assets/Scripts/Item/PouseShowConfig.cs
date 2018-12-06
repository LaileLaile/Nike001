using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PouseShowConfig : MonoBehaviour {

    public float pouseTime;
    public InputField pouse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void ShowPointConfig(float data)
    {
        pouse.text = data.ToString ();
    }

    public void ChangeConfig()
    {
        pouseTime = float.Parse(pouse.text);
        Globaldata.Global.showItemManger.ChangePoouse(pouseTime);
    }
}
