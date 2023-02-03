using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private MeshRenderer renderer;
    private Material mat;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        mat = renderer.material;
    }

    void Update()
    {
        if (PlayerController.inputFailed)
        {
            mat.color = Color.red;
        }
        else
            mat.color = Color.blue;
    }
}
