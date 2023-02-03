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
    public static bool inAir;
    //public static bool inputFailed;

    public ActionType currentAction = ActionType.None;

    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode jump;

    //private bool keyPressed;

    float timeInterval = 0;

    public float limitXleft = -2;
    public float limitXRight = 2;
    public float limitYUp = 2;

    Color matColor;
    Color baseColor;

    int XPos = 0;
    int YPos = 0;

    public ColorOnBeat colorOnBeat;
    public RipplePostProcessor rippleEffect;
    private bool isCrouched;
    private bool skipBeat;



    //Hp system
    [SerializeField] GameObject[] hearts;
    [SerializeField] int hp;
    private int currentHp;
    private bool bumped;

    LeanTweenType leanType = LeanTweenType.easeOutElastic;
    private void Start()
    {
        currentAction = ActionType.None;
        baseColor = colorOnBeat.materialColor;
        currentHp = hp;
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

        HeartsUpdate();

        if (BPM.beatFull)
        {
            if (!bumped)
            {
                colorOnBeat.materialColor = baseColor;
                colorOnBeat.Colorize();
            }

            OnBeatOccurrence();
        }


    }
    void OnBeatOccurrence()
    {
        //keyPressed = false;
        //inputFailed = false;
        bumped = false;
        //matColor = baseColor;
        //colorOnBeat.materialColor = matColor;
        if (inAir)
        {
            HandleMovement(inAir);
            skipBeat = true;
            inAir = false;
        }

        if (isCrouched)
        {
            HandleMovement(inAir);

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

    //on Bump
    private void OnCollisionStay(Collision collision)
    {
        if (BPM.beatFullAction && !bumped)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Bump!");
                bumped = true;
                currentHp -= 1;
                colorOnBeat.materialColor = Color.red;
                colorOnBeat.Colorize();
                if (IsDead())
                {
                    LevelManager.instance.OnDie();
                }
            }
        }
    }
    void HeartsUpdate()
    {
        switch (currentHp)
        {
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[4].SetActive(false);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(false);
                hearts[3].SetActive(false);
                hearts[4].SetActive(false);
                break;
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                hearts[3].SetActive(false);
                hearts[4].SetActive(false);
                break;
            case 4:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                hearts[3].SetActive(true);
                hearts[4].SetActive(false);
                break;
            case 5:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                hearts[3].SetActive(true);
                hearts[4].SetActive(true);
                break;
        }
    }
    bool IsDead()
    {
        if (currentHp == 0) return true;
        else return false;
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
    void HandleMovement(bool inAir)
    {
        if (currentAction == ActionType.None) return;

        if (inAir)
        {
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
                default:
                    break;
            }
        }
        currentAction = ActionType.None;
    }

    void MoveLeft()
    {
        XPos -= 2;
        if (XPos < -2) XPos = -2;

        BPM.BPMinstance.ResetBeatActionTimer();
        //keyPressed = true;
        LeanTween.moveX(gameObject, XPos, timeInterval).setEase(leanType);
    }

    void MoveRight()
    {
        XPos += 2;

        if (XPos > 2) XPos = 2;
        //keyPressed = true;
        BPM.BPMinstance.ResetBeatActionTimer();
        LeanTween.moveX(gameObject, XPos, timeInterval).setEase(leanType);
    }
    void Jump()
    {
        YPos += 2;

        if (YPos > 2) YPos = 2;
        //keyPressed = true;
        inAir = true;
        BPM.BPMinstance.ResetBeatActionTimer();
        LeanTween.moveY(gameObject, YPos, timeInterval).setEase(leanType);
    }

    void DoActionsAfterBeatSkip()
    {
        BPM.BPMinstance.ResetBeatActionTimer();

        StandUp();

        if (transform.position.y > 0.3f)
        {
            LeanTween.moveY(gameObject, 0.25f, timeInterval).setEase(leanType);
            rippleEffect.Ripple();
        }
    }
    void Crouch()
    {
        isCrouched = true;
        LeanTween.scaleY(gameObject, 1, timeInterval).setEase(leanType);
    }
    void StandUp()
    {
        if (transform.localScale.y < 2)
        {
            LeanTween.scaleY(gameObject, 2, timeInterval).setEase(leanType);
        }
    }

   
}
