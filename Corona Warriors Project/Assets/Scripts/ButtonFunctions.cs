using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = GameObject.Find("Scene Transition").GetComponent<TransitionManager>();
    }

    public void Play()
    {
        transitionManager.sceneName = "Game  (with Images)";
        transitionManager.ChangeScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
