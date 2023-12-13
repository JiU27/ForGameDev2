using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class OpponentAIControl : MonoBehaviour
{
    public enum OpAIStage
    {
        OpIdle,
        OpInfluence,
        OpTired,
        AngerPlus,
        Anger,
        Think,
        Escape,
        None
    }


    public OpAIStage currentStage;
    public float anger, think, escape;
    public GameObject d, e, f, g, stun, m, n, o, p;
    public GameObject Opponent, Player, Op1;
    public MainControl mainControl;
    public Button Inf1, Inf2, Inf3, Inf4;
    public Button Tac1, Tac2, Tac3;
    public Animator opponentAnimator;
    public bool isMoving = false;

    private bool isButtonPressed = false;
    private bool isTimerActive = false;
    private float timerDuration = 0f; 
    private float timerElapsed = 1.0f;
    private float randomChanceForAngerPlus;
    private OpAIStage previousStage;
    private Vector3 initialOpponentPosition;

    Coroutine MoveRutine;



    void Start()
    {
        currentStage = OpAIStage.OpIdle;
        mainControl.PlayerHPText.text = mainControl.OpcurrentHP.ToString();
        mainControl.PlayerHPText.text = mainControl.currentHP.ToString();
        opponentAnimator = Op1.GetComponent<Animator>();
        ResetStates();

        Tac1.onClick.AddListener(() => OnButtonClicked());
        Tac2.onClick.AddListener(() => OnButtonClicked());
        Tac3.onClick.AddListener(() => OnButtonClicked());

        Inf1.onClick.AddListener(() => OnInfluenceButtonClicked(1));
        Inf2.onClick.AddListener(() => OnInfluenceButtonClicked(2));
        Inf3.onClick.AddListener(() => OnInfluenceButtonClicked(3));
        Inf4.onClick.AddListener(() => OnInfluenceButtonClicked(4));
    }

    void FixedUpdate()
    {
        if (MoveRutine == null)
        {


            if (currentStage != previousStage)
            {
                previousStage = currentStage;
                Debug.Log("Previous Stage updated to: " + previousStage);
            }


            switch (currentStage)
            {
                case OpAIStage.OpIdle:
                    ResetStates();
                    if (mainControl.currentStage == MainControl.PlayerStage.Influence)
                    {
                        currentStage = OpAIStage.OpInfluence;
                    }
                    break;
                case OpAIStage.OpInfluence:

                    opponentAnimator.SetBool("OpR&B", false);
                    opponentAnimator.SetBool("OpMove", false);
                    opponentAnimator.SetBool("OpLose", false);
                    break;
                case OpAIStage.OpTired:
                    ResetStates();
                    opponentAnimator.SetBool("OpLose", true);
                    stun.SetActive(true);
                    if (mainControl.currentStage == MainControl.PlayerStage.TacticChoose)
                    {
                        ResetUI();
                        currentStage = OpAIStage.OpIdle;
                    }
                    break;
                case OpAIStage.Anger:
                    Anger();
                    //currentStage = OpAIStage.OpIdle;
                    break;
                case OpAIStage.AngerPlus:
                    AngerPlus();
                    //currentStage = OpAIStage.OpIdle;
                    break;
                case OpAIStage.Escape:
                    Escape();
                    //currentStage = OpAIStage.OpIdle;
                    break;
                case OpAIStage.Think:
                    Think();
                    //currentStage = OpAIStage.OpIdle;
                    break;
            }
        }



    }

    public void OnInfluenceButtonClicked(int influenceType)
    {
        if (currentStage != OpAIStage.OpTired)
        {
            StartTimer();
            float total = anger + think + escape;
            float a = anger / total;
            float b = think / total;
            float c = escape / total;

            float rand = Random.Range(0f, 1f);

            switch (influenceType)
            {
                case 1:
                    d.SetActive(true);
                    initialOpponentPosition = Opponent.transform.position;
                    ResetUI();
                    //Debug.Log("Initial Opponent Position: " + initialOpponentPosition);

                    if (randomChanceForAngerPlus == 0f)
                    {
                        randomChanceForAngerPlus = Random.Range(0f, 1f);
                    }

                    if (randomChanceForAngerPlus <= 0.6f)
                    {
                        //Debug.Log("ChooseAnger");
                        currentStage = OpAIStage.Anger;
                    }
                    else
                    {
                        //Debug.Log("ChooseAngerPlus");
                        currentStage = OpAIStage.AngerPlus;
                    }
                    break;
                case 2:
                    e.SetActive(true);
                    initialOpponentPosition = Opponent.transform.position;
                    ResetUI();

                    if ((e.activeSelf && rand <= b))
                    {
                        currentStage = OpAIStage.Think;
                    }
                    else
                    {
                        currentStage = OpAIStage.AngerPlus;
                    }
                    break;
                case 3:
                    f.SetActive(true);
                    initialOpponentPosition = Opponent.transform.position;
                    ResetUI();
                    //Debug.Log("Initial Opponent Position: " + initialOpponentPosition);

                    if ((f.activeSelf && rand <= a))
                    {
                        currentStage = OpAIStage.Anger;
                    }
                    else
                    {
                        currentStage = OpAIStage.Escape;
                    }
                    break;
                case 4:
                    g.SetActive(true);
                    initialOpponentPosition = Opponent.transform.position;
                    ResetUI();
                    //Debug.Log("Initial Opponent Position: " + initialOpponentPosition);

                    if ((g.activeSelf && rand <= a))
                    {
                        currentStage = OpAIStage.Anger;
                    }
                    else if ((g.activeSelf && rand <= a + b))
                    {
                        currentStage = OpAIStage.Escape;
                    }
                    else
                    {
                        currentStage = OpAIStage.Think;
                    }
                    break;
            }
        }
            
    }

    public void AngerPlus()
    {
        opponentAnimator.SetBool("OpR&B", true);
        if (isButtonPressed)
        {
                d.SetActive(false);
                e.SetActive(false);
                f.SetActive(false);
                g.SetActive(false);
                m.SetActive(true);
            //Debug.Log("m active state: " + m.activeSelf);

            MoveTowardsPlayer(3f);
            currentStage = OpAIStage.OpTired;
        }
    }

    public void Anger()
    {
        opponentAnimator.SetBool("OpR&B", true);
        if (isButtonPressed)
        {
            d.SetActive(false);
            e.SetActive(false);
            f.SetActive(false);
            g.SetActive(false);
            n.SetActive(true);

            MoveTowardsPlayer(3f);
            currentStage = OpAIStage.OpIdle;
        }
    }

    public void Think()
    {
        opponentAnimator.SetBool("OpR&B", true);
        if (isButtonPressed)
        {
            d.SetActive(false);
            e.SetActive(false);
            f.SetActive(false);
            g.SetActive(false);
            o.SetActive(true);

            mainControl.OpcurrentHP = Mathf.Min(mainControl.OpcurrentHP + 1, mainControl.MaxOpHP);
            mainControl.OpponentHPText.text = mainControl.OpcurrentHP.ToString();

            if (isTimerActive)
            {
                timerElapsed += Time.deltaTime;

                if (timerElapsed >= timerDuration)
                {
                    currentStage = OpAIStage.OpIdle;

                    StopTimer();
                    isButtonPressed = false;
                }
            }
        }

    }

    public void Escape()
    {
        opponentAnimator.SetBool("OpR&B", true);
        if (isButtonPressed)
        {
            d.SetActive(false);
            e.SetActive(false);
            f.SetActive(false);
            g.SetActive(false);
            p.SetActive(true);

            MoveAwayFromPlayer(3f);
            isButtonPressed = false;
            currentStage = OpAIStage.OpIdle;
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {


        if (collider.gameObject.tag == "Player")
        {
            
            //Debug.Log("OnTriggerEnter2D called");
            if (previousStage == OpAIStage.AngerPlus)
            {
                //Debug.Log("AngerPlus Hit");
                mainControl.currentHP = Mathf.Max(mainControl.currentHP - 2, mainControl.MinHp);
                mainControl.PlayerHPText.text = mainControl.currentHP.ToString();
                if (MoveRutine != null)
                {
                    StopCoroutine(MoveRutine);
                    MoveRutine = null;
                }
                Opponent.transform.position = initialOpponentPosition;
                if (isTimerActive)
                {
                    timerElapsed += Time.deltaTime;

                    if (timerElapsed >= timerDuration)
                    {
                        opponentAnimator.SetBool("OpLose", true); 
                        currentStage = OpAIStage.OpTired;

                        StopTimer();
                        isButtonPressed = false;
                    }
                }
            }
            else if (previousStage == OpAIStage.Anger)
            {
                //Debug.Log("Anger Hit");
                mainControl.currentHP = Mathf.Max(mainControl.currentHP - 1, mainControl.MinHp);
                mainControl.PlayerHPText.text = mainControl.currentHP.ToString();
                if(MoveRutine != null)
                {
                    StopCoroutine(MoveRutine);
                    MoveRutine = null;
                }
                Opponent.transform.position = initialOpponentPosition;
                if (isTimerActive)
                {
                    timerElapsed += Time.deltaTime;

                    if (timerElapsed >= timerDuration)
                    {
                        currentStage = OpAIStage.OpIdle;

                        StopTimer();
                        isButtonPressed = false;
                    }
                }
            }
            else if (previousStage == OpAIStage.Think)
            {
                //Debug.Log("Think be Hit");
                mainControl.OpcurrentHP = Mathf.Max(mainControl.OpcurrentHP - 1, mainControl.MinOpHp);
                mainControl.OpponentHPText.text = mainControl.OpcurrentHP.ToString();
                if (isTimerActive)
                {
                    timerElapsed += Time.deltaTime;

                    if (timerElapsed >= timerDuration)
                    {
                        currentStage = OpAIStage.OpIdle;

                        StopTimer();
                        isButtonPressed = false;
                    }
                }
            }
        }
        else
        {
            if (previousStage == OpAIStage.Think)
            {
                //Debug.Log("Think Recover");
                if (isTimerActive)
                {
                    timerElapsed += Time.deltaTime;

                    if (timerElapsed >= timerDuration)
                    {
                        currentStage = OpAIStage.OpIdle;

                        StopTimer();
                        isButtonPressed = false;
                    }
                }
            }
            else if (previousStage == OpAIStage.AngerPlus)
            {
                //Debug.Log("AngerPluse GoBack");
                if (isTimerActive)
                {
                    timerElapsed += Time.deltaTime;

                    if (timerElapsed >= timerDuration)
                    {
                        currentStage = OpAIStage.OpTired;

                        StopTimer();
                        isButtonPressed = false;

                    }
                }
            }
            else if (previousStage == OpAIStage.Anger)
            {
                //Debug.Log("Anger GoBack");
                if (isTimerActive)
                {
                    timerElapsed += Time.deltaTime;

                    if (timerElapsed >= timerDuration)
                    {
                        currentStage = OpAIStage.OpIdle;

                        StopTimer();
                        isButtonPressed = false;
                    }
                }
            }
        }
    }


    public void ResetStates()
    {
        anger = 1;
        think = 1;
        escape = 1;
        d.SetActive(false);
        e.SetActive(false);
        f.SetActive(false);
        g.SetActive(false);
        
        stun.SetActive(false);
        isButtonPressed = false;
        randomChanceForAngerPlus = 0f;

    }

    private void ResetUI()
    {
        m.SetActive(false);
        n.SetActive(false);
        o.SetActive(false);
        p.SetActive(false);
    }

    public void OnButtonClicked()
    {
        isButtonPressed = true;
    }

    private void StartTimer()
    {
        isTimerActive = true;
        timerElapsed = 0.0f;
    }

    private void StopTimer()
    {
        isTimerActive = false;
        timerElapsed = 0.0f;
    }

    private void MoveTowardsPlayer(float distance)
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 opponentPosition = Opponent.transform.position;
        Vector3 targetPosition;

        if (opponentPosition.x == playerPosition.x)
        {
            targetPosition = new Vector3(opponentPosition.x + distance, opponentPosition.y, opponentPosition.z);
        }
        else if (opponentPosition.x < playerPosition.x)
        {
            targetPosition = new Vector3(Mathf.Min(opponentPosition.x + distance, 6), opponentPosition.y, opponentPosition.z);
        }
        else
        {
            targetPosition = new Vector3(Mathf.Max(opponentPosition.x - distance, -6), opponentPosition.y, opponentPosition.z);
        }

        float rotationY = playerPosition.x < opponentPosition.x ? 0 : -180;
        Opponent.transform.rotation = Quaternion.Euler(0, rotationY, 0);

        MoveRutine = StartCoroutine(MoveOpponent(targetPosition, 0.5f));
    }

    private void MoveAwayFromPlayer(float distance)
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 opponentPosition = Opponent.transform.position;
        Vector3 targetPosition;

        if (opponentPosition.x == playerPosition.x)
        {
            targetPosition = new Vector3(opponentPosition.x - distance, opponentPosition.y, opponentPosition.z);
        }
        else if (opponentPosition.x < playerPosition.x)
        {
            targetPosition = new Vector3(Mathf.Max(opponentPosition.x - distance, -6), opponentPosition.y, opponentPosition.z);
        }
        else
        {
            targetPosition = new Vector3(Mathf.Min(opponentPosition.x + distance, 6), opponentPosition.y, opponentPosition.z);
        }

        float rotationY = playerPosition.x < opponentPosition.x ? 0 : -180;
        Opponent.transform.rotation = Quaternion.Euler(0, rotationY, 0);

        MoveRutine = StartCoroutine(MoveOpponent(targetPosition, 0.5f));
    }

    IEnumerator MoveOpponent(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = Opponent.transform.position;
        float time = 0;

        opponentAnimator.SetBool("OpMove", true);
        //Debug.Log("OpMove isTrigger");

        while (time < duration)
        {
            Opponent.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Opponent.transform.position = targetPosition;  
        opponentAnimator.SetBool("OpMove", false);  

        MoveRutine = null;
    }

}
