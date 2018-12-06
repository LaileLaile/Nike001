using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurve : MonoBehaviour
{
    void Start()
    {
        var tempCurve = this.gameObject.AddComponent<UICurve>();
        UICurveData tempcd = new UICurveData();
        tempcd.Ccolor = Color.yellow;
        tempcd.Thickness = 2;
        for (int i = 0; i < 360; i++)
        {
            tempcd.Addpos(i * 2, (float)Mathf.Cos(i));
        }
        tempCurve.AddCurveData(1, tempcd);
    }
}