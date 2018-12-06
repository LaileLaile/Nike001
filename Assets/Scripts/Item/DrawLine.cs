using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Vectrosity;


public class DrawLine :MonoBehaviour  {

    public List<VectorLine> LineList = new List<VectorLine>();
    private VectorLine line;
    public Transform drawTran;
    

    //Thread thread;
    public void Start()
    {
        //line = AddLine();
        //line.points2.Add(new Vector2(0, 0));
        //line.points2.Add(new Vector2(1, 1));
    }


    public int   AddLine(List<Vector2 > list )
    {
        LineList.Add(new VectorLine("Line", list, 1, LineType.Continuous));
        LineList[LineList.Count - 1].Draw();
        LineList[LineList.Count - 1].drawTransform = drawTran;
       
       // LineList[LineList.Count - 1].useViewportCoords = true;
        return LineList .Count -1;
    }
    public void Draw(int index,List<Vector2> pos )
    {

        LineList[index].points2=pos ;

        LineList[index ].Draw();
       // LineList[index].drawTransform.position -= new Vector3(100, 10);

    }

    public void Drawsetactive(int index)
    {

        LineList[index].points2 = new List<Vector2>();


        LineList[index].Draw();
        // LineList[index].drawTransform.position -= new Vector3(100, 10);

    }
    public void DrawPos(List<Vector2 > pos)
    {

    }


    private void Update()
    {
        for (int i = 0; i < LineList.Count ; i++)
        {
            LineList[i].Draw();
        }
    }
}


