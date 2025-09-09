using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneLoader : MonoBehaviour
{
    AsyncOperation op;
    public Button sceneSwitch;
    public Slider progressLoad;
    
    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    public void FinishLoad()
    {
        op.allowSceneActivation = true;
    }

    private IEnumerator LoadSceneAsync()
    {
        op = SceneManager.LoadSceneAsync(1);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            progressLoad.value = op.progress;
            yield return null;
        }

        progressLoad.value = 1;
        sceneSwitch.interactable = true;
    }
}