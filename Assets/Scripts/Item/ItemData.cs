
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DataBase
{
    public float start;
    public float end;
    public float duration;
    public float extent;
    public string audioname;
    public string audiopath;


    public int index;
    public string dmxPort;
    public float lightDegreemin;
    public float lightDegreemax;



    public string videoName;

    public string videoPath;
    public int type;
    public DataBase(int datatype)
    {
        start = 0;
        end = 0;
        duration = 0;
        extent = 0;
        audioname = "";
        dmxPort = "";
        index = 1;
        lightDegreemin = 0;
        lightDegreemax = 0;
        videoName = "";
        type = datatype;
        videoPath = "";
    }
    public DataBase()
    {
        start = 0;
        end = 0;
        duration = 0;
        extent = 0;
        audioname = "";
        audiopath = "";
        dmxPort = "";
        index = 1;
        lightDegreemin = 0;
        lightDegreemax = 0;
        videoName = "";
        videoPath = "";

    }




}
public class PointItemData
{
    public int index;
    public float currentTime;
    public float num;
}


public class SavaData


{
    public string DmxCOM;
    public string showTitle;
    public bool isLoop;
    public int second;
    public List<SelfConfig> lights;


    public List<float> pauseTime;

}



public class SelfConfig
{
    //public float sizeX;
    //public float sizeY;
    //public float localpostionX;
    //public float localpostionY;
    public DataBase data;
    public List<pointItemConfig> pointsConfig;



}


public class pointItemConfig
{
    public float localpostionX;
    public float localpostionY;
    public bool isStart;
    public bool isEnd;
    public PointItemData pointdata;
}





public class PauseData
{
    public DataBase data;
    public float currentLight;
    public PauseData(DataBase lightdata, float light)
    {
        data = lightdata;
        currentLight = light;
    }
}


public class Usually
{
    public Usually()
    {

    }
    public Usually(int indexs,float   light)
    {
        index = indexs;
        lightdree = light;
    }
    public int   index;
    public float  lightdree;

}