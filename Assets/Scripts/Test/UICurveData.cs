using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICurveData
{
    #region [Fields]
    public List<Vector2> Postion = new List<Vector2>();
    public Color Ccolor;
    public float Thickness = 1;
    #endregion

    #region [PublicTools]
    public void Addpos(float varX, float varY)
    {
        Addpos(new Vector2(varX, varY));
    }
    public void Addpos(Vector2 varV2)
    {
        Postion.Add(varV2);
    }
    #endregion

}