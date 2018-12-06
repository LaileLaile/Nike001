
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class Globaldata
{

    private static Globaldata global;


    public ShowItemManger showItemManger;//显示音乐 灯光信息管理

    public ActionEvent eventmanger;

    public VideoManager videomanager;

    public int animCount;//时间线计数

    public float second;//运行时间

    public bool Isrun;//是否在运行

    public float maxEndTime;//结束时间


    public string dmxPort = "COM4";//dmx端口号

    public List<OrthogonBase> orthogons = new List<OrthogonBase>();//所有的轨道

    public Dmxmanager dmxmanager;


    public List<OrthogonBase> startplays = new List<OrthogonBase>();
    public List<OrthogonBase> stopPlays = new List<OrthogonBase>();

    public LightItem currentLight;

    public LightItem currentPoint;

    public DrawLine lineManger;

    public Vector3 top;
    public Vector3 bottom;


    public OrthogonBase copyTemp;

    public List<DataBase> tempSaveData = new List<DataBase>();

    public const float LIGHT = 255;


    public Transform pointer;
    public List<SingleScaleplate> scaliplate = new List<SingleScaleplate>();

    public bool isPause;

    public int Current = -1;

    public List<PauseLine> pauseTime = new List<PauseLine>();

    public DataBase copydata;

    public static Globaldata Global
    {
        get
        {
            if (global == null)
            {
                global = new Globaldata();
            }
            return global;
        }

    }

    public void Startplay()
    {
        OrthogonBase[] temp = new OrthogonBase[orthogons.Count];
        orthogons.CopyTo(temp);
        startplays = temp.ToList();
    }



    public GameObject LoadItem(Transform parent, string path, Vector3 vector)//加载物体方法
    {
        Transform temp = (GameObject.Instantiate(Resources.Load(path)) as GameObject).transform;
        temp.SetParent(parent);
        temp.localScale = Vector3.one;
        temp.localPosition = vector;
        return temp.gameObject;
    }

    public GameObject LoadItemPoint(Transform parent, string path, Vector3 vector)//加载物体方法
    {
        Transform temp = (GameObject.Instantiate(Resources.Load(path)) as GameObject).transform;
        temp.SetParent(parent);
        temp.localScale = Vector3.one;
        temp.position = vector;
        return temp.gameObject;
    }



    public GameObject CopyLoadItemPoint(Transform parent, string path, Vector3 vector)//加载物体方法
    {
        Transform temp = (GameObject.Instantiate(Resources.Load(path)) as GameObject).transform;
        temp.SetParent(parent);
        temp.localScale = Vector3.one;
        temp.localPosition = vector;
        return temp.gameObject;
    }

    //  public GameObject CopyLoadItemPouse(Transform parent, string path, Vector3 vector)//加载物体方法
    //  {
    //      Transform temp = (GameObject.Instantiate(Resources.Load(path)) as GameObject).transform;
    //temp.position  = vector;


    //      temp.SetParent(parent);
    //      temp.localScale = Vector3.one;
    //      return temp.gameObject;
    //  }
    public void Stop()//停止播放
    {
        try
        {
            Isrun = false;
            isPause = false;
            if (copydata != null)
            {
                copyTemp.mydata = Clone(copydata);
            }
            for (int i = 0; i < orthogons.Count; i++)
            {
                orthogons[i].mydata = tempSaveData[i];
                try
                {
                    if (orthogons[i].isPlay)
                    {
                        orthogons[i].Stop();

                    }
                }
                catch (Exception e)
                {
                    int s = 0;

                }


            }
            tempSaveData = new List<DataBase>();
            TimerManager.Manager.Clear();
            second = 0;

        }
        catch (Exception)
        {


        }

    }

    public void Start()
    {
        Globaldata.Global.Isrun = true;
        Globaldata.Global.second = 0;
        List<DataBase> temp = new List<DataBase>();
        if (copyTemp != null)
        {
            copydata = Clone(copyTemp.mydata);
        }
        PauseData();
        for (int i = 0; i < orthogons.Count; i++)
        {
            DataBase data = CopyData(orthogons[i].mydata);
            temp.Add(data);
            orthogons[i].Play();

        }
        Current = -1;
        for (int i = 0; i < temp.Count; i++)
        {
            tempSaveData.Add(Clone(temp[i]));
        }


        for (int i = 0; i < pauseTime.Count; i++)
        {
            pauseTime[i].pauseData.Clear();
        }
        //tempSaveData.Clear();
        //tempSaveData = temp.ToList();
        //Startplay();
    }

    public void SetDmxPort(string str)
    {

        Dmxmanager.GetDmx().ChangePort(str);
    }


    public void MaxTime()
    {
        List<float> f_Ends = new List<float>();
        for (int i = 0; i < orthogons.Count; i++)
        {
            f_Ends.Add(orthogons[i].returnEndtime());
        }

        maxEndTime = f_Ends.Max();
    }



    public void DrawLine(List<Transform> trans)
    {
        List<Vector2> pos = new List<Vector2>();

        for (int i = 0; i < trans.Count; i++)
        {
            pos.Add(trans[i].position);
        }
        for (int i = 0; i < pos.Count; i++)
        {
            Vector2 t = pos[i];

            int j = i;
            while ((j > 0) && (pos[j - 1].x > t.x))
            {

                pos[j] = pos[j - 1];
                --j;
            }

            pos[j] = t;
        }
        if (currentLight.LineIndex == -1)
        {
            currentLight.LineIndex = lineManger.AddLine(pos);
        }
        else
        {
            lineManger.Draw(currentLight.LineIndex, pos);
        }

    }
    public void DrawLine(List<Transform> trans, int index)
    {
        List<Vector2> pos = new List<Vector2>();

        for (int i = 0; i < trans.Count; i++)
        {
            pos.Add(trans[i].position);
        }
        for (int i = 0; i < pos.Count; i++)
        {
            Vector2 t = pos[i];

            int j = i;
            while ((j > 0) && (pos[j - 1].x > t.x))
            {

                pos[j] = pos[j - 1];
                --j;
            }

            pos[j] = t;
        }
        if (index != -1)
        {
            lineManger.Draw(index, pos);

        }


    }
    public void SortePoint(List<PointItem> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            PointItem t = points[i];

            int j = i;
            while ((j > 0) && (points[j - 1].data.currentTime > t.data.currentTime))
            {

                points[j] = points[j - 1];
                --j;
            }

            points[j] = t;
        }

    }
    public void choseDis(int index)
    {
        lineManger.Drawsetactive(index);
    }
    //深度拷贝
    public T Clone<T>(T RealObject)
    {
        using (Stream stream = new MemoryStream())
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, RealObject);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(stream);
        }


    }



    public DataBase CopyData(DataBase RealObject)
    {


        DataBase data = new DataBase();
        data.audioname = Clone(RealObject.audioname);
        data.dmxPort = Clone(RealObject.dmxPort);
        data.duration = Clone(RealObject.duration);
        data.end = Clone(RealObject.end);
        data.extent = Clone(RealObject.extent);
        data.index = Clone(RealObject.index);
        data.lightDegreemax = Clone(RealObject.lightDegreemax);
        data.lightDegreemin = Clone(RealObject.lightDegreemin);
        data.start = Clone(RealObject.start);
        data.type = Clone(RealObject.type);
        data.videoName = Clone(RealObject.videoName);
        data.videoPath = Clone(RealObject.videoPath);

        return data;



    }
    public T Clone2<T>(T RealObject)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, RealObject);
        stream.Position = 0;
        return (T)formatter.Deserialize(stream);


        //T  obj = JsonUtility.FromJson<T>(JsonUtility.ToJson (RealObject));


        //return obj;



    }


    public void DeleteLight(LightItem light)
    {
        int deleteindex = orthogons.IndexOf(light);

        for (int i = deleteindex + 1; i < orthogons.Count; i++)
        {
            orthogons[i].transform.position = new Vector3(orthogons[i].transform.position.x, orthogons[i].transform.position.y + 130);
        }

        //try
        //{
        //    ((LightItem)orthogons[deleteindex - 1]).ShowMyconfig();
        //}
        //catch (Exception)
        //{


        //}
        light.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(light.transform.parent.GetComponent<RectTransform>().sizeDelta.x, light.transform.parent.GetComponent<RectTransform>().sizeDelta.y - 130);
        OrthogonBase temp = orthogons[deleteindex];
        orthogons.Remove(orthogons[deleteindex]);
        lineManger.LineList[((LightItem)temp).LineIndex].active = false;
        temp.Delete();
        //if (copyTemp ==light )
        //{
        //    copyTemp
        //}
        if (copyTemp != null)
        {
            copyTemp.Delete();
        }

        animCount--;
    }




    public void LastPause()
    {
        //List<float> times = new List<float>();
        //List<int> indexs = new List<int>();

        //for (int i = 0; i < pauseTime.Count; i++)
        //{
        //    if (pauseTime[i].time * 100 < Globaldata.global.second)
        //    {
        //        float temptime = Globaldata.global.second - pauseTime[i].time * 100;
        //        times.Add(temptime);
        //        indexs.Add(i);
        //    }

        //}
        //if (times.Count > 0)
        //{
        //    float min = times.Min();
        //    int index = times.IndexOf(min);
        //    TimerManager.Manager.ResultPause();
        //    for (int i = index; i < pauseTime.Count; i++)
        //    {
        //        pauseTime[i].StartPlay();
        //    }

        //    Globaldata.global.second = pauseTime[index].time * 100;
        //}
        if (Current != -1 && isPause && Current != 0)
        {
            //if (isPause)
            //{
            StorePause();
            TimerManager.Manager.ResultPause();

            for (int i = Current; i < pauseTime.Count; i++)
            {
                pauseTime[i].StartPlay();
            }
            Current -= 1;
            if (Current < 0)
            {
                Current = 0;
            }
            Globaldata.global.second = pauseTime[Current].time * 100;













            for (int i = 0; i < orthogons.Count; i++)
            {
                try
                {
                    LightItem temp = (LightItem)orthogons[i];
                    if (temp.points[0].data.currentTime >= Globaldata.global.second)
                    {
                        temp.StartPlay();
                    }
                    else if (temp.points[temp.points.Count - 1].data.currentTime > Globaldata.global.second)
                    {
                        int index = -1;
                        for (int j = 0; j < temp.points.Count; j++)
                        {
                            if (temp.points[j].data.currentTime > Globaldata.global.second)
                            {
                                index = j;
                                break;
                            }
                        }
                        temp.StartPlay(index);
                    }
                }
                catch (Exception)
                {

                    AudioItem temp = (AudioItem)orthogons[i];
                    if (temp.mydata.start + temp.nowPlayTime * 100 >= Globaldata.global.second)
                    {
                        temp.StartPlay();
                    }
                }

            }
            for (int i = 0; i < orthogons.Count; i++)
            {
                try
                {
                    ((LightItem)orthogons[i]).mydata = Clone(pauseTime[Current].pauseData[i].data);
                    LightItem light = ((LightItem)orthogons[i]);
                    if (pauseTime[Current].pauseData.Count > 0)
                    {
                        light.lightDegree = Clone(pauseTime[Current].pauseData[i].currentLight);
                    }
                }
                catch (Exception)
                {

                    ((AudioItem)orthogons[i]).mydata = Clone(pauseTime[Current].pauseData[i].data);
                    AudioItem audio = ((AudioItem)orthogons[i]);
                    if (pauseTime[Current].pauseData.Count > 0)
                    { float time = pauseTime[Current].pauseData[i].currentLight;
                        audio.nowPlayTime =Clone (time );
                    }
                }



 
            }

            //}
            //else
            //{
            //    TimerManager.Manager.ResultPause();
            //    for (int i = Current; i < pauseTime.Count; i++)
            //    {
            //        pauseTime[i].StartPlay();
            //    }

            //    Globaldata.global.second = pauseTime[Current].time * 100;
            //    Current -= 1;
            //}


        }
    }


    public void StorePause()
    {
        for (int i = 0; i < pauseTime.Count; i++)
        {
            PauseLine t = pauseTime[i];

            int j = i;
            while ((j > 0) && (pauseTime[j - 1].time > t.time))
            {

                pauseTime[j] = pauseTime[j - 1];
                --j;
            }

            pauseTime[j] = t;
        }
    }


    public void PauseData()
    {
        //for (int i = 0; i < pauseTime.Count; i++)
        //{
        //    for (int j = 0; j < orthogons.Count; j++)
        //    {
        //        LightItem temp = ((LightItem)orthogons[j]);
        //        int index = -1;
        //        for (int k = 0; k < temp.points.Count; k++)
        //        {
        //            if (pauseTime[i].time*100 < temp.points[k].data.currentTime)
        //            {
        //                index = k;
        //                break;
        //            }
        //        }
        //        if (index != -1)
        //        {
        //            //PauseData tempdata=new PauseData ()
        //            DataBase templightdata = new DataBase();
        //            float light=0;
        //            templightdata.start = temp.points[index - 1].data.currentTime;
        //            templightdata.end = temp.points[index].data.currentTime;
        //            templightdata.lightDegreemax = temp.points[index].data.num;
        //            templightdata.lightDegreemin = temp.points[index - 1].data.num;
        //                float time = temp.points[index].data.currentTime - pauseTime[i].time*100;
        //            light = templightdata.lightDegreemin+(((templightdata.lightDegreemax - templightdata.lightDegreemin) / ((templightdata.end - templightdata.start))) * Time.deltaTime*100 ) * (time/100 / Time.deltaTime);
        //            if (light < 0)
        //            {
        //                light = 0;
        //            }
        //            pauseTime[i].pauseData.Add(new global::PauseData(templightdata, light));
        //        }
        //    }
        //}


    }
}