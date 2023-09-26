using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMashClicker : MonoBehaviour
{
    public Camera camera;
    

    private NavMeshAgent activeAgent;
    private List<NavMeshAgent> allAgents = new List<NavMeshAgent>();

    private void Start()
    {
        allAgents.AddRange(FindObjectsOfType<NavMeshAgent>());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseRay = camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 100))
            {
                NavMeshAgent clickedAgent = hitInfo.transform.GetComponent<NavMeshAgent>();

                if (clickedAgent != null)
                {
                    SetActiveAgent(clickedAgent);
                }
                else if (activeAgent != null)
                {
                    activeAgent.SetDestination(hitInfo.point);
                }
            }
        }
    }

    void SetActiveAgent(NavMeshAgent newActiveAgent)
    {
        // �����һ��֮ǰ�������Agent���ͽ�����ʻ�ԭΪĬ�ϲ���
        if (activeAgent != null)
        {
            
        }

        activeAgent = newActiveAgent;

        // ���µ�NavMeshAgent�Ĳ�������Ϊ��������
        if (activeAgent != null)
        {
            
        }
    }
}
