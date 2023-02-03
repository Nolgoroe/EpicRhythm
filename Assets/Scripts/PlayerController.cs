using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool inputFailed;

    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode jump;

    private bool keyPressed;
    private bool inAir;

    void Start()
    {

    }

    void Update()
    {
        if (BPM.beatFull)
        {
            keyPressed = false;
            inputFailed = false;
            if (inAir)
            {
                Drop();
            }
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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft(new Vector3(transform.position.x - 2, transform.position.y, transform.position.z));
            if (transform.position.x >= -2) transform.position = new Vector3(-2, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight(new Vector3(transform.position.x + 2, transform.position.y, transform.position.z));
            if (transform.position.x >= 2) transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z));
            if (transform.position.y >= 2) transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Crouch();
        }
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
    void Jump(Vector3 target)
    {
        keyPressed = true;
        inAir = true;
        BPM.BPMinstance.ResetBeatActionTimer();
        LeanTween.move(gameObject, target, 0).setEase(LeanTweenType.linear);
    }
    void Drop()
    {
        LeanTween.move(gameObject, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), 0).setEase(LeanTweenType.linear);
        inAir = false;
    }
    void Crouch()
    {

    }
    void StandUp()
    {

    }
}
