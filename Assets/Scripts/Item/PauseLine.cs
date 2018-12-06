
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseLine : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CursorChange change;
    bool isup;


    public float time;
    private Button _button;
    private SingleScaleplate parent;
    public bool isInit;
    public List<PauseData> pauseData = new List<PauseData>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun)
        {
            change.PointImage();
            isup = true;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun)
        {
            change.normalImage();
            isup = false;
        }

    }



    // Use this for initialization
    void Start()
    {
        parent = transform.parent.GetComponent<SingleScaleplate>();
        if (isInit)
        {
            ChangeMyself(time);
            isInit = false;
        }
        else
        {
            time = parent.index * 10 + ((transform.localPosition.x + 500) / 100);
        }

        change = Camera.main.GetComponent<CursorChange>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowMyconfing);


        ShowMyconfing();

        // Globaldata.Global .showItemManger.
    }

    // Update is called once per frame
    void Update()
    {
        if (isup && Input.GetMouseButtonDown(1))
        {
            Globaldata.Global.pauseTime.Remove(this);
            Globaldata.Global.StorePause();
            change.normalImage();
            Destroy(gameObject);

        }
    }

    public void ShowMyconfing()
    {
        Globaldata.Global.showItemManger.pouseItem = this;
        Globaldata.Global.showItemManger.pouseItemShow.pouseTime = time;
        Globaldata.Global.showItemManger.ShowPouse();
        Globaldata.Global.showItemManger.ShowPouseConfig(time);
    }

    public void ChangeMyself(float sendtime)
    {
        time = sendtime;
        int s = (int)time / 10;

        transform.SetParent(Globaldata.Global.scaliplate[s].transform);
        transform.localPosition = new Vector3((time - s * 10) * 100 - 500, transform.localPosition.y);
        Globaldata.Global.StorePause();
        //if (time )
        //{

        //}
        //transform.position = new Vector3(Globaldata.Global.pointer.position.x + time, transform.position.y);
    }


    public void StartPlay()
    {
        TimerManager.Manager.PauseInvoke(time * 100, Pause);
    }

    public void Pause()
    {
        Globaldata.Global.isPause = true;
        if (pauseData.Count == 0)
        {
            for (int i = 0; i < Globaldata.Global.orthogons.Count; i++)
            {
                try
                {
                    DataBase data = Globaldata.Global.Clone(((LightItem)Globaldata.Global.orthogons[i]).mydata);
                    float light = Globaldata.Global.Clone(((LightItem)Globaldata.Global.orthogons[i]).lightDegree);
                    pauseData.Add(new PauseData(data, light));
                }
                catch (Exception)
                {

                    DataBase data = Globaldata.Global.Clone(((AudioItem )Globaldata.Global.orthogons[i]).mydata);
                    float temptime = time - Globaldata.Global.orthogons[i].mydata.start / 100;
                    float light = Globaldata.Global.Clone(temptime);
                    pauseData.Add(new PauseData(data, light));
                }

            }
        }

        //Globaldata.Global.Current = Globaldata.Global.pauseTime.IndexOf(this);
    }

    public float ReturnMyTime()
    {
        return time;
    }
}