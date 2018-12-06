using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowLineTran : MonoBehaviour {

    public RectTransform rect;
    public RectTransform myrect;

    public Vector3 rectpos;
    public Vector3 myrectpos;
	// Use this for initialization
	void Start () {
        myrectpos = myrect.localPosition;
        rectpos = rect.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
     

        myrect.localPosition = new Vector3(myrectpos.x + (rect.localPosition.x- rectpos.x) , myrectpos.y +(rect.localPosition.y - rectpos.y)  );
  //      Debug.Log(rect.localPosition.x - rectpos.x);
	}
}
