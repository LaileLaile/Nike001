using UnityEngine;
using System.Collections;

public class CursorChange : MonoBehaviour
{
    public Texture2D normal;

    public Texture2D stretch;

    public Texture2D point;
    void Start()
    {
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);

      


    }

    public void stretchImage()
    {
        Cursor.SetCursor(stretch, Vector2.zero, CursorMode.Auto);
    }
    public void normalImage()
    {
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
    }

    public void PointImage()
    {
        Cursor.SetCursor(point ,Vector2.zero  , CursorMode.Auto);
    }
}
