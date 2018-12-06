using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleplateManager : MonoBehaviour {

    public int scend=10;

    private int sc= 1920;
    private RectTransform rect;
    public RectTransform otherrect;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        for (int i = 0; i < scend ; i++)
        {
            GameObject temp = Instantiate(Resources.Load("scaleplate/sonscaleplate")) as GameObject ;
            temp.transform.SetParent(transform);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = new Vector3(i * 1000 - 460, 501.5f);
            if (sc<i*1000)
            {
                if (i==scend -1)
                {
                    sc += 1000;
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x + 1000+250, rect.sizeDelta.y);
                }
                else
                {
                    sc += 1000;
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x + 1000, rect.sizeDelta.y);
                }
                //otherrect.localPosition  = rect.localPosition ;
                otherrect.sizeDelta =new Vector2 ( rect.sizeDelta.x ,otherrect .sizeDelta .y );
            }
            temp.GetComponent<SingleScaleplate>().index = i;
            Globaldata.Global.scaliplate.Add(temp.GetComponent<SingleScaleplate>());

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
