using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public RectTransform otherrect;
    public RectTransform myrect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        myrect.localPosition =new Vector3 ( otherrect.localPosition.x, myrect.localPosition.y );
	}
}
