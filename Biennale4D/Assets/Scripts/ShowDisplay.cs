using UnityEngine;
using System.Collections;

/*
	function to show display when user enters a given radius of designated marker object
    created on 19th july 2018 by k2

    IMPORTANT:
 	rendering mode of the shader of the to "display" assigned game object shout be set to "Transparent" (Legacy Shaders/Transparent/Diffuse) in order to be able to manipulate the alpha value
*/

public class ShowDisplay : MonoBehaviour {
   
    public float radius = 2.5f;
    public float speed = 3.0f;
    public GameObject display;
    public GameObject marker;

    //private GameObject[] displayComponents;

    private float distance;
    private bool isOutside = true;
    private float alphaHide = 0f;
    private float alphaShow = 1f;

    private Color displayCol;
    private Color displayHidden;
    private Color displayShow;
    private Material displayMat;
    private Renderer displayRend;


    // Use this for initialization
    void Start () {
        //displayComponents = display.GetComponentsInChildren<GameObject>();
    
        // set display to invisible
        display.GetComponent<Renderer>().enabled = true;

        /*
        foreach (GameObject comp in displayComponents)
        {
            comp.GetComponent<Renderer>().enabled = false;
        }
        */

        // set alpha of display to zero
        displayRend = display.GetComponent<Renderer>();
        displayMat = display.GetComponent<Renderer>().material;

        displayCol = display.GetComponent<Renderer>().material.color;
        displayCol.a = alphaShow;
        displayShow = displayCol;
        displayCol.a = alphaHide;
        displayHidden = displayCol;
        display.GetComponent<Renderer>().material.color = displayCol;

        distance = 1000f;
    }

    // Update is called once per frame
    void Update () {

        // show display 
        distance = Vector3.Distance(Camera.main.transform.position, marker.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < radius && isOutside)
        {
            isOutside = false;
            //FadeInX(speed);
            StopAllCoroutines();
            StartCoroutine(FadeIn(speed));
        }

        if (!isOutside && distance > radius)
        {
            isOutside = true;
            //FadeOutX(speed);
            StopAllCoroutines();
            StartCoroutine(FadeOut(speed));
            }
    }

    
    void FadeInX(float fadeTime)
    {
        //display.GetComponent<Renderer>().enabled = true;
        displayCol = display.GetComponent<Renderer>().material.color;
        displayCol.a = alphaShow;
        display.GetComponent<Renderer>().material.color = displayCol;

        /*
        var alphaSpectrum = alphaShow - alphaHide;
        var alphaIncr = alphaSpectrum / fadeTime;
        //var alphaCurrent = alphaHide;

        // fade in
        for (i = 0; i < fadeTime; ++i) {
            displayCol.a += alphaIncr;
            display.GetComponent<Renderer>().material.color = displayCol;
            yield return new WaitForSeconds(0.001f);
        }
        */
    }

    void FadeOutX(float fadeTime)
    {
        //display.GetComponent<Renderer>().enabled = false;
        displayCol = display.GetComponent<Renderer>().material.color;
        displayCol.a = alphaHide;
        display.GetComponent<Renderer>().material.color = displayCol;

        Debug.Log("hide display");
    }



    IEnumerator FadeIn(float fadeTime)
    {
        Debug.Log("FadeIn-Coroutine started");
        Color c = display.GetComponent<Renderer>().material.color;
        for (float f= 0f; f<=alphaShow; f += 0.02f)
        {
            c.a = f;
            displayRend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut(float fadeTime)
    {
        Debug.Log("FadeOut-Coroutine started");
        Color c = display.GetComponent<Renderer>().material.color;
        for (float f = 1.0f; f >= alphaHide; f -= 0.02f)
        {
            c.a = f;
            displayRend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }        
    }

}


