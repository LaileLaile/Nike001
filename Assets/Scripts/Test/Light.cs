using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using DG.Tweening;
using System;

public class Light :MonoBehaviour {

    public bool isOpen;

    public double start;
    public double end;
    private float lightDegree;
    private int index;
    //  public TestLight testLight;

    private void Awake()
    {
        index = int.Parse(gameObject.name);
    }
    private void Update()
    {
      //  Debug.LogError(lightDegree);
    }
    public void changLight()
    {
        while (isOpen)
        {
            try
            {
 Dmxmanager.GetDmx(). ChoseContralLight(index , lightDegree);
          
            }
            catch (System.Exception e)
            {

                throw;
            }
         

            Thread.Sleep(100);
        }
        if (!isOpen )
        {
            lightDegree = 0;
            Dmxmanager.GetDmx().ChoseContralLight(index, lightDegree);
        }
    }

    private void OnEnable()
    {
        isOpen = true;
        lightDegree = 0;
        DOTween.To(() => lightDegree, x => lightDegree = x, 10, (float )(end - start));
        Thread thread = new Thread(new ThreadStart(changLight));
        thread.Start();

        Debug.LogError("激活"+gameObject.name);
    }

    public  void Close()
    {

        isOpen = false;
        lightDegree = 0;
        Dmxmanager.GetDmx().ChoseContralLight(int.Parse(gameObject.name), lightDegree);
        Debug.LogError(gameObject.name);
    }

    private void OnDisable()
    {
        isOpen = false;
        lightDegree = 0;
        Dmxmanager.GetDmx().ChoseContralLight(int.Parse(gameObject.name), lightDegree);
        Debug.LogError(gameObject.name);
    }


    private void OnApplicationQuit()
    {
        isOpen = false;
    }

   
}
