using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    MoveLeft,
    MoveRight,
    Jump,
    Crouch,
    None
}
public class PlayerController : MonoBehaviour
{
    //public static bool inputFailed;

    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode jump;

    //private bool keyPressed;
    private bool inAir;
    private bool isCrouched;
    private bool skipBeat;

    float timeInterval = 0;

    public float limitXleft = -2;
    public float limitXRight = 2;
    public float limitYUp = 2;

    public ActionType currentAction = ActionType.None;

    int XPos = 0;
    int YPos = 0;

    public ColorOnBeat colorOnBeat;
    private void Start()
    {
        currentAction = ActionType.None;
    }
    void Update()
    {
        timeInterval = BPM.BPMinstance.beatInterval;

        DetectPlayerInput();

        //if (BPM.beatFullAction)
        //{
        //    DetectPlayerInput();
        //    //if (!keyPressed)
        //    //{
        //    //}
        //}


        if (BPM.beatFull)
        {
            //keyPressed = false;
            //inputFailed = false;

            if (inAir)
            {
                HandleMovement();
                skipBeat = true;
                inAir = false;
            }

            if (isCrouched)
            {
                skipBeat = true;
                isCrouched = false;
            }

            if (skipBeat)
            {
                skipBeat = false;
                DoActionsAfterBeatSkip();
                return;
            }

            HandleMovement();
        }


    }

    private void DetectPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentAction = ActionType.MoveLeft;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentAction = ActionType.MoveRight;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentAction = ActionType.Jump;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentAction = ActionType.Crouch;
        }
    }

    void HandleMovement()
    {
        if (currentAction == ActionType.None) return;


        switch (currentAction)
        {
            case ActionType.MoveLeft:
                if (transform.position.x - 2 < limitXleft) return;
                MoveLeft();
                break;
            case ActionType.MoveRight:
                if (transform.position.x + 2 > limitXRight) return;
                MoveRight();
                break;
            case ActionType.Jump:
                if (transform.position.y + 2 > limitYUp) return;
                Jump();
                break;
            case ActionType.Crouch:
                if (transform.localScale.y == 1) return;
                Crouch();
                break;
            default:
                break;
        }

        currentAction = ActionType.None;
    }

    void MoveLeft()
    {
        XPos -= 2;
        if (XPos < -2) XPos = -2;

        BPM.BPMinstance.ResetBeatActionTimer();
        //keyPressed = true;
        LeanTween.moveX(gameObject, XPos, timeInterval).setEase(LeanTweenType.easeOutElastic);
    }

    void MoveRight()
    {
        XPos += 2;

        if (XPos > 2) XPos = 2;
        //keyPressed = true;
        BPM.BPMinstance.ResetBeatActionTimer();
        LeanTween.moveX(gameObject, XPos, timeInterval).setEase(LeanTweenType.easeOutElastic);
    }
    void Jump()
    {
        YPos += 2;

        if (YPos > 2) YPos = 2;
        //keyPressed = true;
        inAir = true;
        BPM.BPMinstance.ResetBeatActionTimer();
        LeanTween.moveY(gameObject, YPos, timeInterval).setEase(LeanTweenType.easeOutElastic);
    }

    void DoActionsAfterBeatSkip()
    {
        BPM.BPMinstance.ResetBeatActionTimer();

        StandUp();

        if (transform.position.y > 0.25f)
        {
            LeanTween.moveY(gameObject, 0, timeInterval).setEase(LeanTweenType.easeOutElastic);
            colorOnBeat.Colorize();
        }
    }
    void Crouch()
    {
        isCrouched = true;
        LeanTween.scaleY(gameObject, 1, timeInterval).setEase(LeanTweenType.easeOutElastic);
    }
    void StandUp()
    {
        if (transform.localScale.y < 2)
        {
            LeanTween.scaleY(gameObject, 2, timeInterval).setEase(LeanTweenType.easeOutElastic);
        }
    }

    //void ResetMoveRight()
    //{
    //    transform.position = new Vector3(XNum, transform.position.y, transform.position.z);
    //}
}
