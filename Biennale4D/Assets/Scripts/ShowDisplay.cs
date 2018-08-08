﻿using UnityEngine;
using System.Collections;

/*
	function to show display when user enters a given radius of designated marker object
    display can either be the display item itself or a parent object that contains multiple nested items
    created on 19th july 2018 by k2

    IMPORTANT:
 	rendering mode of the shader of the to "display" assigned game object shout be set to "Transparent" (Legacy Shaders/Transparent/Diffuse) in order to be able to manipulate the alpha value
*/

public class ShowDisplay : MonoBehaviour
{

    public float radius = 2.0f;
    public float speed = 1.0f;
    public bool isVisibleAtStart = false;
    public GameObject display;
    public GameObject collider;
    public GameObject marker;

    public Material markerMaterial;
    public Material markerMaterialActive;
    //public Text markerText;

    private Transform[] displayComponents;

    private float distance;
    private bool isOutside = true;
    private float alphaHide = 0f;
    private float alphaShow = 1f;

    // set update frequence for transitions (e.g. fade)
    private float frequence = 90;
    private float updateFrequence;

    private Color displayCol;
    private Color markerCol;
    private Color markerMaterialCol;
    private Color markerMaterialActiveCol;
    private Color currentColor;

    // Use this for initialization
    void Start()
    {
        // calculate update frequence
        updateFrequence = 1 / frequence;


        // get all child objects of the selected display object, including self
        displayComponents = display.GetComponentsInChildren<Transform>();

        foreach (Transform comp in displayComponents)
        {
            if (comp.GetComponent<MeshFilter>() != null)
            {
                Debug.Log(comp.name);
                //comp.GetComponent<Renderer>().enabled = false;
            }
        }


        // set alpha of all display components to given visibility parameter
        foreach (Transform comp in displayComponents)
        {
            // check if component has a mesh
            if (comp.GetComponent<MeshFilter>() != null)
            {
                displayCol = comp.GetComponent<Renderer>().material.color;
                if (isVisibleAtStart)
                {
                    displayCol.a = alphaShow;
                }
                else
                {
                    displayCol.a = alphaHide;
                }
                comp.GetComponent<Renderer>().material.color = displayCol;
            }
        }


        // define colors
        markerCol = marker.GetComponent<Renderer>().material.color;
        markerMaterialCol = markerMaterial.color;
        markerMaterialActiveCol = markerMaterialActive.color;


        // set marker material
        if (isOutside)
        {
            marker.GetComponent<Renderer>().material = markerMaterial;
        }
        else
        {
            marker.GetComponent<Renderer>().material = markerMaterialActive;
        }


        // instanciate distance --> just for unity :-)
        distance = 1000f;
    }


    // Update is called once per frame
    void Update()
    {

        // show display 
        distance = Vector3.Distance(Camera.main.transform.position, collider.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < radius && isOutside)
        {
            isOutside = false;
            StopAllCoroutines();

            // check if display is just a parent object or the actual display
            if (display.GetComponent<MeshFilter>() != null)
            {
                StartCoroutine(FadeIn(speed, alphaShow));
            }
            else
            {
                foreach (Transform comp in displayComponents)
                {
                    // check if component has a mesh
                    if (comp.GetComponent<MeshFilter>() != null)
                    {
                        StartCoroutine(FadeComponentIn(comp, speed, alphaShow));
                    }
                }
            }

            // change color of marker object
            StartCoroutine(LerpColor(speed, markerMaterialActiveCol));
        }

        if (!isOutside && distance > radius)
        {
            isOutside = true;
            StopAllCoroutines();

            // check if display is just a parent object or the actual display
            if (display.GetComponent<MeshFilter>() != null)
            {
                StartCoroutine(FadeOut(speed, alphaHide));
            }
            else
            {
                foreach (Transform comp in displayComponents)
                {
                    // check if component has a mesh
                    if (comp.GetComponent<MeshFilter>() != null)
                    {
                        StartCoroutine(FadeComponentOut(comp, speed, alphaHide));
                    }
                }
            }

            // change color of marker object
            StartCoroutine(LerpColor(speed, markerMaterialCol));
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
        float alphaIncr = alphaSpectrum / (fadeTime * frequence);

        // fade in
        for (float alpha = c.a; alpha <= targetOpacity; alpha += alphaIncr)
        {
            c.a += alphaIncr;
            display.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(updateFrequence);
        }
    }


    IEnumerator FadeComponentIn(Transform comp, float fadeTime, float targetOpacity)
    {
        // get current color of material
        Color c = comp.GetComponent<Renderer>().material.color;
        // calculate difference between current opacity and target opacity
        float alphaSpectrum = Mathf.Abs(targetOpacity - c.a);
        // calculate incrementation steps
        float alphaIncr = alphaSpectrum / (fadeTime * frequence);

        // fade in
        for (float alpha = c.a; alpha <= targetOpacity; alpha += alphaIncr)
        {
            c.a += alphaIncr;
            comp.GetComponent<Renderer>().material.color = c;
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


    IEnumerator FadeComponentOut(Transform comp, float fadeTime, float targetOpacity)
    {
        Debug.Log("FadeOut-Coroutine started");

        // get current color of material
        Color c = comp.GetComponent<Renderer>().material.color;
        // calculate difference between current opacity and target opacity
        float alphaSpectrum = Mathf.Abs(targetOpacity - c.a);
        // calculate incrementation steps
        float alphaDecr = alphaSpectrum / (fadeTime * frequence);

        // fade out
        for (float alpha = c.a; alpha >= targetOpacity; alpha -= alphaDecr)
        {
            c.a -= alphaDecr;
            comp.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(updateFrequence);
        }
    }


    IEnumerator LerpColor(float time, Color targetColor)
    {
        Debug.Log("LerpColor-Coroutine started");

        // get current color of material
        Color c = marker.GetComponent<Renderer>().material.color;

        float progress = 0;
        float increment = 1 / (time * frequence);
        while (progress < 1)
        {
            currentColor = Color.Lerp(c, targetColor, progress);
            marker.GetComponent<Renderer>().material.color = currentColor;
            progress += increment;
            yield return new WaitForSeconds(updateFrequence);
        }
    }
}
