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
    private float alphaHide = 0.1f;
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
            FadeInX(speed);
            //StartCoroutine("FadeIn");
        }

        if (!isOutside && distance > radius)
        {
            isOutside = true;
            FadeOutX(speed);
            //StartCoroutine("FadeOut");
            }
    }

    
    void FadeInX(float fadeTime)
    {
        //display.GetComponent<Renderer>().enabled = true;
        displayCol = display.GetComponent<Renderer>().material.color;
        displayCol.a = alphaShow;
        display.GetComponent<Renderer>().material.color = displayCol;

        /*
        //background.GetComponent<Renderer>().enabled = true;
        backgroundCol = background.GetComponent<Renderer>().material.color;
        backgroundCol.a = alphaShow;
        background.GetComponent<Renderer>().material.color = backgroundCol;
        */
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

    void FadeOutX(float fadeTime)
    {
        //display.GetComponent<Renderer>().enabled = false;
        displayCol = display.GetComponent<Renderer>().material.color;
        displayCol.a = alphaHide;
        display.GetComponent<Renderer>().material.color = displayCol;

        /*
        //background.GetComponent<Renderer>().enabled = false;
        backgroundCol = background.GetComponent<Renderer>().material.color;
        backgroundCol.a = alphaHide;
        background.GetComponent<Renderer>().material.color = backgroundCol;
        */
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


    //from https://answers.unity.com/questions/1230671/how-to-fade-out-a-game-object-using-c.html
    private IEnumerator Lerp_MeshRenderer_Color(MeshRenderer target_MeshRender, float lerpDuration, Color startLerp, Color targetLerp)
    {
        float lerpStart_Time = Time.time;
        float lerpProgress;
        bool lerping = true;
        while (lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;
            if (target_MeshRender != null)
            {
                target_MeshRender.material.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
            }
            else
            {
                lerping = false;
            }


            if (lerpProgress >= lerpDuration)
            {
                lerping = false;
            }
        }
        yield break;
    }


    IEnumerator FadeIn()
    {
        Debug.Log("FadeIn-Coroutine started");
        for (float f= 0.05f; f<=1; f+= 0.05f)
        {
            Color c = displayRend.material.color;
            c.a = f;
            displayRend.material.color = c;
            Debug.Log("FadeIn-Coroutine f: " + f);
        }
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator FadeOut()
    {
        Debug.Log("FadeOut-Coroutine started");
        for (float f = 1.0f; f >= 0; f -= 0.05f)
        {
            Color c = displayRend.material.color;
            c.a = f;
            displayRend.material.color = c;
            Debug.Log("FadeOut-Coroutine f: " + f);
        }
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = displayRend.material.color;
            c.a = f;
            displayRend.material.color = c;
            yield return null;
        }
    }
}


