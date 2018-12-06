using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Videocontorl : MonoBehaviour
{
    DisplayUGUI playugui;
    MediaPlayer playcotorl;
    public string VideoPath;
    // Use this for initialization
    void Awake()
    {


        if (playugui == null)
        {
            playugui = GetComponent<DisplayUGUI>();

            playcotorl = playugui._mediaPlayer;
            playcotorl.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, VideoPath);
           
      
        }
        
        playcotorl.Play();
        playcotorl.m_Muted  = false  ;

    }

    public void Changvido()
    {
    
        playcotorl.Control.Play();
    }
    public void getTime()
    {

        if (playugui == null)
        {
            playugui = GetComponent<DisplayUGUI>();

            playcotorl = playugui._mediaPlayer;
            playcotorl.m_Muted = true  ;
            playcotorl.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, VideoPath,false );

            playcotorl.Play();
           
              

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        //playcotorl.Control.Play();
    }
}
