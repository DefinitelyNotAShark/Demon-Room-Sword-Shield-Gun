using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField]
    string nextScene = "MainMenu";
    [SerializeField]
    OVRScreenFade screenFade;

    private void OnEnable()
    {
        SpawnEnemy.WavesCompleted += LoadWhenWavesDone;
    }

    private void OnDisable()
    {
        SpawnEnemy.WavesCompleted -= LoadWhenWavesDone;
    }

    private void LoadWhenWavesDone()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        screenFade.FadeOut();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
