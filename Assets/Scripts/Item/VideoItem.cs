using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoItem : OrthogonBase
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowMyconfig);

   

        ShowMyconfig();
        Initbase();

        GetComponent<VideoItem>().ChangeMySlef(GetComponent<VideoItem>().mydata);
        //Debug.LogError(mydata.index);
        // mydata = new DataBase(3);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Play()
    {
        base.Play();

        Debug.LogError(mydata.index);
        TimerManager.Manager.Invoke(mydata.start, StartPlay);
        TimerManager.Manager.VideoInvoke(mydata.end, Stop, mydata.start );

    }

    public void StartPlay()
    {
        if (mydata.videoName != "")
        {
            //  Debug.Log(mydata .start + "    " + mydata.index);
            Globaldata.Global.showItemManger.disMask();
            Globaldata.Global.videomanager.PlayVideo(mydata.videoPath, mydata.index);
         
        }
        isPlay = true;
    }
    public override void Stop()
    {
        Globaldata.Global.videomanager.Stop();
        isPlay = false;
    }


    public override void ShowMyconfig(DataBase data)
    {
        base.ShowMyconfig(data);
        Globaldata.Global.showItemManger.ShowVideo(data);
    }


    public void ShowMyconfig()
    {
        Globaldata.Global.showItemManger.videoItem = GetComponent<VideoItem>();
        Globaldata.Global.showItemManger.showVideo();
        Globaldata.Global.showItemManger.video.videoData = mydata;
        Globaldata.Global.showItemManger.ShowVideo(mydata);
       

    }



    public override void ChangeMySlef(DataBase data)
    {
        base.ChangeMySlef(data);


        mydata.start = data.start;
        mydata.end = data.end;
        mydata.extent = data.extent;
        mydata.duration = data.duration;
        mydata.videoName = data.videoName;
        mydata.videoPath = data.videoPath;
        mydata.index = data.index;


    }


    public void setTime(float timenum)
    {
        mydata .duration = timenum;
        mydata.end = mydata.start + timenum * 100;
        Globaldata.Global.showItemManger.ShowVideo(mydata);
    }


    public void setPathAndName(DataBase data)
    {
        mydata.videoName = data.videoName;
        mydata.videoPath = data.videoPath;
    }
}
