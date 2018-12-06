using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System;
//using UnityStandardAssets.Utility;
using UnityEngine.UI;

public class Udp_Any : MonoBehaviour
{

    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipend;
    Thread connectThread1;
    byte[] sendData;
    public byte[] sendByte = { 125, 255, 255, 50, 255, 255 };
    private int recvLen;
    byte[] recvData;
    string recvstr;
    //private string lastResult = "";//zhu
    public string currentResult = "";
    //string test = "1234 from unity"; //zhu
    //[HideInInspector]
    public int port = 5678;
    public string localip = "127.0.0.1";

    //public int data_count;//zhu
    private float call_time;

    private float calm_time;
    private bool socketready;
    //private bool levelreload;//zhu
    public string sendmessage;
    public bool send;
    public float machine_num;
    private float f_divrsor = 1300;


    string receiveStr = "";

    private float f_tempSpeed;
    public GameManager manager;
    //public InputField rule;
    //private float rules = 610;
    void Awake()
    {

        print(Network.player.ipAddress);
        localip = Network.player.ipAddress;
    }

    void Init()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipend = new IPEndPoint(IPAddress.Broadcast, port);                       //目标机地址
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        IPEndPoint sender = new IPEndPoint(IPAddress.Parse(localip), port);      //本机地址
        serverEnd = (EndPoint)sender;
        socket.Bind(sender);
        //socket.Bind(ipend);
#if UNITY_EDITOR
        Debug.Log("waiting for sending Udp dgram");
#endif
        //握手
        SocketSend();
        connectThread1 = new Thread(new ThreadStart(SocketReceive));
        connectThread1.Start();
    }
    // Use this for initialization
    void Start()
    {
        //ip = PlayerPrefs.GetString("targetIP");
        //localip = PlayerPrefs.GetString("localIP");
        Init();
        call_time = Time.time;
        socketready = true;
        //levelreload = true; //zhu

    }

    // Update is called once per frame
    float time = 0;
    void Update()
    {

        time += Time.deltaTime;

        if (socketready && Time.time - call_time > 10)
        {
            call_time = Time.time;
            SocketSend();
        }

        if (send)
        {
            SocketSend();
            send = false;
            //print(sendmessage);
            sendmessage = "";
        }


        if (currentResult != "")
        {
            if (currentResult.Contains("BACK"))
            {
                Globaldata.Global.LastPause();
            }
            else if (currentResult.Contains("NEXT"))
            {
                if (Globaldata.Global.Isrun)
                {
                    if (Globaldata.Global.Current != Globaldata.Global.pauseTime.Count - 1)
                    {
                        Globaldata.Global.isPause = false;
                    }

                }
                else
                {
                    manager.Run();
                }
            }
            else
            {
                manager.Stop();
                Globaldata.Global.isPause = false;
            }

            currentResult = "";
        }
    }

    void SocketSend()
    {
        sendData = new byte[1024];
        sendData = Encoding.ASCII.GetBytes(sendmessage);
        //sendData = System.Text.Encoding.Default.GetBytes(test);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipend);

        //socket.SendTo(sendByte, ipend);
        //print("send something");
    }

    void SocketReceive()
    {
        while (true)
        {
            try
            {
                recvData = new byte[1024];
                recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
                if (recvLen != 0)
                {
                    recvstr = Encoding.ASCII.GetString(recvData, 0, recvLen);
                    Debug.Log("UDP receive : " + recvstr);

                    receiveStr = "UDP receive : " + recvstr;
                    currentResult = recvstr.Split(':')[1];


                }

            }
            catch (Exception)
            {

                throw;
            }

            //print(currentResult);
        }



    }

    void OnApplicationQuit()
    {
        if (connectThread1 != null)
        {
            connectThread1.Interrupt();
            connectThread1.Abort();

        }
        if (socket != null)
        {
            socket.Close();
        }
    }

    public float GetVoule()
    {
        //Debug.LogError(machine_num);

        return machine_num;
    }
    public void SetVoule()
    {
        machine_num = 0;
    }


    public void Divrsor(InputField num)
    { }
    //public void setrule()
    //{
    //    rules =float .Parse ( rule.text);
    //}
}
