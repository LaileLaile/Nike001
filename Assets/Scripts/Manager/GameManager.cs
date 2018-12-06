
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Threading;


public class GameManager : MonoBehaviour
{

    protected float s1 = 0;
    bool s;
    public Text showtime;


    public RectTransform rect;
    public float x;
    public float contentx;
    public Transform pionter;

    DateTime last = DateTime.Now;


    public Transform top;
    public Transform bottom;


    public Transform contont;
    public float contontmovtime;
    public ScrollRect scroll;

    public Transform pointer;
    bool isloop = false;
    public InputField[] inputs;

    public InputField inputShow;
    public Text showText;


    public Create create;
    public string saveUrl;

    public InputField dmxInput;
    public Button[] buttons;
    public Toggle loop;
    Thread threadSecond;
    public bool isTime;
    private bool exitApp;
    private List<Usually> lights=new List<Usually> ();

    public List<Usually> usuallies = new List<Usually>();
    // Use this for initialization
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1920, 1080, true);
        usuallies.Add(new Usually(4,5));
        usuallies.Add(new Usually(5, 5));
        usuallies.Add(new Usually(6, 5));
        usuallies.Add(new Usually(9, 5));
        //usuallies.Add(new Usually(64, 5));
        // Application.targetFrameRate = 1000;
        //Time.maximumDeltaTime = 0.01f;
        saveUrl = Application.dataPath + "/saveData/";
        if (!Directory.Exists(saveUrl))
        {
            Directory.CreateDirectory(saveUrl);
           
        }
         Usually a = new Usually();
            lights.Add(a);

            File.WriteAllText(saveUrl + "saveusually.xml", XmlUtil.Serializer(lights));
        Globaldata.Global.videomanager = FindObjectOfType<VideoManager>();
        Globaldata.Global.eventmanger = FindObjectOfType<ActionEvent>();
        Globaldata.Global.lineManger = FindObjectOfType<DrawLine>();
        StartCoroutine(Runaction());
        Globaldata.Global.pointer = pointer;
        Globaldata.Global.showItemManger = FindObjectOfType<ShowItemManger>();
        Globaldata.Global.top = top.position;
        Globaldata.Global.bottom = bottom.position;

        x = rect.localPosition.x - rect.sizeDelta.x / 2;
        ChangeContont();
        StartInit();
        dmxInput.text = Globaldata.Global.dmxPort;
        InvokeRepeating("SaveSet", 0, 60);

        //Invoke("InitTime", 0);
        InitTime();
        threadSecond = new Thread(new ThreadStart(UPdateS));
        threadSecond.Start();
        dmxInput.enabled = true;

        for (int i = 0; i < usuallies.Count ; i++)
        {
            Dmxmanager.GetDmx().ChoseContralLight(usuallies[i].index, usuallies[i].lightdree );
        }




       
    }


    public void InitTime()
    {
        System.Timers.Timer t = new System.Timers.Timer(10);   //实例化Timer类，设置间隔时间为10000毫秒；   
        t.Elapsed += new System.Timers.ElapsedEventHandler(Updatesecond); //到达时间的时候执行事件；   
        t.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
        t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件； 

    }

    public void Updatesecond(object source, System.Timers.ElapsedEventArgs e)
    {
        isTime = true;


    }


    public void UPdateS()
    {
        while (!exitApp)
        {
            if (!Globaldata.Global.isPause)
            {
                while (isTime)
                {

                    Globaldata.Global.second += (1);
                    //Debug.Log(Time.deltaTime);
                    isTime = false;
                }
            }
        }

    }

    public void StartInit()
    {
        SavaData save = LoadSave();
        if (save != null)
        {
            for (int i = 0; i < save.lights.Count; i++)
            {
                create.Init(save.lights[i].data, save.lights[i].pointsConfig);
            }
            for (int i = 0; i < save.pauseTime.Count; i++)
            {
                CreatePause(save.pauseTime[i]);
            }
            Globaldata.Global.dmxPort = save.DmxCOM;
            inputShow.text = save.showTitle;
            showText.text = save.showTitle;
            isloop = save.isLoop;
            loop.isOn = save.isLoop;
        }


    }
    public IEnumerator Runaction()
    {
        while (true)
        {
            try
            {
                if (Globaldata.Global.Isrun)
                {

                    showtime.text = String.Format("{0:N1}", Globaldata.Global.second / 100);




                    if (Globaldata.Global.maxEndTime <= Globaldata.Global.second)
                    {
                        Stop();
                    }
                }

                if (Globaldata.Global.Isrun)
                {
                    if (Globaldata.Global.second >= 1600)
                    {
                        contont.position = new Vector3(contentx - (Globaldata.Global.second - contontmovtime), contont.position.y);
                    }
                    else
                    {
                        pionter.localPosition = new Vector3(x + Globaldata.Global.second, pionter.localPosition.y);
                        contontmovtime = Globaldata.Global.second;
                    }

                }
                else
                {
                    pionter.localPosition = new Vector3(x, pionter.localPosition.y);
                }
            }
            catch (Exception e)
            {

                Debug.Log(e.Message);
            }

            yield return new WaitForSeconds(0.000001f);
        }

    }
    private void FixedUpdate()
    {
        //if (!Globaldata.Global.isPause)
        //{
        //    Globaldata.Global.second += (Time.deltaTime * 100);
        if (Globaldata.Global.tempSaveData.Count > 0)
        {
            Debug.Log(Globaldata.Global.tempSaveData[0].start);
        }

        //}
    }
    // Update is called once per frame
    void Update()
    {
        if (!Globaldata.Global.isPause)
        {
            TimerManager.Manager.Update(Time.deltaTime * 100);
            TimerManager.Manager.VideoUpdate(Time.deltaTime * 100);

        }
        // Debug.LogError(Globaldata.Global.pauseTime.Count);

        if (Input.GetKeyDown(KeyCode.S))
        {
            Run();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            Stop();
            Globaldata.Global.isPause = false;
        }


        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    isloop = true;
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Globaldata.Global.LastPause();
        //}

        //if (Input.GetKeyUp(KeyCode.T))
        //{
        //    // Globaldata.Global.choseDis(0);
        //}


        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Globaldata.Global.Current != Globaldata.Global.pauseTime.Count - 1)
            {
                Globaldata.Global.isPause = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            for (int i = 1; i < 500; i++)
            {
                Dmxmanager.GetDmx().ChoseContralLight(i, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            for (int i = 0; i < usuallies.Count; i++)
            {
                Dmxmanager.GetDmx().ChoseContralLight(usuallies[i].index, usuallies[i].lightdree);
            }
        }
    }
    //public void changLight()
    //{
    //    while (!isQuiteApp)
    //    {
    //        if (Globaldata.Global.Isrun)
    //        {
    //            if (Globaldata.Global.maxEndTime < Globaldata.Global.second)
    //            {
    //                Stop();
    //            }
    //            Globaldata.Global.second += 10f;

    //            Thread.Sleep(100);

    //        }

    //    }

    //}


    public void Stop()
    {

        Debug.LogError("END" + (DateTime.Now - last).TotalSeconds);
        Globaldata.Global.Stop();
        contont.position = new Vector3(contentx, contont.position.y);

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].interactable = true;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }
        if (isloop)
        {
            pionter.localPosition = new Vector3(x, pionter.localPosition.y);
            Run();
        }
        else
        {
            scroll.horizontal = true;
            scroll.vertical = true;
        }


    }

    public void Run()
    {
        //try
        //{
        //    Stop();
        //}
        //catch (Exception)
        //{


        //}
        //Debug.LogError("Start"+DateTime.Now.ToLongDateString());
        last = DateTime.Now;
        if (!Globaldata.Global.Isrun)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].interactable = false;
            }
            Globaldata.Global.MaxTime();
            PauseLine[] tempArray = FindObjectsOfType<PauseLine>();
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i].StartPlay();
            }

            Globaldata.Global.Start();
            contont.position = new Vector3(contentx, contont.position.y);
            s = true;
            s1 = 0;
            scroll.horizontal = false;
            scroll.vertical = false;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = false;
            }

            for (int i = 0; i < Globaldata.Global.orthogons.Count ; i++)
            {
                try
                {
                    ((AudioItem)Globaldata.Global.orthogons[i]).nowPlayTime = 0;
                }
                catch (Exception)
                {

                  
                }
            }
        }




    }

    private void OnApplicationQuit()
    {

        Stop();
        exitApp = true;
    }


    public void ChangeContont()
    {
        contentx = contont.position.x;
    }


    public void ChangeTextShow()
    {
        showText.text = inputShow.text;
    }
    public void SaveSet()
    {
        if (!Globaldata.Global.Isrun)
        {
            SavaData savaData = new SavaData
            {
                DmxCOM = Globaldata.Global.dmxPort,
                isLoop = isloop
            };

            List<SelfConfig> selfConfigs = new List<SelfConfig>();
            for (int i = 0; i < Globaldata.Global.orthogons.Count; i++)
            {
                SelfConfig temp = Globaldata.Global.orthogons[i].ReturnSelfConfig();
                selfConfigs.Add(temp);
            }

            savaData.lights = selfConfigs;

            List<float> pauseTimes = new List<float>();
            for (int i = 0; i < Globaldata.Global.pauseTime.Count; i++)
            {
                pauseTimes.Add(Globaldata.Global.pauseTime[i].ReturnMyTime());
                Globaldata.Global.StorePause();
            }
            savaData.pauseTime = pauseTimes;
            savaData.showTitle = showText.text;
            //SaveXml(savaData);

            string s = XmlUtil.Serializer(savaData);

            File.WriteAllText(saveUrl + "save.xml", s);


            //Workbook workbook1 = new Workbook();
            //workbook1.LoadFromXml(saveUrl + "save.xml");
            //workbook1.SaveToFile(saveUrl+ "test.xlsx", ExcelVersion.Version2013);

        }




    }


    public SavaData LoadSave()
    {
        string url;

        url = saveUrl + "save.xml";
        if (File.Exists(url))
        {
            XmlDocument xmldoc;


            xmldoc = new XmlDocument();
            xmldoc.Load(url);
            return XmlUtil.Deserialize<SavaData>(xmldoc.InnerXml);
        }
        else
        {
            return null;
        }




    }

    public void SaveXml(SavaData save)
    {
        XmlDocument xmlDoc = new XmlDocument();
        //创建类型声明节点    
        XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
        xmlDoc.AppendChild(node);


        List<SelfConfig> lights = save.lights;

        //创建根节点    
        XmlNode root = xmlDoc.CreateElement("SaveData");
        xmlDoc.AppendChild(root);
        CreateNode(xmlDoc, root, "dmxPort", save.DmxCOM);
        CreateNode(xmlDoc, root, "IsLoop", save.isLoop.ToString());
        CreateNode(xmlDoc, root, "ShowFont", save.showTitle);
        CreateNode(xmlDoc, root, "Second", save.second.ToString());

        for (int i = 0; i < save.pauseTime.Count; i++)
        {
            CreateNode(xmlDoc, root, "pauseTime", save.pauseTime[i].ToString());
        }

        for (int i = 0; i < lights.Count; i++)
        {
            XmlNode light = xmlDoc.CreateElement("LightTimeLine");
            root.AppendChild(light);
            //CreateNode(xmlDoc, light, "sizeX", lights[i].sizeX.ToString());
            //CreateNode(xmlDoc, light, "sizeY", lights[i].sizeY.ToString());
            //CreateNode(xmlDoc, light, "localpostionX", lights[i].localpostionX.ToString());
            //CreateNode(xmlDoc, light, "localpostionY", lights[i].localpostionY.ToString());

            for (int j = 0; j < lights[i].pointsConfig.Count; j++)
            {
                XmlNode point = xmlDoc.CreateElement("point");
                light.AppendChild(point);
                CreateNode(xmlDoc, point, "isStart", lights[i].pointsConfig[j].isStart.ToString());
                CreateNode(xmlDoc, point, "isEnd", lights[i].pointsConfig[j].isEnd.ToString());
                CreateNode(xmlDoc, point, "loacalPosX", lights[i].pointsConfig[j].localpostionX.ToString());
                CreateNode(xmlDoc, point, "loacalPosY", lights[i].pointsConfig[j].localpostionY.ToString());
            }

        }

        try
        {
            xmlDoc.Save((saveUrl + "save.xml"));
        }
        catch (Exception e)
        {
            //显示错误信息    
            Console.WriteLine(e.Message);
        }




        // XmlUtil.Deserialize<SavaData>( (Application.streamingAssetsPath + "/save.xml"));
        //Console.ReadLine();    
    }


    public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
    {
        XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
        node.InnerText = value;
        parentNode.AppendChild(node);
    }



    public void CreatePause(float times)
    {
        GameObject temp = Globaldata.Global.LoadItemPoint(transform, "Item/pause", Input.mousePosition);
        temp.GetComponent<PauseLine>().time = times;
        temp.GetComponent<PauseLine>().isInit = true;
        Globaldata.Global.pauseTime.Add(temp.GetComponent<PauseLine>());
        Globaldata.Global.StorePause();
    }


    public void ChangeDmxCOM(InputField input)
    {


        Globaldata.Global.dmxPort = input.text;
        SaveSet();
        Globaldata.Global.SetDmxPort(Globaldata.Global.dmxPort);


    }


    public void setLoop(Toggle toggle)
    {
        isloop = toggle.isOn;
    }
}