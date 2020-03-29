using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AreaLoadCheckpoint : Checkpoint
{
    public string sceneName;

    private bool isLoaded = false;

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (!isLoaded && other.CompareTag("Player"))
        {
            isLoaded = true;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            manager.UpdateOrbs();
        }
    }
}
