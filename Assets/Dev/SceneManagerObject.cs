using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    public static SceneManagerObject instance;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public void LoadSpecificScene(int index)
    {
        SceneManager.LoadScene(index);

        if (BPM.BPMinstance != null)
        {
            Destroy(BPM.BPMinstance.gameObject);
        }
    }

    public void OnGameStart()
    {

    }

}
