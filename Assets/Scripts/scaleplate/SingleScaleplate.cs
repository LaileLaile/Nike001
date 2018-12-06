﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleScaleplate : MonoBehaviour
{


    public Transform itemDownscale;//小刻度线
    public Transform bigtick;//大刻度线
    public Transform parent;

    private int Timelength = 1000;

    public int index;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i <= Timelength; i++)
        {
            if (i % 100 == 0)
            {
                Transform temp = Instantiate(bigtick.gameObject).transform;
                temp.SetParent(parent);
                temp.localPosition = new Vector3(i - 500, bigtick.localPosition.y);
                temp.localScale = Vector3.one;
                temp.gameObject.SetActive(true);
                temp.Find("Text").GetComponent<Text>().text = (index * 10 + ((i / 100))).ToString();
            }
            else if (i % 10 == 0)
            {
                Transform temp = Instantiate(itemDownscale.gameObject).transform;
                temp.SetParent(parent);
                temp.localPosition = new Vector3(i - 500, itemDownscale.localPosition.y);
                temp.localScale = Vector3.one;
                temp.gameObject.SetActive(true);
            }
        }

        GetComponent<Button>().onClick.AddListener(CreatePause);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CreatePause()
    {
        if (!Globaldata.Global.Isrun)
        {
            GameObject temp = Globaldata.Global.LoadItemPoint(transform, "Item/pause", Input.mousePosition);

            Globaldata.Global.pauseTime.Add(temp.GetComponent<PauseLine>());
            Globaldata.Global.StorePause();
        }

    }
}