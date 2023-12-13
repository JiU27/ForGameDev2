using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private int currentPrefabIndex = 0;

    public PrefabDisplay prefabDisplay;
    public MouseLine mouseLine;
    public RainSpawner rainSpawner;

    public float delayBeforeRain = 5f;
    public SpriteRenderer forbiddenArea; // ����ԭ�еĵ�����ֹ����
    public SpriteRenderer[] additionalForbiddenAreas; // �µĽ�ֹ��������
    public GameObject Sky;

    public void Start()
    {
        Sky.SetActive(false);
        if (prefabDisplay != null)
        {
            prefabDisplay.InitializeDisplay(prefabs);
        }
        rainSpawner.Land1.SetActive(false);
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ����Ƿ���ԭ�е�forbiddenArea��
        bool isMouseOverForbiddenArea = forbiddenArea.bounds.Contains(mousePosition);
        // �ı�ԭ��forbiddenArea��͸����
        Color areaColor = forbiddenArea.color;
        areaColor.a = isMouseOverForbiddenArea ? 0.2f : 0f;
        forbiddenArea.color = areaColor;

        // ����Ƿ����κ��µĽ�ֹ������
        bool isMouseOverAnyAdditionalForbiddenArea = IsMouseOverAnyAdditionalForbiddenArea(mousePosition);

        if (Input.GetMouseButtonDown(0) && !isMouseOverForbiddenArea && !isMouseOverAnyAdditionalForbiddenArea)
        {
            SpawnPrefab(mousePosition);
        }
    }

    private bool IsMouseOverAnyAdditionalForbiddenArea(Vector2 mousePosition)
    {
        foreach (var area in additionalForbiddenAreas)
        {
            if (area.bounds.Contains(mousePosition))
                return true;
        }
        return false;
    }


    private void SpawnPrefab(Vector2 position)
    {
        if (currentPrefabIndex < prefabs.Length)
        {
            Instantiate(prefabs[currentPrefabIndex], position, Quaternion.identity);
            prefabDisplay.RemoveDisplayItem(currentPrefabIndex);
            currentPrefabIndex++;
            if (currentPrefabIndex >= prefabs.Length)
            {
                if (mouseLine != null)
                {
                    mouseLine.StopDrawing();
                }

                StartCoroutine(DelayedStartRain());
            }
        }

        if (currentPrefabIndex >= prefabs.Length)
        {
            Sky.SetActive(true);
        }
    }

    private IEnumerator DelayedStartRain()
    {
        yield return new WaitForSeconds(delayBeforeRain);

        if (rainSpawner != null)
        {
            rainSpawner.enabled = true;
        }
    }

}
