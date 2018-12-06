
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;


public class LightItem : OrthogonBase
{

    public string COMPort;

    public float lightDegree;


    public DateTime times;


    public float lightDegreeTarget;

    public bool isCreatePoint;

    public List<PointItem> points = new List<PointItem>();

    public List<Transform> posTran = new List<Transform>();

    public int LineIndex = -1;

    private Button _button;

    public bool iscopy;

    public DateTime lastClickTime;
    private List<float> count = new List<float>();
    // public new  DataBase mydata=new DataBase ();
    // private DataBase myselfdata;

    public List<pointItemConfig> pointsConfig;
    // Use this for initialization
    void Start()
    {
        COMPort = Globaldata.Global.dmxPort;
        Initbase();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowMyconfig);
        ShowMyconfig();
        GetComponent<LightItem>().ChangeMySlef(GetComponent<LightItem>().mydata);


        //mydata  = new DataBase(1);
        if (iscopy)
        {
            if (pointsConfig != null)
            {
                Create();
                pointsConfig = null;
            }
            else
            {
                CopyPoint();
            }
        }

        transform.parent.parent.parent.GetComponent<ScrollRect>().verticalScrollbar.value = 0;
    }
    public LightItem()
    {

    }

    void Update()
    {
        //updateevent();


        if (isPlay && !Globaldata.Global.isPause)
        {
            lightDegree += ((mydata.lightDegreemax - mydata.lightDegreemin) / ((mydata.end - mydata.start))) * Time.deltaTime * 100;

            if (lightDegree < 0)
            {
                lightDegree = 0;
            }
            //lightDegree = Mathf.Lerp(lightDegree, mydata.lightDegreemax,/*Time.deltaTime*/  ((1 / ((mydata.end - mydata.start))) * Time.deltaTime)*100 );
            //Debug.Log(lightDegree);
        }
        if (Input.GetKeyDown(KeyCode.Delete) && isCreatePoint)
        {
            Globaldata.Global.DeleteLight(this);

        }

        //if (isup && !Globaldata.Global.Isrun && Input.GetMouseButtonDown(1))
        //{
        //    Globaldata.Global.DeleteLight(this);
        //}
        // Debug.Log(transform.position);

        //if (50 > transform.localPosition .y && -700< transform.localPosition.y)
        //{
        //    Globaldata.Global.DrawLine(posTran);
        //}
        //else
        //{
        //    Globaldata.Global.choseDis(LineIndex);
        //}
    }

    private void SendMessage()
    {
        if (isPlay)
        { //float send = lightDegree * 10000;
            Dmxmanager.GetDmx().ChoseContralLight(mydata.index, lightDegree);
        }


    }

    public override void ShowMyconfig(DataBase data)
    {
        base.ShowMyconfig(data);


        Globaldata.Global.showItemManger.showLight(data);
    }
    private void FixedUpdate()
    {

        UpdateLine();
    }
    public override void Play()
    {
        base.Play();
        //  TimerManager.Manager.Invoke(Time.deltaTime, SendMessage, true);



        TimerManager.Manager.LightInvoke(points[0].data.currentTime, StartPlay);
        TimerManager.Manager.LightInvoke(mydata.end, Stop);
        //Debug.LogError(mydata.end);
    }
    public float getLightnum()
    {
        return lightDegree;
    }
    public void StartPlay()
    {
        //mydata.lightDegreemin = points[0].num;
        //lightDegree = mydata.lightDegreemin;
        //if (points .Count >=2)
        //{
        //    mydata.lightDegreemax = points[2].num;
        //}
        //else
        //{
        //    mydata.lightDegreemax = points[1].num;
        //}

        //tweener = DOTween.To(() => lightDegree, x => lightDegree = x, mydata.lightDegreemax, mydata.extent).SetEase(Ease.Linear);
        for (int i = 0; i < points.Count; i++)
        {
            TimerManager.Manager.LightInvoke(points[i].data.currentTime, points[i].SetParentLight);
        }
        times = DateTime.Now;

        isPlay = true;
    }

    public void StartPlay(int indexpoint)
    {
        //mydata.lightDegreemin = points[0].num;
        //lightDegree = mydata.lightDegreemin;
        //if (points .Count >=2)
        //{
        //    mydata.lightDegreemax = points[2].num;
        //}
        //else
        //{
        //    mydata.lightDegreemax = points[1].num;
        //}

        //tweener = DOTween.To(() => lightDegree, x => lightDegree = x, mydata.lightDegreemax, mydata.extent).SetEase(Ease.Linear);
        for (int i = indexpoint; i < points.Count; i++)
        {
            TimerManager.Manager.LightInvoke(points[i].data.currentTime, points[i].SetParentLight);
        }
        times = DateTime.Now;

        isPlay = true;
    }
    //public void GetTimeAndNum()
    //{

    //}








    public override void LightChange()

    {
        _button.enabled = false;
        for (int i = 0; i < points.Count; i++)
        {
            points[i].transform.SetParent(transform.parent);
        }

        for (int i = 0; i < points.Count; i++)
        {
            points[i].data.index = mydata.index;
            if (posTran[i].transform.position.x > transform.position.x + GetComponent<RectTransform>().sizeDelta.x / 2 || posTran[i].transform.position.x < transform.position.x - GetComponent<RectTransform>().sizeDelta.x / 2)
            {
                if (!posTran[i].GetComponent<PointItem>().isEnd && !posTran[i].GetComponent<PointItem>().isStart)
                {
                    points.Remove(posTran[i].GetComponent<PointItem>());
                    Transform temp = posTran[i];
                    posTran.Remove(posTran[i]);
                    Destroy(temp.gameObject);
                    i = 0;
                }

            }
        }
    }

    public override void Stop()
    {
        isPlay = false;


        TimeSpan ts = DateTime.Now - times;
        // Debug.Log(ts.TotalMilliseconds + "   " + mydata.index);
        Dmxmanager.GetDmx().ChoseContralLight(mydata.index, 0);
    }


    public void ShowMyconfig()
    {

        if (!Globaldata.Global.Isrun)
        {
            if (!isCreatePoint)
            {
                Globaldata.Global.showItemManger.showLight();
                if (Globaldata.Global.currentLight != null)
                {
                    Globaldata.Global.currentLight.isCreatePoint = false;
                    Globaldata.Global.currentLight.GetComponent<Image>().color = Color.yellow;
                }
                isCreatePoint = true;
                GetComponent<Image>().color = Color.blue;
                Globaldata.Global.currentLight = GetComponent<LightItem>();
                Globaldata.Global.showItemManger.lightitem = GetComponent<LightItem>();
                Globaldata.Global.showItemManger.showLight();
                Globaldata.Global.showItemManger.light.lightdata = mydata;

                Globaldata.Global.showItemManger.showLight(mydata);
                if (!iscopy)
                {
                    if (points.Count <= 0)
                    {


                        GameObject temp1 = Globaldata.Global.LoadItemPoint(transform, "Item/PointItem", new Vector3(transform.position.x - GetComponent<RectTransform>().sizeDelta.x / 2, transform.position.y));
                        temp1.GetComponent<PointItem>().isStart = true;
                        temp1.GetComponent<PointItem>().data.currentTime = mydata.start;
                        GameObject temp2 = Globaldata.Global.LoadItemPoint(transform, "Item/PointItem", new Vector3(transform.position.x + GetComponent<RectTransform>().sizeDelta.x / 2, transform.position.y));
                        temp2.GetComponent<PointItem>().isEnd = true;
                        temp2.GetComponent<PointItem>().data.currentTime = mydata.end;
                        posTran.Add(temp1.transform);
                        posTran.Add(temp2.transform);
                        Globaldata.Global.DrawLine(posTran);
                        Globaldata.Global.SortePoint(points);

                    }

                }





            }
            else
            {
                if ((DateTime.Now - lastClickTime).TotalSeconds < 0.4f)
                {
                    //  Debug.Log((DateTime.Now - lastClickTime).TotalSeconds);
                    GameObject temp = Globaldata.Global.LoadItemPoint(transform, "Item/PointItem", new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                    posTran.Add(temp.transform);
                    temp.GetComponent<PointItem>().ShowConfig();
                    Globaldata.Global.DrawLine(posTran);
                    Globaldata.Global.SortePoint(points);

                }

                else
                {
                    GetComponent<Image>().color = Color.blue;
                    Globaldata.Global.currentLight = GetComponent<LightItem>();
                    Globaldata.Global.showItemManger.lightitem = GetComponent<LightItem>();
                    Globaldata.Global.showItemManger.showLight();
                    Globaldata.Global.showItemManger.light.lightdata = mydata;

                    Globaldata.Global.showItemManger.showLight(mydata);
                }


                lastClickTime = DateTime.Now;

            }

        }


    }
    public override void ChangeMySlef(DataBase data)
    {
        base.ChangeMySlef(data);
        mydata.start = data.start;
        mydata.end = data.end;
        mydata.extent = data.extent;
        mydata.lightDegreemin = data.lightDegreemin;
        mydata.lightDegreemax = data.lightDegreemax;
        mydata.index = data.index;
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].isStart)
            {
                points[i].transform.position = new Vector3(transform.position.x - GetComponent<RectTransform>().sizeDelta.x / 2, points[i].transform.position.y);
            }
            if (points[i].isEnd)
            {
                points[i].transform.position = new Vector3(transform.position.x + GetComponent<RectTransform>().sizeDelta.x / 2, points[i].transform.position.y);

            }
        }



        Globaldata.Global.DrawLine(posTran);
        Globaldata.Global.SortePoint(points);

    }


    //private void OnWillRenderObject()
    //{

    //}

    //private void OnBecameInvisible()
    //{
    //    Globaldata.Global.choseDis(LineIndex);

    //}
    //private void OnBecameVisible()
    //{

    //}


    public override void StopLightChange()
    {
        base.StopLightChange();

        _button.enabled = true;
        for (int i = 0; i < points.Count; i++)
        {

            if (points[i].isStart)
            {
                points[i].transform.position = new Vector3(transform.position.x - GetComponent<RectTransform>().sizeDelta.x / 2, points[i].transform.position.y);
            }
            if (points[i].isEnd)
            {
                points[i].transform.position = new Vector3(transform.position.x + GetComponent<RectTransform>().sizeDelta.x / 2, points[i].transform.position.y);

            }

            points[i].transform.SetParent(transform);
            points[i].SetTimeNum();


        }

        Globaldata.Global.DrawLine(posTran, LineIndex);
        Globaldata.Global.SortePoint(points);

    }
    public override void UpdateLine()
    {
        base.UpdateLine();
        Globaldata.Global.DrawLine(posTran, LineIndex);
        Globaldata.Global.SortePoint(points);
        // PlayCount();
    }


    public void BreakParent()
    {
        for (int i = 0; i < points.Count; i++)
        {

            points[i].transform.SetParent(transform.parent);


        }
    }



    public void CopyPoint()
    {


        LightItem temp = (LightItem)Globaldata.Global.copyTemp;


        for (int i = 0; i < temp.points.Count; i++)
        {
            GameObject temp1 = Globaldata.Global.CopyLoadItemPoint(transform, "Item/PointItem", temp.points[i].transform.localPosition);
            temp1.GetComponent<PointItem>().isStart = temp.points[i].isStart;
            temp1.GetComponent<PointItem>().isEnd = temp.points[i].isEnd;
            temp1.GetComponent<PointItem>().data = temp.points[i].data;
            posTran.Add(temp1.transform);

        }

        Globaldata.Global.DrawLine(posTran);
        Globaldata.Global.SortePoint(points);

    }

    public void Create()
    {





        for (int i = 0; i < pointsConfig.Count; i++)
        {
            GameObject temp1 = Globaldata.Global.CopyLoadItemPoint(transform, "Item/PointItem", new Vector3(pointsConfig[i].localpostionX, pointsConfig[i].localpostionY));
            temp1.GetComponent<PointItem>().isStart = Globaldata.Global.Clone(pointsConfig[i].isStart);
            temp1.GetComponent<PointItem>().isEnd = Globaldata.Global.Clone(pointsConfig[i].isEnd);
            temp1.GetComponent<PointItem>().data = Globaldata.Global.Clone(pointsConfig[i].pointdata);
            posTran.Add(temp1.transform);

        }

        Globaldata.Global.DrawLine(posTran);
        Globaldata.Global.SortePoint(points);

    }

    public void showMyselfLightConfig()
    {
        Globaldata.Global.currentLight = GetComponent<LightItem>();
        Globaldata.Global.showItemManger.lightitem = GetComponent<LightItem>();
        Globaldata.Global.showItemManger.showLight();
        Globaldata.Global.showItemManger.light.lightdata = mydata;

        Globaldata.Global.showItemManger.showLight(mydata);
    }








    public override SelfConfig ReturnSelfConfig()
    {
        SelfConfig self = new SelfConfig();
        //self.sizeX = GetComponent<RectTransform>().sizeDelta.x ;
        //self.sizeY = GetComponent<RectTransform>().sizeDelta.y;
        //self.localpostionX= transform.localPosition.x ;
        //self.localpostionY= transform.localPosition.y;
        self.data = mydata;

        List<pointItemConfig> listconfig = new List<pointItemConfig>();

        for (int i = 0; i < points.Count; i++)
        {
            pointItemConfig temp = new pointItemConfig();
            temp.isStart = points[i].isStart;
            temp.isEnd = points[i].isEnd;
            temp.pointdata = points[i].data;

            temp.localpostionX = points[i].transform.localPosition.x;
            temp.localpostionY = points[i].transform.localPosition.y;
            listconfig.Add(temp);
        }

        self.pointsConfig = listconfig;

        return self;




    }

    //private List<float> temps = new List<float>();
    //public void PlayCount()
    //{
    //    float s = points[0].data.num;
    //    for (int i = 1; i < points.Count ; i++)
    //    {
    //        while (Mathf.Abs(s-points[i].data .num )>((points[i].data.num - s) / ((points[i].data.currentTime - points[i - 1].data.currentTime))) * Time.deltaTime * 100*2)
    //        {

    //            s += ((points[i].data.num - s) / ((points[i].data.currentTime - points[i-1].data.currentTime))) * Time.deltaTime * 100;
    //            temps.Add(s);
    //        }
    //        s = points[i].data.num ;
    //    }
    //}

}