using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode jump;

    private bool keyPressed;

    void Start()
    {
    }

    void Update()
    {
        if (BPM.beatFull)
        {
            keyPressed = false;
        }
        if (BPM.beatFullAction)
        {
            if (!keyPressed)
            {
                HandleMovement();
            }
        }
    }
    void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MoveLeft(new Vector3(transform.position.x - 2, transform.position.y, transform.position.z));
        if (Input.GetKeyDown(KeyCode.D))
            MoveRight(new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
    }
    void MoveLeft(Vector3 target)
    {
        BPM.BPMinstance.ResetBeatActionTimer();
        keyPressed = true;
        LeanTween.move(gameObject, target, 0).setEase(LeanTweenType.linear);
    }
    void MoveRight(Vector3 target)
    {
        keyPressed = true;
        BPM.BPMinstance.ResetBeatActionTimer();
        LeanTween.move(gameObject, target, 0).setEase(LeanTweenType.linear);
    }
}
