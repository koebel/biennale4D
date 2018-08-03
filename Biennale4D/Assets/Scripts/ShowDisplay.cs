using UnityEngine;
using System.Collections;

/*
	function to show display when user enters a given radius of designated marker object
    created on 19th july 2018 by k2

    IMPORTANT:
 	rendering mode of the shader of the to "display" assigned game object shout be set to "Transparent" (Legacy Shaders/Transparent/Diffuse) in order to be able to manipulate the alpha value
*/

public class ShowDisplay : MonoBehaviour {
   
    public float radius = 2.0f;
    public float speed = 1.0f;
    public bool isVisibleAtStart = false;
    public GameObject display;
    public GameObject marker;

    //private GameObject[] displayComponents;

    private float distance;
    private bool isOutside = true;
    private float alphaHide = 0f;
    private float alphaShow = 1f;

    // set update frequence for transitions (e.g. fade)
    private float frequence = 90;
    private float updateFrequence;

    private Color displayCol;


    // Use this for initialization
    void Start () {
        updateFrequence = 1 / frequence;


        //displayComponents = display.GetComponentsInChildren<GameObject>();

        /*
        foreach (GameObject comp in displayComponents)
        {
            comp.GetComponent<Renderer>().enabled = false;
        }
        */


        // set alpha of display to given visibility parameter
        displayCol = display.GetComponent<Renderer>().material.color;
        if (isVisibleAtStart)
        {
            displayCol.a = alphaShow;
        }
        else
        {
            displayCol.a = alphaHide;
        }
        display.GetComponent<Renderer>().material.color = displayCol;

        // instanciate distance --> just for unity :-)
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
            StopAllCoroutines();
            StartCoroutine(FadeIn(speed, alphaShow));
        }

        if (!isOutside && distance > radius)
        {
            isOutside = true;
            StopAllCoroutines();
            StartCoroutine(FadeOut(speed, alphaHide));
            }
    }


    IEnumerator FadeIn(float fadeTime, float targetOpacity)
    {
        Debug.Log("FadeIn-Coroutine started");

        // get current color of material
        Color c = display.GetComponent<Renderer>().material.color;
        // calculate difference between current opacity and target opacity
        float alphaSpectrum = Mathf.Abs(targetOpacity - c.a);
        // calculate incrementation steps
        float alphaIncr = alphaSpectrum / (fadeTime*frequence);

        // fade in
        for (float alpha = c.a; alpha <= targetOpacity; alpha += alphaIncr)
        {
            c.a += alphaIncr;
            display.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(updateFrequence);
        }
    }


    IEnumerator FadeOut(float fadeTime, float targetOpacity)
    {
        Debug.Log("FadeOut-Coroutine started");

        // get current color of material
        Color c = display.GetComponent<Renderer>().material.color;
        // calculate difference between current opacity and target opacity
        float alphaSpectrum = Mathf.Abs(targetOpacity - c.a);
        // calculate incrementation steps
        float alphaDecr = alphaSpectrum / (fadeTime * frequence);

        // fade out
        for (float alpha = c.a; alpha >= targetOpacity; alpha -= alphaDecr)
        {
            c.a -= alphaDecr;
            display.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(updateFrequence);
        }     
    }

}


