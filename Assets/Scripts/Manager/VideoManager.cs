using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour {
    DisplayUGUI playugui;
    MediaPlayer playcotorl;
    bool isOpen=true ;
    //public string VideoPath;
    // Use this for initialization
    void Start () {
        if (playugui == null)
        {
            playugui = FindObjectOfType<DisplayUGUI>();

            playcotorl = playugui._mediaPlayer;
          //  playcotorl.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, VideoPath);


        }

    }

    // Update is called once per frame
    void Update () {
   
        if (playcotorl.Control.GetCurrentTimeMs()!=0&&!isOpen )
        {

            Globaldata.Global.showItemManger.video.ChangeDuration(playcotorl.Info.GetDurationMs()/1000);
            isOpen = true;
        }
	}


    public void PlayVideo(string Path,int index)
    {
        for (int i = 0; i < index; i++)
        {
            TimerManager.Manager.RemoveVideo(i);
        }
        playcotorl.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, Path,true );
    }


    public void GetTime(string path)
    {
        playcotorl.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, path, false);
        isOpen = false;
    
    }
    public void Stop()
    {
    
            playcotorl.CloseVideo  ();
       
    }

    public IEnumerator load(string url)
    {

        WWW www = new WWW(url);
        yield return www;
        if (!Globaldata.Global.Isrun)
        {

          
        }
    }
}
