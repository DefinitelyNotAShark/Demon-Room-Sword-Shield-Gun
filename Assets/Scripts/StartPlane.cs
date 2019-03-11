using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlane : MonoBehaviour, IFightable
{
    [SerializeField]
    string nextScene = "Colloseum";
    [SerializeField]
    OVRScreenFade screenFade;

    public void GunHit()
    {
        StartCoroutine(LoadNextScene());
    }

    public void SwordHit()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        screenFade.FadeOut();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    
}
