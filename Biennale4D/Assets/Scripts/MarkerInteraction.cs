using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerInteraction : MonoBehaviour {
    public GameObject markerCanvas;
    public Material materialOutline;
    public float radius = 0.7f;
    public float linewidth = 0.02f;

    // Use this for initialization
    void Start () {

        GameObject circle = new GameObject { name = "Circle" };
        circle.transform.parent = markerCanvas.transform;
        circle.transform.localPosition = new Vector3(0f, 0f, 0f);
        circle.transform.localRotation = Quaternion.Euler(180, 90, 90);
        circle.transform.localScale = new Vector3(100, 100, 100);

        circle.DrawCircle(radius, linewidth);
        circle.GetComponent<LineRenderer>().material = materialOutline;
        circle.GetComponent<LineRenderer>().numCornerVertices = 90;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
