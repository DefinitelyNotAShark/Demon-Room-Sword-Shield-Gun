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

    private bool coroutineStarted;

    public void GunHit(Vector3 collisionTransform, int bulletDamage)
    {
        StartCoroutine(LoadNextScene());
    }


    public void SwordHit(Vector3 collisionTransform, int swordDamage)
    {
        StartCoroutine(LoadNextScene());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet") && !coroutineStarted)
        {
            StartCoroutine(LoadNextScene());
            coroutineStarted = true;
        }

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
