using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://forum.unity.com/threads/simple-ui-animation-fade-in-fade-out-c.439825/

public class TintScreen : MonoBehaviour
{
    private Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    public IEnumerator FadeImage()
    {

        // lfade from transparent to opaque
        for (float i = 0; i <= .3f; i += Time.deltaTime)
        {
            image.color = new Color(255, 0, 0, i);
        }

        // fade from opaque to transparent
        for (float i = .3f; i >= 0; i -= Time.deltaTime)
        {
            image.color = new Color(255, 0, 0, i);
            yield return null;
        }
    }
}
