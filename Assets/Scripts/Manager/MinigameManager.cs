using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector endlessrunDirector;
    [SerializeField] private PlayableDirector battleDirector;
    public static MinigameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }
    
    public void EndlessRun() {
        endlessrunDirector.Play();
    }

    public void SwitchScene(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void Battle() {
        battleDirector.Play();
    }
}
