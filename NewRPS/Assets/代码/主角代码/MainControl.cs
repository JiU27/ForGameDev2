using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    public GameObject Player;
    public PlayerStage currentStage;
    public GameObject InfluenceUI;
    public GameObject TacticUI;
    public GameObject Talk1, Talk2, Talk3, Talk4;
    public Button Inf1, Inf2, Inf3, Inf4;
    public Button Tac1, Tac2, Tac3;
    public TextMeshProUGUI PlayerHPText, OpponentHPText;
    public int OpcurrentHP = 3;
    public int currentHP = 5;
    public int MaxOpHP, MinOpHp, MaxHp, MinHp;

    public OpponentAIControl opponentAI;
    public GameObject Opponent,Player1;
    public Animator playerAnimator;


    private int selectedTactic;
    private Vector3 initialPlayerPosition;

    Coroutine MoveRutine;

    public enum PlayerStage
    {
        Idle,
        Influence,
        TacticChoose
    }

    void Start()
    {
        MaxOpHP = OpcurrentHP;
        MaxHp = currentHP;
        MinHp = 0;
        MinOpHp = 0;
        currentStage = PlayerStage.Idle;
        InfluenceUI.SetActive(false);
        TacticUI.SetActive(false);
        playerAnimator.SetBool("Move", false);
        playerAnimator.SetBool("R&D", true);
        playerAnimator.SetBool("Lose", false);
        Talk1.SetActive(false);
        Talk2.SetActive(false);
        Talk3.SetActive(false);
        Talk4.SetActive(false);


        PlayerHPText.text = currentHP.ToString();
        OpponentHPText.text = OpcurrentHP.ToString();


        Inf1.onClick.AddListener(() => OnInfluenceButtonClicked(1));
        Inf2.onClick.AddListener(() => OnInfluenceButtonClicked(2));
        Inf3.onClick.AddListener(() => OnInfluenceButtonClicked(3));
        Inf4.onClick.AddListener(() => OnInfluenceButtonClicked(4));

        Tac1.onClick.AddListener(() => OnTacticButtonClicked(1));
        Tac2.onClick.AddListener(() => OnTacticButtonClicked(2));
        Tac3.onClick.AddListener(() => OnTacticButtonClicked(3));

        playerAnimator = Player1.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (MoveRutine == null){

            switch (currentStage)
            {
                case PlayerStage.Idle:
                    InfluenceUI.SetActive(true);
                    TacticUI.SetActive(false);
                    playerAnimator.SetBool("R&D", true);
                    currentStage = PlayerStage.Influence;
                    break;
                case PlayerStage.Influence:

                    break;
                case PlayerStage.TacticChoose:

                    break;
            }
        }
    }

    public void OnInfluenceButtonClicked(int influenceType)
    {
        switch (influenceType)
        {
            case 1:
                Talk1.SetActive(true);
                opponentAI.anger += 2;
                opponentAI.think -= 1;
                opponentAI.escape -= 1;
                break;
            case 2:
                Talk2.SetActive(true);
                opponentAI.think += 1;
                opponentAI.escape += 1;
                opponentAI.anger -= 1;
                break;
            case 3:
                Talk3.SetActive(true);
                opponentAI.escape += 2;
                opponentAI.anger += 1;
                opponentAI.think -= 1;
                break;
            case 4:
                Talk4.SetActive(true);
                opponentAI.ResetStates(); 
                break;
        }

        InfluenceUI.SetActive(false);
        TacticUI.SetActive(true);
        playerAnimator.SetBool("Move", false);
        //playerAnimator.SetBool("Lose", false);
        currentStage = PlayerStage.TacticChoose;
        
    }

    public void OnTacticButtonClicked(int tacticType)
    {
        selectedTactic = tacticType;
        Vector3 playerPosition = Player.transform.position;

        Vector3 opponentPosition = Opponent.transform.position;
        initialPlayerPosition = playerPosition;
        Vector3 targetPosition = playerPosition; 
        float rotationY = (playerPosition.x < opponentPosition.x) ? 0 : -180;

        Talk1.SetActive(false);
        Talk2.SetActive(false);
        Talk3.SetActive(false);
        Talk4.SetActive(false);



        switch (tacticType)
        {
            case 1:
                playerAnimator.SetBool("R&D", false);
                //playerAnimator.SetBool("Idle", true);
                if (playerPosition.x == opponentPosition.x) 
                {
                    targetPosition = new Vector3(Mathf.Min(playerPosition.x + 3, 6), playerPosition.y, playerPosition.z);
                }
                else if (playerPosition.x < opponentPosition.x) 
                {
                    targetPosition = new Vector3(Mathf.Min(playerPosition.x + 3, 6), playerPosition.y, playerPosition.z);
                }
                else 
                {
                    targetPosition = new Vector3(Mathf.Max(playerPosition.x - 3, -6), playerPosition.y, playerPosition.z);
                }
                Player.transform.rotation = Quaternion.Euler(0, rotationY, 0);
                MoveRutine = StartCoroutine(MovePlayer(targetPosition, 0.5f)); 
                
                break;

            case 2: 
                if (playerPosition.x == opponentPosition.x) 
                {
                    targetPosition = new Vector3(Mathf.Max(playerPosition.x - 3, -6), playerPosition.y, playerPosition.z);
                }
                else if (playerPosition.x < opponentPosition.x) 
                {
                    targetPosition = new Vector3(Mathf.Max(playerPosition.x - 3, -6), playerPosition.y, playerPosition.z);
                }
                else 
                {
                    targetPosition = new Vector3(Mathf.Min(playerPosition.x + 3, 6), playerPosition.y, playerPosition.z);
                }
                Player.transform.rotation = Quaternion.Euler(0, rotationY, 0);

                MoveRutine = StartCoroutine(MovePlayer(targetPosition, 0.5f));
                break;

            case 3:
                playerAnimator.SetBool("R&D", true); 
                currentHP = Mathf.Min(currentHP + 1, MaxHp);
                PlayerHPText.text = currentHP.ToString();
                break;
        }

        currentStage = PlayerStage.Idle;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Opponent" )
        {
            //Debug.Log("Collide with opponent");
            switch (selectedTactic)
            {

                //Debug.Log("case 1 hit active");
                case 1:
                    OpcurrentHP = Mathf.Max(OpcurrentHP - 1, MinOpHp);
                    OpponentHPText.text = OpcurrentHP.ToString();
                    if (MoveRutine != null)
                    {
                        StopCoroutine(MoveRutine);
                        MoveRutine = null;
                    }
                    Player.transform.position = initialPlayerPosition; 
                    currentStage = PlayerStage.Idle;
                    break;

                case 3:
                    //Debug.Log("case 3 hit active");
                    currentHP = Mathf.Max(currentHP - 1, MinHp);
                    PlayerHPText.text = currentHP.ToString();
                    currentStage = PlayerStage.Idle;
                    break;
            }
        }
        else
        {
            switch (selectedTactic)
            {
                case 1:
                    //Debug.Log("case 1 goback active");
                    currentStage = PlayerStage.Idle;
                    break;
                case 3:
                    //Debug.Log("case 3 recover active");
                    //int currentHP = int.Parse(PlayerHPText.text);
                    currentStage = PlayerStage.Idle;
                    break;
            }
        }
    }

    IEnumerator MovePlayer(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = initialPlayerPosition; // 使用 initialPlayerPosition 作为起始位置
        float time = 0;

        playerAnimator.SetBool("Move", true);

        while (time < duration)
        {
            Player.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Player.transform.position = targetPosition;
        playerAnimator.SetBool("Move", false);

        MoveRutine = null;
    }


}
