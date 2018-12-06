
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ActionEvent : MonoBehaviour
{
    public List<Action> actions = new List<Action>();
    bool quite;
    DateTime last = DateTime.Now;

    Thread thread1;
    public float[] senddata;
    // Use this for initialization
    void Start()
    {
        // StartCoroutine(Runaction());
        thred = new Thread(new ThreadStart(Runactions));
        thred.Start();
        thread1 = new Thread(new ThreadStart(RunactionLight));
        thread1.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    Thread thred;
    public IEnumerator Runaction()
    {
        while (!quite)
        {
            try
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i]();
                    actions.Remove(actions[i]);
                }
            }
            catch (Exception e)
            {

                Debug.Log(e.Message);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    public void Runactions()
    {
        while (!quite)
        {
            try
            {



                TimerManager.Manager.LightUpdate(Globaldata.Global.second);

                TimerManager.Manager.PauseUpdate(Globaldata.Global.second);



                Thread.Sleep(10);
            }
            catch (Exception e)
            {

                Debug.Log(e.Message);
            }


        }

    }



    public void RunactionLight()
    {
        while (!quite)
        {
            try
            {
                if (senddata.Length < Globaldata.Global.orthogons.Count)
                {
                    senddata = new float[Globaldata.Global.orthogons.Count];
                }
                for (int i = 0; i < Globaldata.Global.orthogons.Count; i++)
                {
                    try
                    {
                        LightItem temp = (LightItem)Globaldata.Global.orthogons[i];
                        if (temp != null && temp.isPlay)
                        {
                            if (senddata[i] != temp.getLightnum())
                            {

                                //Debug.Log(temp.mydata.index + "index" + temp.getLightnum());
                                senddata[i] = temp.getLightnum();
                                Dmxmanager.GetDmx().ChoseContralLight(temp.mydata.index, temp.getLightnum());



                            }



                        }


                    }
                    catch (Exception)
                    {


                    }

                }
                if (!Globaldata.Global.Isrun)
                {
                    for (int i = 0; i < senddata.Length; i++)
                    {
                        senddata[i] = 0;
                    }
                }

                Thread.Sleep(200);
            }
            catch (Exception e)
            {

                Debug.Log(e.Message);
            }


        }

    }
    public int AddAction(Action action)
    {
        actions.Add(action);
        return actions.Count - 1;
    }

    public void remove(int index)
    {
        actions.RemoveAt(index);
    }
    private void OnApplicationQuit()
    {
        quite = true;
    }
}