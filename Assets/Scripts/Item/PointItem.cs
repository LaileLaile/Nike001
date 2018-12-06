using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PointItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler,IEndDragHandler
{


    CursorChange change;
    bool iskedow;
    bool isup;
    private RectTransform parent;
    private LightItem parentLight;

   // public float time;
   // public float num;

    public bool isStart;
    public bool isEnd;

    public PointItemData data = new PointItemData();


    Button button;
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("dkjfklsdjfkj");



    //}


    public void StartPlay()
    {

    }
    public void Play()
    {
        parentLight.mydata.start = data.currentTime;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun && parentLight.isCreatePoint)
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
        change = Camera.main.GetComponent<CursorChange>();
        parent = transform.parent.GetComponent<RectTransform>();
        parentLight = parent.GetComponent<LightItem>();
        parentLight.points.Add(GetComponent<PointItem>());
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowConfig);
        ChangeIndex();
        
        if (isStart)
        {
            data.currentTime = parentLight.mydata.start;
        }
        else
        {
            data.currentTime = parentLight.mydata.start + transform.position.x - (parent.position.x - parent.sizeDelta.x / 2);
        }



        if (isStart || isEnd)
        {
            if (!parentLight.iscopy )
            {
                transform.position = new Vector3(GetComponent<PointItem>().transform.position.x, GetComponent<PointItem>().transform.position.y - 50);
            }
            else
            {
                transform.position = new Vector3(GetComponent<PointItem>().transform.position.x, GetComponent<PointItem>().transform.position.y);
            }
           
        }
     
        data.num = (transform.localPosition.y + 50) / 100 * Globaldata.LIGHT;
        if (data.num <= 0)
        {
            data.num = 0;
        }
        Globaldata.Global.DrawLine(parentLight.posTran);
        Globaldata.Global.SortePoint(parentLight.points);

        if (!isStart &&!isEnd )
        {
            ShowConfig();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            DeleteMyGame();
        }
     
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        iskedow = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (parentLight.isCreatePoint)
        {
            SetTimeNum();
            Globaldata.Global.DrawLine(parentLight.posTran);
            Globaldata.Global.SortePoint(parentLight.points);
        }

    }

    public void SetTimeNum()
    {
        data.currentTime = parentLight.mydata.start + transform.position.x - (parent.position.x - parent.sizeDelta.x / 2);
        data.num = (transform.localPosition.y + 50) / 100 * Globaldata.LIGHT;
        if (data.num <= 0)
        {
            data.num = 0;
        }
        //ShowConfig();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Globaldata.Global.Isrun && parentLight.isCreatePoint)
        {
            if (isStart || isEnd)
            {
                transform.position = new Vector3(transform.position.x, Input.mousePosition.y);
            }
            else
            {
                transform.position = Input.mousePosition;
                if (transform.position.x >= parent.position.x + parent.sizeDelta.x / 2)
                {
                    transform.position = new Vector3(parent.position.x + parent.sizeDelta.x / 2, transform.position.y);
                }
                if (transform.position.x <= parent.position.x - parent.sizeDelta.x / 2)
                {
                    transform.position = new Vector3(parent.position.x - parent.sizeDelta.x / 2, transform.position.y);
                }






            }
            if (transform.position.y >= parent.position.y + parent.sizeDelta.y / 2)
            {
                transform.position = new Vector3(transform.position.x, parent.position.y + parent.sizeDelta.y / 2);
            }
            if (transform.position.y <= parent.position.y - parent.sizeDelta.y / 2)
            {
                transform.position = new Vector3(transform.position.x, parent.position.y - parent.sizeDelta.y / 2);
            }
        }


    }


    public void DeleteMyGame()
    {
        if (!Globaldata.Global.Isrun)
        {
            if (isup)
            {
                if (!isStart && !isEnd)
                {
                    parentLight.points.Remove(GetComponent<PointItem>());
                    parentLight.posTran.Remove(transform);
                    Destroy(gameObject);
                    Globaldata.Global.DrawLine(parentLight.posTran);
                    Globaldata.Global.SortePoint(parentLight.points);
                }

            }
        }


    }


    public void SetParentLight()
    {
        //parentLight.mydata.lightDegreemin = num;

        if (MyIndex() < parentLight.points.Count - 1)
        {
            parentLight.lightDegree = data.num;
            parentLight.mydata.start = data.currentTime;

            parentLight.mydata.lightDegreemin = data.num;
            parentLight.mydata.lightDegreemax = parentLight.points[MyIndex() + 1].data.num;
            parentLight.mydata.end = parentLight.points[MyIndex() + 1].data.currentTime;
        }





    }

    public int MyIndex()
    {
        return parentLight.points.IndexOf(this);
        //for (int i = 0; i < parentLight.points .Count ; i++)
        //{
        //    if (parentLight.points[i])
        //    {

        //    }
        //}
    }


    public void ShowConfig()
    {
        Globaldata.Global.showItemManger.pointItem = this;
        Globaldata.Global.showItemManger.pointItemShow.pointData = data;
        Globaldata.Global.showItemManger.showPoint();
        Globaldata.Global.showItemManger.ShowPointConfig(data);
    }

    public void ChangeMyself(PointItemData changedata )
    {
        if (isStart)
        {
            transform.position = new Vector3(changedata.currentTime - parentLight.mydata.start + (parent.position.x - parent.sizeDelta.x / 2), 0);
            transform.localPosition = new Vector3(transform.localPosition.x, (changedata.num / Globaldata.LIGHT) * 100 - 50);
            data.currentTime = parentLight.mydata.start;
        }
        else
        {

            transform.position = new Vector3(changedata.currentTime - parentLight.mydata.start + (parent.position.x - parent.sizeDelta.x / 2),0);
            transform.localPosition = new Vector3(transform.localPosition.x, (changedata.num / Globaldata.LIGHT) * 100-50);


            //(transform.localPosition.y + 50) / 100 * 30;

        }

    }

    public void ChangeIndex()
    {
        data.index = parentLight.mydata.index;
    }

    private void OnDestroy()
    {
       parentLight. showMyselfLightConfig();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentLight.isCreatePoint)
        {
            SetTimeNum();
            ShowConfig();
            Globaldata.Global.DrawLine(parentLight.posTran);
            Globaldata.Global.SortePoint(parentLight.points);
            
        }
    }
}
