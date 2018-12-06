using ETC.Platforms;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.Video;

public class TestLight : MonoBehaviour
{

 //   private DMX dmx;
  //  public string ComPort = "COM8";


    public Slider[] sliders;
    PlayableDirector playable ;
    public Transform lightroot;

    public Videocontorl[] videos;
     

    public Light[] lights;
    

    public List<ActivationTrack> Lightactivas=new List<ActivationTrack> ();
    public List<ActivationTrack> videoActivas = new List<ActivationTrack>();

    // Use this for initialization
    public void setLight(int index)
    {
        Dmxmanager.GetDmx ().ChoseContralLight(index +1, (sliders[index].value * 255));
        //this.dmx.Channels[2]= (byte)(slider.value * 255);
        //this.dmx.Channels[3] = (byte)(slider.value * 255);

    }
    private void Awake()
    {
        //for (int i = 0; i < l_ctrl.Length ; i++)
        //{
        //    l_ctrl[i] = new Light();
        //}
    }

    public  object BytesToObject(byte[] Bytes)
    {
        using (MemoryStream ms = new MemoryStream(Bytes))
        {
            IFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(ms);
        }
    }

    void Start()
    {
        byte[] bytes = File.ReadAllBytes(Application.streamingAssetsPath + "/AVProVideoSamples/AVPro Video.prefab");   

       

        playable = GetComponent<PlayableDirector>();

        

        try
        {

            IEnumerable<PlayableBinding> playableBindings = playable.playableAsset.outputs;
            
            List<PlayableBinding> plays = playableBindings.ToList();
            for (int i = 0; i < plays.Count ; i++)
            {
                try
                {

                  
    ActivationTrack active = plays[i].sourceObject as ActivationTrack;
                    if (active !=null )
                    {
                        string s = "";
                        try
                        {
                            s= active.GetGroup().name; 
                        }
                        catch (System.Exception)
                        {

                       
                        }
                        if (s!= "Light")
                        {

                            Debug.Log("active" + active.name);
                            lights[Lightactivas.Count].start = active.start;

                            lights[Lightactivas.Count].end = active.end;

                     
                            
                            Lightactivas.Add(active);
                        }
                        else
                        {
                           videos[videoActivas.Count].getTime();
                            


                        }
              
                    }
        
                }
                catch (System.Exception)
                {

                    throw;
                }
            
            }
        }
        catch (System.Exception e)
        {

            throw;
        }


      
      //  dmx = new DMX(ComPort);
    }
  

    public void startOpenLight()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < l_ctrl.Length; i++)
        //{
        //    if (l_ctrl[i].isOpen)
        //    {


        //        if (l_ctrl[i].lightDegree <= 0)
        //        {
        //            islimitation[i] = true;
        //        }
        //        else if (l_ctrl[i].lightDegree >= 255)
        //        {
        //            islimitation[i] = false;
        //        }
        //        if (islimitation[i])
        //        {
        //            l_ctrl[i].lightDegree += 0.5f;
        //        }
        //        else
        //        {
        //            l_ctrl[i].lightDegree -= 0.5f;
        //        }

        //        dmx.Channels[i + 1] = (byte)l_ctrl[i].lightDegree;
        //        dmx.Send();
        //        Debug.Log(l_ctrl[0].lightDegree);
        //        isclose[i] = true;
        //    }
        //    else
        //    {
        //        if (isclose[i])
        //        {
        //            dmx.Channels[i + 1] = (byte)0;
        //            l_ctrl[i].lightDegree = 0;
        //            dmx.Send();
        //        }

        //        isclose[i] = false;
        //    }
        //}
    }


    //public IEnumerator ContralLight(int index, float dealytime)
    //{
    //    yield return new WaitForSeconds(dealytime);

    //    l_ctrl[index].isOpen = l_ctrl[index].isOpen = !l_ctrl[index].isOpen; ;
    //}

    public void ChoseContralLight(int index,float lightDegree)
    {
       // dmx.Channels[index ] = (byte)lightDegree;
       //     dmx.Send();
    }





    public void CloseAll()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].Close();
        }

    }


    private void OnApplicationQuit()
    {
       
    }
}
