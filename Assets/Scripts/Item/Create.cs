
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public int Type;
    public Transform parent;
    public RectTransform parentRect;

    public Vector2 parentPos;
    // Use this for initialization
    void Awake()
    {
        parentRect = parent.GetComponent<RectTransform>();
        parentPos = parentRect.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    parentRect.localPosition = parentPos;
        //    CreateAudio();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (!Globaldata.Global.Isrun)
        //    {
        //        parentRect.localPosition = parentPos;
        //        CreateLight();
        //    }

        //}

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    parentRect.localPosition = parentPos;
        //    CreateVideo();
        //}
        if (/*Input.GetKeyDown(KeyCode.LeftControl)&&*/ Input.GetKeyDown(KeyCode.C))
        {
            Copy();
        }

        if (/*Input.GetKeyDown(KeyCode.LeftControl) &&*/ Input.GetKeyDown(KeyCode.V))
        {
            Paste();
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateAudio();
        }
    }


    public void CreateAudio()
    {
        Globaldata.Global.showItemManger.SetRootActive();
        float y = Globaldata.Global.animCount * -130f - 50;
        Vector3 vec = new Vector3(0, y);
        GameObject temp = Globaldata.Global.LoadItem(parent, "Item/AudioItem", vec);
        //temp.GetComponent<AudioItem>() = new DataBase();
        temp.GetComponent<AudioItem>().mydata.type = 2;
        Globaldata.Global.orthogons.Add(temp.GetComponent<AudioItem>());
        if (y < -400)
        {
            parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 130);
        }


        Globaldata.Global.animCount++;
    }


    public void CreateLight()
    {
        for (int i = 0; i < 1; i++)
        {
            Globaldata.Global.showItemManger.SetRootActive();
            float y = Globaldata.Global.animCount * -130f - 50;
            Vector3 vec = new Vector3(0, y);
            GameObject temp = Globaldata.Global.LoadItem(parent, "Item/LightItem", vec);
            temp.GetComponent<LightItem>().mydata.type = 1;
            Globaldata.Global.orthogons.Add(temp.GetComponent<LightItem>());
            if (y < -400)
            {
                parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 130);
            }

            Globaldata.Global.animCount++;


        }







    }

    public void Copy()
    {
        if (Globaldata.Global.copyTemp != null)
        {
            Destroy(Globaldata.Global.copyTemp.gameObject);
            Destroy(Globaldata.Global.copyTemp);
        }
        if (Globaldata.Global.showItemManger.lightitem != null)
        {
            Globaldata.Global.copyTemp = Copy(Globaldata.Global.showItemManger.lightitem);




        }


    }

    public void Paste()
    {
        if (Globaldata.Global.copyTemp != null&& !Globaldata.Global .isPause )
        {
            Globaldata.Global.showItemManger.SetRootActive();
            float y = Globaldata.Global.animCount * -130f - 50;
            Vector3 vec = new Vector3(0, y);

            GameObject temp = Globaldata.Global.LoadItem(parent, "Item/LightItem", vec);

            temp.GetComponent<LightItem>().mydata = GetLight((LightItem)Globaldata.Global.copyTemp).mydata;
            temp.GetComponent<LightItem>().mydata.index = Globaldata.Global.animCount;
            temp.GetComponent<LightItem>().iscopy = true;
            Globaldata.Global.showItemManger.light.ChangeConfig();
            Globaldata.Global.orthogons.Add(temp.GetComponent<LightItem>());
            if (y < -400)
            {
                parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 130);
            }

            Globaldata.Global.animCount++;
        }

        //Globaldata.Global.showItemManger.light.ChangeConfig();
        //Globaldata.Global.showItemManger.light.ShowLightConfig(Globaldata.Global.showItemManger.lightitem.mydata);


    }

    public void CreateVideo()
    {
        Globaldata.Global.showItemManger.SetRootActive();
        float y = Globaldata.Global.animCount * -130f - 50;
        Vector3 vec = new Vector3(0, y);
        GameObject temp = Globaldata.Global.LoadItem(parent, "Item/VideoItem", vec);
        Globaldata.Global.showItemManger.videoItem = temp.GetComponent<VideoItem>();
        Globaldata.Global.showItemManger.videoItem.mydata.type = 3;
        Globaldata.Global.orthogons.Add(temp.GetComponent<VideoItem>());
        temp.GetComponent<VideoItem>().mydata.index = Globaldata.Global.animCount;
        if (y < -400)
        {
            parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 130);
        }

        Globaldata.Global.animCount++;

    }


    public LightItem Copy(LightItem item)
    {

        GameObject obj = new GameObject("CopyTemp");
        obj.SetActive(false);
        obj.AddComponent<LightItem>();
        LightItem temp = obj.GetComponent<LightItem>();
        temp.mydata = item.mydata;
        temp.points = item.points;
        temp.LineIndex = -1;

        return temp;

    }


    public LightItem GetLight(LightItem item)
    {
        LightItem temp = new LightItem();
        temp.mydata.start = item.mydata.start;
        temp.mydata.end = item.mydata.end;
        temp.mydata.duration = item.mydata.duration;
        temp.mydata.extent = item.mydata.extent;
        temp.mydata.audioname = item.mydata.audioname;
        temp.mydata.audiopath = item.mydata.audiopath;
        temp.mydata.index = item.mydata.index;
        temp.mydata.dmxPort = item.mydata.dmxPort;
        temp.mydata.lightDegreemin = item.mydata.lightDegreemin;
        temp.mydata.lightDegreemax = item.mydata.lightDegreemax;
        temp.mydata.videoName = item.mydata.videoName;
        temp.mydata.videoPath = item.mydata.videoPath;
        temp.mydata.type = item.mydata.type;
        return temp;
    }


    public void Init(DataBase data, List<pointItemConfig> pointsConfig)
    {

        Globaldata.Global.showItemManger.SetRootActive();
        Globaldata.Global.showItemManger.showAudio();
        if (data .type ==2)
        {
            float y = Globaldata.Global.animCount * -130f - 50;
            Vector3 vec = new Vector3(0, y);

            GameObject temp = Globaldata.Global.LoadItem(parent, "Item/AudioItem", vec);

            temp.GetComponent<AudioItem >().mydata = data;



            Globaldata.Global.showItemManger.audioitem = temp.GetComponent<AudioItem>();
          
            Globaldata.Global.orthogons.Add(temp.GetComponent<AudioItem >());
            Globaldata.Global.showItemManger.audio.BackLoadAudio(data.audiopath );
            Globaldata.Global.showItemManger.audio.ChangeConfig();
            if (y < -400)
            {
                parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 130);
            }

            Globaldata.Global.animCount++;
        }
        else
        {
            float y = Globaldata.Global.animCount * -130f - 50;
            Vector3 vec = new Vector3(0, y);

            GameObject temp = Globaldata.Global.LoadItem(parent, "Item/LightItem", vec);

            temp.GetComponent<LightItem>().mydata = data;

            temp.GetComponent<LightItem>().iscopy = true;

            temp.GetComponent<LightItem>().pointsConfig = pointsConfig;


            Globaldata.Global.showItemManger.light.ChangeConfig();
            Globaldata.Global.orthogons.Add(temp.GetComponent<LightItem>());
            if (y < -400)
            {
                parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y + 130);
            }

            Globaldata.Global.animCount++;
        }
       


        //Globaldata.Global.showItemManger.light.ChangeConfig();
        //Globaldata.Global.showItemManger.light.ShowLightConfig(Globaldata.Global.showItemManger.lightitem.mydata);


    }
}