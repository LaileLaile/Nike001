using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurve : MaskableGraphic
{
    #region [Fields]
    private Dictionary<int, UICurveData> mCurveData = new Dictionary<int, UICurveData>();
    #endregion

    #region [Inherit]
    protected override void OnPopulateMesh(VertexHelper varVerHeler)
    {
        varVerHeler.Clear();

        foreach (var tempKvp in mCurveData)
        {
            var tempUICurveData = tempKvp.Value;
            if (tempUICurveData.Postion.Count < 2)
            {
                continue;
            }
            for (int i = 1; i < tempUICurveData.Postion.Count; i++)
            {
                UIVertex[] verts = new UIVertex[4];

                float x1 = tempUICurveData.Postion[i - 1].x;
                float y1 = tempUICurveData.Postion[i - 1].y;
                float x2 = tempUICurveData.Postion[i].x;
                float y2 = tempUICurveData.Postion[i].y;

                float xd = (y2 - y1) / Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) * Mathf.Pow(y2 - y1, 2)) * tempKvp.Value.Thickness / 2;
                float yd = (x2 - x1) / Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) * Mathf.Pow(y2 - y1, 2)) * tempKvp.Value.Thickness / 2;

                int idx = 0;
                verts[idx].position = new Vector3(tempUICurveData.Postion[i - 1].x - xd, tempUICurveData.Postion[i - 1].y + yd);
                verts[idx].color = tempUICurveData.Ccolor;
                verts[idx].uv0 = Vector2.zero;

                idx++;
                verts[idx].position = new Vector3(tempUICurveData.Postion[i].x - xd, tempUICurveData.Postion[i].y + yd);
                verts[idx].color = tempUICurveData.Ccolor;
                verts[idx].uv0 = Vector2.zero;

                idx++;
                verts[idx].position = new Vector3(tempUICurveData.Postion[i].x + xd, tempUICurveData.Postion[i].y - yd);
                verts[idx].color = tempUICurveData.Ccolor;
                verts[idx].uv0 = Vector2.zero;

                idx++;
                verts[idx].position = new Vector3(tempUICurveData.Postion[i - 1].x + xd, tempUICurveData.Postion[i - 1].y - yd);
                verts[idx].color = tempUICurveData.Ccolor;
                verts[idx].uv0 = Vector2.zero;

                varVerHeler.AddUIVertexQuad(verts);
            }
        }

    }
    #endregion

    #region [PublicTools]
    public void AddCurveData(int varID, UICurveData varCurveData)
    {
        mCurveData.Add(varID, varCurveData);
        SetAllDirty();
    }
    public void Clear()
    {
        mCurveData.Clear();
        SetAllDirty();
    }
    public void RemovePointIDs(params int[] varRemovepoints)
    {
        List<int> tempL = new List<int>();
        tempL.AddRange(varRemovepoints);
        RemovePointIDs(tempL);
    }
    public void RemovePointIDs(List<int> varRemovePoints)
    {
        foreach (var i in varRemovePoints)
        {
            if (!mCurveData.ContainsKey(i)) continue;
            mCurveData.Remove(i);
        }
        SetAllDirty();
    }
    #endregion
}