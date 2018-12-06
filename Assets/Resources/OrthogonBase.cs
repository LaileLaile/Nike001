
﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrthogonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{

    public bool isup;//在本体上面
    private RectTransform rect;
    public bool isstretch;
    private CursorChange change;
    private Vector3 vec;
    private Vector3 myvec;
    public bool isPlay;
    protected bool isLoop;
    public DataBase mydata = new DataBase();
    private BoxCollider2D collider;

    public virtual void Stop()
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun)
        {
            vec = Input.mousePosition;
        }


        // throw new System.NotImplementedException();
    }
    public virtual void ChangeData(DataBase data)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun)
        {
            isup = true;
            if (isup)
            {
                if (/*(Input.mousePosition.x < rect.transform.position.x - rect.sizeDelta.x / 2 + 10 && Input.mousePosition.x > rect.transform.position.x - rect.sizeDelta.x / 2) || */(Input.mousePosition.x < rect.transform.position.x + rect.sizeDelta.x / 2 && Input.mousePosition.x > rect.transform.position.x + rect.sizeDelta.x / 2 - 10))
                {

                    isstretch = true;
                    change.stretchImage();
                }
                else
                {
                    isstretch = false;
                    change.normalImage();
                }


            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //isstretch = false;
        if (!Globaldata.Global.Isrun)
        {
            isup = false;
            change.normalImage();
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun)
        {
            isstretch = false;
            change.normalImage();
        }

    }


    void Start()
    {

    }
    public void Initbase()
    {


        rect = GetComponent<RectTransform>();

        change = Camera.main.GetComponent<CursorChange>();
        myvec = rect.localPosition;


    }

    // Update is called once per frame
    void Update()
    {






    }

    public virtual void Play()
    {

    }


    public void updateevent()
    {







    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun)
        {
            if (isstretch)
            {
                Vector2 temp = new Vector2(rect.sizeDelta.x + (Input.mousePosition.x - vec.x), rect.sizeDelta.y);
                if (temp.x > 10)
                {
                    rect.sizeDelta = temp;
                    if (temp.x > 10)
                    {
                        rect.localPosition += new Vector3((Input.mousePosition.x - vec.x) / 2, 0);

                    }


                }
                else
                {
                    rect.sizeDelta = new Vector2(10, rect.sizeDelta.y);
                    rect.localPosition = myvec;
                }
                vec = Input.mousePosition;

                mydata.start = (rect.localPosition.x - rect.sizeDelta.x / 2);
                mydata.end = (rect.localPosition.x + rect.sizeDelta.x / 2);
                mydata.extent = (mydata.end - mydata.start);

                ShowMyconfig(mydata);
                LightChange();

            }
        }




    }

    public virtual void LightChange()
    {

    }

    public virtual void ShowMyconfig(DataBase data)
    {


    }


    public virtual void ChangeMySlef(DataBase data)
    {
        if (data.end - data.start < 10)
        {
            rect.sizeDelta = new Vector2(10, rect.sizeDelta.y);
        }
        else
        {
            rect.sizeDelta = new Vector2((data.end - data.start), rect.sizeDelta.y);
        }

        rect.localPosition = new Vector2(data.start + rect.sizeDelta.x / 2, rect.localPosition.y);
        //collider.size = rect.sizeDelta;

    }



    public void SetPort(string dmxport) { mydata.dmxPort = dmxport; }
    public float returnEndtime()
    {
        return mydata.end;
    }

    public virtual void ChangeLight()
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StopLightChange();
    }



    public virtual void StopLightChange()
    {

    }

    public virtual void UpdateLine()
    {

    }

    public void Delete()
    {
        Destroy(gameObject);
    }


    public virtual SelfConfig ReturnSelfConfig()
    {
        return null;
    }
}