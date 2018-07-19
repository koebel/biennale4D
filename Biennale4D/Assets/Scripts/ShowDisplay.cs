using UnityEngine;
using System.Collections;

/*
	function to show display when user enters a given radius of designated marker object
    created on 19th july 2018 by k2

    IMPORTANT:
 	rendering mode of the shader of the to "display" assigned game object shout be set to "Transparent" in order to be able to manipulate the alpha value
*/

public class ShowDisplay : MonoBehaviour {
   
    public float radius = 2.5f;
    public float speed = 3.0f;
    public GameObject display;
    public GameObject marker;

    private float distance;
    private bool isOutside = true;
    private float alphaHide = 0f;
    private float alphaShow = 1f;

    private Color displayCol;
    private Material displayMat;


    // Use this for initialization
    void Start () {

        // set display to invisible
        display.GetComponent<Renderer>().enabled = false;

        // set alpha of display to zero
        displayCol = display.GetComponent<Renderer>().material.color;
        displayCol.a = 0f;
        display.GetComponent<Renderer>().material.color = displayCol;

        distance = 1000f;
    }

    // Update is called once per frame
    void Update () {

        // show display 
        distance = Vector3.Distance(Camera.main.transform.position, marker.transform.position);
        //Debug.Log("Distance: " + distance);

        // TODO: isOutside doesn't changes properly if User teleports himself on top layer of marker

        if (distance < radius && isOutside)
        {
            isOutside = false;         
            FadeIn(speed);
        }

        if (!isOutside && distance > radius)
        {
            isOutside = true;
            FadeOut(speed);
        }
    }


    void FadeIn(float fadeTime)
    {
        //display.SetActive(true);
        display.GetComponent<Renderer>().enabled = true;

        displayCol.a = alphaShow;
        display.GetComponent<Renderer>().material.color = displayCol;

        /*
        // .material getter clones the material, 
        // so cache this copy in a member variable so we can dispose of it when we're done.
        displayMat = display.GetComponent<Renderer>().material;

        // Start a coroutine to fade the material to zero alpha over 3 seconds.
        // Caching the reference to the coroutine lets us stop it mid-way if needed.
        StartCoroutine(FadeTo(displayMat, alphaShow, fadeTime));
        */

        // do something
        // TODO: make Object Fade in
        Debug.Log("show display");

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

    void FadeOut(float fadeTime)
    {
        //display.SetActive(false);
        display.GetComponent<Renderer>().enabled = false;

        displayCol.a = alphaHide;
        display.GetComponent<Renderer>().material.color = displayCol;

        /*
        // .material getter clones the material, 
        // so cache this copy in a member variable so we can dispose of it when we're done.
        displayMat = display.GetComponent<Renderer>().material;

        // Start a coroutine to fade the material to zero alpha over 3 seconds.
        // Caching the reference to the coroutine lets us stop it mid-way if needed.
        StartCoroutine(FadeTo(displayMat, alphaHide, fadeTime));
        */

        // do something
        // TODO: make Object FadeOut
        Debug.Log("hide display");
    }


    // from https://gamedev.stackexchange.com/questions/142791/how-can-i-fade-a-game-object-in-and-out-over-a-specified-duration

    // Define an enumerator to perform our fading.
    // Pass it the material to fade, the opacity to fade to (0 = transparent, 1 = opaque),
    // and the number of seconds to fade over.
    IEnumerator FadeTo(Material material, float targetOpacity, float duration)
    {

        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < duration)
        {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            material.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
    }

}


