using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointItemShowConfig : MonoBehaviour
{

    public InputField curretTime;
    public InputField index;
    public InputField lightNum;
    public PointItemData pointData;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPointConfig(PointItemData data)
    {

        curretTime.text = (data.currentTime / 100).ToString();
        index.text = (data.index).ToString();

        lightNum.text = (data.num).ToString();

        index.text = data.index.ToString();

        // ChangeConfig();
    }


    public void ChangePointConfig()
    {

        pointData.currentTime = float.Parse(curretTime.text) * 100;


        pointData.num = float.Parse(lightNum.text);


        Globaldata.Global.showItemManger.ChangePointData(pointData);
        Globaldata.Global.showItemManger.lightitem.BreakParent();

        Globaldata.Global.showItemManger.lightitem.LightChange();
        Globaldata.Global.showItemManger.lightitem.StopLightChange();
 
    }
}
