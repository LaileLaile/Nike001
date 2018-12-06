using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemManger : MonoBehaviour {
    public AudioItemConfig audio;
    public AudioItem audioitem;
    public GameObject root;

    public GameObject audioObj;
    public GameObject lightObj;
    public GameObject videoObj;
    public GameObject pointObj;
    public GameObject pouseObj;
    public LightItem  lightitem;
    public LightIetmShowConfig light;


    public PointItem pointItem;
    public PointItemShowConfig pointItemShow;

    public VideoItem videoItem;
    public VideoIetmShowConfig video;


    public PauseLine pouseItem;
    public PouseShowConfig pouseItemShow;

    public Image playMask;

   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
  
	}



    public void SetRootActive()
    {
        root.SetActive(true);
    }
    public void ShowAudio(DataBase  data)
    {
        audio.ShowAudioConfig(data.start, data.end ,data .duration ,data .audioname );
    }



    public void ChangeAudioItem(DataBase data)
    {


        audioitem.ChangeMySlef(data);
        audio.ShowAudioConfig(data.start, data.end, data.duration, data.audioname);
    }
    public void showLight(DataBase data)
    {
        light.ShowLightConfig(data);
    }

    public void ChangeLightItem(DataBase data)
    {


        lightitem .ChangeMySlef(data);
        lightitem.ChangeData(data);
        light .ShowLightConfig(data );

    }


    public void showAudio()
    {
        try
        {
        audioObj.SetActive(true);
        lightObj.SetActive(false);
        videoObj.SetActive(false);
        pointObj.SetActive(false);
        pouseObj.SetActive(false);
        }
        catch (System.Exception)
        {

        }

    }
    public void showLight()
    {
        try
        {
        audioObj.SetActive(false );
        lightObj.SetActive(true);
        videoObj.SetActive(false);
        pointObj.SetActive(false);
        pouseObj.SetActive(false);
        }
        catch (System.Exception)
        {

      
        }

    }
    public void showVideo()
    {
        try
        {
        audioObj.SetActive(false );
        lightObj.SetActive(false);
        videoObj.SetActive(true);
        pointObj.SetActive(false);
        pouseObj.SetActive(false);
        }
        catch (System.Exception)
        {


        }

    }

    public void showPoint()
    {
        try
        {
        audioObj.SetActive(false);
        lightObj.SetActive(false);
        videoObj.SetActive(false );
        pointObj.SetActive(true);
        pouseObj.SetActive(false);
        }
        catch (System.Exception)
        {


        }

    }

    public void ShowPouse()
    {
        try
        {
            root.SetActive(true);
        audioObj.SetActive(false);
        lightObj.SetActive(false);
        videoObj.SetActive(false);
        pointObj.SetActive(false);
        pouseObj.SetActive(true);
        }
        catch (System.Exception)
        {


        }

    }

    public void ShowVideo(DataBase data)
    {
        video .ShowVideoConfig(data.start, data.end, data.duration, data.videoName);
    }



    public void ChangeVideoItem(DataBase data)
    {


        videoItem .ChangeMySlef(data);
        video.ShowVideoConfig(data.start, data.end, data.duration, data.videoName);
    }


    public void setPathAndName(DataBase data)
    {
        videoItem.setPathAndName(data);
        video.ShowVideoConfig(videoItem.mydata .start, videoItem.mydata.end, videoItem.mydata.duration, videoItem.mydata.videoName);
    }



    public void disMask()
    {
        playMask.GetComponent<Image>().color = new Color(0, 0, 86 / 255, 1);
    }
    public void ShowMask()
    {
        playMask.GetComponent<Image>().color = new Color(0, 0, 86/255, 1);
    }




    public void ChangePointData(PointItemData data)
    {
        pointItem.ChangeMyself(data);
    }
    public void ShowPointConfig(PointItemData data)
    {
        pointItemShow.ShowPointConfig(data);
    }




    public void ChangePoouse(float time)
    {
        pouseItem.ChangeMyself(time);
    }
    public void ShowPouseConfig(float  data)
    {
        pouseItemShow.ShowPointConfig(data);
    }

}
