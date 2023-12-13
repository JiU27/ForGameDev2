using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Player2;

public class Player1 : MonoBehaviour
{
    public GameObject player1;
    public PlayerStage currentStage;
    public GameObject InfluenceUIP1;
    public GameObject TacticUIP1;
    public GameObject Talk1, Talk2, Talk3, Talk4, Talk5, Talk6, Talk7, Talk8, Talk9, Talk10;
    public TextMeshProUGUI Player1HPText, Player2HPText;
    public int Player2HP = 5;
    public int Player1HP = 5;
    public int Max2HP, Min2HP, Max1HP, Min1HP;

    public GameObject Opponent;
    public Animator playerAnimator;
    public Player2 player2control;

    public bool spaceKeyPressed = false;
    public bool WADKeyPressed = false;


    private int selectedTactic;
    private Vector3 initialPlayerPosition;

    Coroutine MoveRutine;

    public enum PlayerStage
    {
        Idle,
        TacticChoose
    }

    void Start()
    {
        Max2HP = Player2HP;
        Max1HP = Player1HP;
        Min1HP = 0;
        Min2HP = 0;
        currentStage = PlayerStage.Idle;
        InfluenceUIP1.SetActive(false);
        TacticUIP1.SetActive(false);
        playerAnimator.SetBool("P1Move", false);
        playerAnimator.SetBool("P1R&B", true);
        playerAnimator.SetBool("P1Lose", false);
        Talk1.SetActive(false);
        Talk2.SetActive(false);
        Talk3.SetActive(false);
        Talk4.SetActive(false);
        Talk5.SetActive(false);
        Talk6.SetActive(false);
        Talk7.SetActive(false);
        Talk8.SetActive(false);
        Talk9.SetActive(false);
        Talk10.SetActive(false);


        Player1HPText.text = Player1HP.ToString();
        Player2HPText.text = Player2HP.ToString();

        playerAnimator = playerAnimator.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (MoveRutine == null) // 确保不在移动过程中
        {
            if (currentStage == PlayerStage.TacticChoose)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    WADKeyPressed = true;
                    Debug.Log("WADPressed");
                }
            }

            switch (currentStage)
            {
                case PlayerStage.Idle:
                    InfluenceUIP1.SetActive(true);
                    TacticUIP1.SetActive(false);
                    WADKeyPressed = false;
                    playerAnimator.SetBool("P1R&B", true);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        spaceKeyPressed = true;
                        if (player2control.ReturnKeyPressed)
                        {
                            currentStage = PlayerStage.TacticChoose;
                            spaceKeyPressed = false;
                            player2control.ReturnKeyPressed = false;
                            player2control.currentStageP2 = Player2.Player2Stage.TacticChoose;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                    {
                        // 随机激活一个 Talk 对象
                        ActivateRandomTalk();
                    }
                    break;
                case PlayerStage.TacticChoose:
                    //Debug.Log("StageChanged P1");
                    InfluenceUIP1.SetActive(false);
                    TacticUIP1.SetActive(true);
                    HandleTacticInput(); // 处理 Tactic 输入
                    break;
            }
        }
    }


    public void OnTacticButtonClicked(int tacticType)
    {
        selectedTactic = tacticType;
        Vector3 playerPosition = player1.transform.position;

        Vector3 opponentPosition = Opponent.transform.position;
        initialPlayerPosition = playerPosition;
        Vector3 targetPosition = playerPosition;
        float rotationY = (playerPosition.x < opponentPosition.x) ? 0 : -180;

        Talk1.SetActive(false);
        Talk2.SetActive(false);
        Talk3.SetActive(false);
        Talk4.SetActive(false);
        Talk5.SetActive(false);
        Talk6.SetActive(false);
        Talk7.SetActive(false);
        Talk8.SetActive(false);
        Talk9.SetActive(false);
        Talk10.SetActive(false);

        
                switch (tacticType)
                {
                    case 1:
                        playerAnimator.SetBool("P1R&B", false);
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
                        player1.transform.rotation = Quaternion.Euler(0, rotationY, 0);
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
                        player1.transform.rotation = Quaternion.Euler(0, rotationY, 0);

                        MoveRutine = StartCoroutine(MovePlayer(targetPosition, 0.5f));
                        break;

                    case 3:
                        playerAnimator.SetBool("P1R&B", true);
                        Player1HP = Mathf.Min(Player1HP + 1, Max1HP);
                        Player1HPText.text = Player1HP.ToString();
                        break;
                }
            


            if (player2control.ULRKeyPressed)
            {
                currentStage = PlayerStage.Idle;
                player2control.ULRKeyPressed = false;
                WADKeyPressed = false;
                player2control.ReturnKeyPressed = false;
                spaceKeyPressed = false;
                player2control.currentStageP2 = Player2.Player2Stage.Idle;
            }
        
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player2")
        {
            Debug.Log("Collide with P2");
            switch (selectedTactic)
            {

                //Debug.Log("case 1 hit active");
                case 1:
                    Player2HP = Mathf.Max(Player2HP - 1, Min2HP);
                    Player2HPText.text = Player2HP.ToString();
                    if (MoveRutine != null)
                    {
                        StopCoroutine(MoveRutine);
                        MoveRutine = null;
                    }
                    player1.transform.position = initialPlayerPosition;
                    currentStage = PlayerStage.Idle;
                    break;

                case 3:
                    //Debug.Log("case 3 hit active");
                    Player1HP = Mathf.Max(Player1HP - 1, Min1HP);
                    Player1HPText.text = Player1HP.ToString();
                    currentStage = PlayerStage.Idle;
                    break;
            }
        }
        else
        {
            switch (selectedTactic)
            {
                case 1:
                    break;
                case 3:
                    currentStage = PlayerStage.Idle;
                    break;
            }
        }
    }

    IEnumerator MovePlayer(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = initialPlayerPosition; 
        float time = 0;

        playerAnimator.SetBool("P1Move", true);

        while (time < duration)
        {
            player1.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        player1.transform.position = targetPosition;
        playerAnimator.SetBool("P1Move", false);

        MoveRutine = null;
    }

    void ActivateRandomTalk()
    {
        Talk1.SetActive(false);
        Talk2.SetActive(false);
        Talk3.SetActive(false);
        Talk4.SetActive(false);
        Talk5.SetActive(false);
        Talk6.SetActive(false);
        Talk7.SetActive(false);
        Talk8.SetActive(false);
        Talk9.SetActive(false);
        Talk10.SetActive(false);

        // 随机选择并激活一个 Talk 对象
        int randomTalk = Random.Range(1, 11); // 生成 1 到 10 之间的随机数
        switch (randomTalk)
        {
            case 1:
                Talk1.SetActive(true);
                break;
            case 2:
                Talk2.SetActive(true);
                break;
           case 3:
                Talk3.SetActive(true);
                break;
                case 4:
                Talk4.SetActive(true);
                break;
                case 5:
                Talk5.SetActive(true);
                break;
                case 6:
                Talk6.SetActive(true);
                break;
                case 7:
                Talk7.SetActive(true);
                break;
                case 8:
                Talk8.SetActive(true);
                break;
                case 9:
                Talk9.SetActive(true);
                break;
            case 10:
                Talk10.SetActive(true);
                break;
        }
    }

    void HandleTacticInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnTacticButtonClicked(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnTacticButtonClicked(2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnTacticButtonClicked(3);
        }
    }

}
