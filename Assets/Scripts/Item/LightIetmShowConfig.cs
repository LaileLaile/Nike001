
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightIetmShowConfig : MonoBehaviour
{
    public DataBase lightdata;
    public InputField starttime;
    public InputField endTime;
    public InputField extentTime;
    public InputField index;
    public InputField changeIntervalmin;
    public InputField changeIntervalmax;
    // Use this for initialization
    void Start()
    {
        lightdata = new DataBase();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangestartVaule()
    {
        if (starttime.text != "" && endTime.text != "" && extentTime.text != "")
        {
            endTime.text = (float.Parse(starttime.text) + float.Parse(extentTime.text)).ToString();
        }

    }


    public void ChangeConfig()
    {
        if (starttime.text != "")

        {
            lightdata.start = float.Parse(starttime.text) * 100;
            lightdata.end = float.Parse(endTime.text) * 100;

            lightdata.extent = float.Parse(extentTime.text);

            lightdata.index = int.Parse(index.text);
            lightdata.lightDegreemin = float.Parse(changeIntervalmin.text);
            lightdata.lightDegreemax = float.Parse(changeIntervalmax.text);

            Globaldata.Global.showItemManger.lightitem.BreakParent();
            Globaldata.Global.showItemManger.ChangeLightItem(lightdata);
            Globaldata.Global.showItemManger.lightitem.LightChange();
            Globaldata.Global.showItemManger.lightitem.StopLightChange();

        }




    }


    public void ShowLightConfig(DataBase data)
    {

        starttime.text = (data.start / 100).ToString();
        endTime.text = (data.end / 100).ToString();

        extentTime.text = ((data.end - data.start) / 100).ToString();

        index.text = data.index.ToString();
        changeIntervalmin.text = data.lightDegreemin.ToString();
        changeIntervalmax.text = data.lightDegreemax.ToString();
        // ChangeConfig();
    }
}