using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        SceneManagerObject.instance.LoadSpecificScene(1);
    }

    public void OnDie()
    {
        SceneManagerObject.instance.LoadSpecificScene(0);
    }
}
