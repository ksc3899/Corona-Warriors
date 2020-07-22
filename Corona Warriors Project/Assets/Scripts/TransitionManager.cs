using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [HideInInspector] public string sceneName;

    private Animator animator;
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeScene()
    {
        animator.SetTrigger("Scene Transition");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
