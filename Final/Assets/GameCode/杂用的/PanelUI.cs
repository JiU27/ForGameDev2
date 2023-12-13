using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    public GameObject pullDown;
    public GameObject pullUp;
    public GameObject gameUI;
    public GameObject pause;

    public float moveDistance = 3.82f;
    public float moveDuration = 1f;

    private bool pullDownClicked = false;
    private bool pullUpClicked = false;
    private bool continueClicked = false;

    void Start()
    {
        pullUp.SetActive(false);
        pause.SetActive(false);
    }

    void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverGameObject(pullDown))
            {
                pullDownClicked = true;
            }
            else if (IsMouseOverGameObject(pullUp))
            {
                pullUpClicked = true;
            }
        }

        // 根据点击处理
        if (pullDownClicked)
        {
            HandlePullDownClick();
        }
        if (pullUpClicked)
        {
            HandlePullUpClick();
        }
    }

    private void HandlePullDownClick()
    {
        // 在激活前调整 pause 的位置
        Vector3 pausePos = pause.transform.position;
        pause.transform.position = new Vector3(pausePos.x, pausePos.y + 10, pausePos.z);

        StartCoroutine(MoveGameUI(Vector3.down * moveDistance));
        pullDown.SetActive(false);
        pullUp.SetActive(true);
        pause.SetActive(true);

        pullDownClicked = false;
    }

    private void HandlePullUpClick()
    {
        // 在停用前调整 pause 的位置
        Vector3 pausePos = pause.transform.position;
        pause.transform.position = new Vector3(pausePos.x, pausePos.y - 10, pausePos.z);

        StartCoroutine(MoveGameUI(Vector3.up * moveDistance));
        pullDown.SetActive(true);
        pullUp.SetActive(false);
        pause.SetActive(false);

        pullUpClicked = false;
    }

    private IEnumerator MoveGameUI(Vector3 moveVector)
    {
        float elapsedTime = 0;
        Vector3 startingPos = gameUI.transform.position;
        Vector3 endPos = startingPos + moveVector;

        while (elapsedTime < moveDuration)
        {
            gameUI.transform.position = Vector3.Lerp(startingPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameUI.transform.position = endPos;
    }

    private bool IsMouseOverGameObject(GameObject gameObject)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
}
