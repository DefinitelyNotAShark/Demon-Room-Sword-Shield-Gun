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

    public void GunHit(Vector3 collisionTransform)
    {
        StartCoroutine(LoadNextScene());
    }

    public void SwordHit(Vector3 collisionTransform)
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        Debug.Log("Loading next Scene");
        screenFade.FadeOut();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    
}
