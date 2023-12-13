using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player2 : MonoBehaviour
{
    public GameObject player2;
    public Player2Stage currentStageP2;
    public GameObject InfluenceUIP2;
    public GameObject TacticUIP2;
    public GameObject Talk1, Talk2, Talk3, Talk4, Talk5, Talk6, Talk7, Talk8, Talk9, Talk10;
    public Player1 player1control;
    public GameObject Opponent, Player2Ani;
    public Animator player2Animator;

    public bool ReturnKeyPressed = false;
    public bool ULRKeyPressed = false;

    private int selectedTactic;
    private Vector3 initialPlayerPosition;

    Coroutine MoveRutine;

    public enum Player2Stage
    {
        Idle,
        Influence,
        TacticChoose
    }

    void Start()
    {
        currentStageP2 = Player2Stage.Idle;
        InfluenceUIP2.SetActive(false);
        TacticUIP2.SetActive(false);
        player2Animator.SetBool("P2Move", false);
        player2Animator.SetBool("P2R&B", true);
        player2Animator.SetBool("P2Lose", false);
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

        player2Animator = Player2Ani.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (MoveRutine == null)
        {
            if (currentStageP2 == Player2Stage.TacticChoose)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ULRKeyPressed = true;
                    Debug.Log("ULRPressed");
                }
            }

            switch (currentStageP2)
            {
                case Player2Stage.Idle:
                    InfluenceUIP2.SetActive(true);
                    TacticUIP2.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        ReturnKeyPressed = true;
                        Debug.Log("ReturnPressed");

                        if (player1control.spaceKeyPressed)
                        {
                            currentStageP2 = Player2Stage.TacticChoose;
                            ReturnKeyPressed = false;
                            player1control.spaceKeyPressed = false;
                            player1control.currentStage = Player1.PlayerStage.TacticChoose;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        ActivateRandomTalk();
                    }
                    break;
                case Player2Stage.TacticChoose:
                    //Debug.Log("StageChanged P2");
                    InfluenceUIP2.SetActive(false);
                    TacticUIP2.SetActive(true);
                    HandleTacticInput();
                    break;
            }
        }
    }


    public void OnTacticButtonClicked(int tacticType)
    {

        selectedTactic = tacticType;
        Vector3 playerPosition = player2.transform.position;

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
                        player2Animator.SetBool("P2R&B", false);
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
                        player2.transform.rotation = Quaternion.Euler(0, rotationY, 0);
                        MoveRutine = StartCoroutine(MovePlayer(targetPosition, 0.5f));

                        break;

                    case 2:
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
                        player2.transform.rotation = Quaternion.Euler(0, rotationY, 0);

                        MoveRutine = StartCoroutine(MovePlayer(targetPosition, 0.5f));
                        break;

                    case 3:
                        player2Animator.SetBool("P2R&B", true);
                        player1control.Player2HP = Mathf.Min(player1control.Player2HP + 1, player1control.Max2HP);
                        player1control.Player2HPText.text = player1control.Player2HP.ToString();
                        break;
                }

                if (player1control.WADKeyPressed)
                {
                    currentStageP2 = Player2Stage.Idle;
                    player1control.spaceKeyPressed = false;
                    player1control.WADKeyPressed = false;
                    ReturnKeyPressed = false;
                    ULRKeyPressed = false;
                    player1control.currentStage = Player1.PlayerStage.Idle;
                }
            
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Debug.Log("Collide with P1");
            switch (selectedTactic)
            {

                //Debug.Log("case 1 hit active");
                case 1:
                    player1control.Player1HP = Mathf.Max(player1control.Player1HP - 1, player1control.Min1HP);
                    player1control.Player1HPText.text = player1control.Player1HP.ToString();
                    if (MoveRutine != null)
                    {
                        StopCoroutine(MoveRutine);
                        MoveRutine = null;
                    }
                    player2.transform.position = initialPlayerPosition;
                    currentStageP2 = Player2Stage.Idle;
                    break;

                case 3:
                    //Debug.Log("case 3 hit active");
                    player1control.Player2HP = Mathf.Max(player1control.Player2HP - 1, player1control.Min2HP);
                    player1control.Player2HPText.text = player1control.Player2HP.ToString();
                    currentStageP2 = Player2Stage.Idle;
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
                    currentStageP2 = Player2Stage.Idle;
                    break;
            }
        }
    }

    IEnumerator MovePlayer(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = initialPlayerPosition;
        float time = 0;

        player2Animator.SetBool("P2Move", true);

        while (time < duration)
        {
            player2.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        player2.transform.position = targetPosition;
        player2Animator.SetBool("P2Move", false);

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnTacticButtonClicked(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnTacticButtonClicked(2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnTacticButtonClicked(3);
        }
    }
}
