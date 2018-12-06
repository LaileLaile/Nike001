using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class AudioItem : OrthogonBase
{

    // Use this for initialization
    private AudioSource audioplay;
    public float nowPlayTime;
    private bool ispause;

    // public AudioClip audioclip;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ShowMyconfig);
        audioplay = GetComponent<AudioSource>();
        // audiodata = new DataBase();
        ShowMyconfig();
        Initbase();
        //mydata = new DataBase(2);
    }

    public void SetClip(AudioClip clip)
    {
        audioplay.clip = clip;
        mydata.end = mydata.start + clip.length * 100;
        mydata.duration = clip.length;
        Globaldata.Global.showItemManger.ShowAudio(mydata);
    }

    // Update is called once per frame
    void Update()
    {
        // updateevent();
        // TimeLine();
        //Debug.LogError(mydata.type);
        if (isPlay)
        {
            if (Globaldata.Global.isPause)
            {
                if (ispause != Globaldata.Global.isPause)
                {
                    if (nowPlayTime ==0)
                    {
                        nowPlayTime = audioplay.time;
                    }
                    audioplay.Stop();

                    ispause = Globaldata.Global.isPause;
                }

            }
            else
            {
                if (ispause != Globaldata.Global.isPause)
                {
                    StartPlay();
                    ispause = Globaldata.Global.isPause;
                }
            }
        }

    }


    public override void ShowMyconfig(DataBase data)
    {
        base.ShowMyconfig(data);
        Globaldata.Global.showItemManger.ShowAudio(data);
    }
    //public override void TimeLine()
    //{
    //    base.TimeLine();
    //    if (Globaldata.Global.second >= mydata.start && !isPlay)
    //    {
    //        if (audioplay.clip != null)
    //        {
    //            audioplay.Play();
    //        }
    //        isPlay = true;
    //    }

    //}
    public override void Play()
    {
        base.Play();
        ispause = false;
        TimerManager.Manager.Invoke(mydata.start, StartPlay);
        TimerManager.Manager.Invoke(mydata.end, Stop);

    }

    public void StartPlay()
    {
        if (audioplay.clip != null&&!Globaldata.Global.isPause)
        {
            audioplay.Stop();
            audioplay.time = nowPlayTime;
            audioplay.Play();
        }
        isPlay = true;
    }
    public override void Stop()
    {
        base.Stop();
        audioplay.Stop();
        isPlay = false;
    }
    public void ShowMyAudio()
    {

    }

    public void ShowMyconfig()
    {
        Globaldata.Global.showItemManger.audioitem = GetComponent<AudioItem>();
        Globaldata.Global.showItemManger.showAudio();
        Globaldata.Global.showItemManger.audio.audiodata = mydata;
        Globaldata.Global.showItemManger.ShowAudio(mydata);

    }

    public override void ChangeMySlef(DataBase data)
    {
        base.ChangeMySlef(data);
        mydata.start = data.start;
        mydata.end = data.end;
        mydata.extent = data.extent;
        mydata.duration = data.duration;
        mydata.audioname = data.audioname;


    }
    public override SelfConfig ReturnSelfConfig()
    {
        SelfConfig self = new SelfConfig();
        //self.sizeX = GetComponent<RectTransform>().sizeDelta.x ;
        //self.sizeY = GetComponent<RectTransform>().sizeDelta.y;
        //self.localpostionX= transform.localPosition.x ;
        //self.localpostionY= transform.localPosition.y;
        self.data = mydata;
        self.data.type = 2;

        List<pointItemConfig> listconfig = new List<pointItemConfig>();



        self.pointsConfig = listconfig;

        return self;




    }
}
