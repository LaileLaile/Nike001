
﻿using ETC.Platforms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmxmanager
{

    public string port;
    private DMX dmx;

    private static Dmxmanager dmxmanange;

    public static Dmxmanager GetDmx()
    {
        if (dmxmanange == null)
        {
            dmxmanange = new Dmxmanager();
        }
        return dmxmanange;
    }
    public Dmxmanager()
    {
        port = Globaldata.Global.dmxPort;

        dmx = new DMX(port);
    }


    public void ChangePort(string port)
    {
        dmx.ChangeDmx(port);
    }

    public void ChoseContralLight(int index, float lightDegree)
    {

        dmx.Channels[index] = (byte)lightDegree;
        


        dmx.Send();
    }

}