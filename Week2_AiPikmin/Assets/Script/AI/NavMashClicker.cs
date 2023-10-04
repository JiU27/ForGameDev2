using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshClicker : MonoBehaviour
{
    public Camera camera;

    public enum States
    {
        Idle,
        Active
    }

    private NavMeshAgent activeAgent;
    private List<NavMeshAgent> allAgents = new List<NavMeshAgent>();

    void Start()
    {
        allAgents.AddRange(FindObjectsOfType<NavMeshAgent>());
        // ��ʼʱ��������agentsΪIdle״̬
        foreach (var agent in allAgents)
        {
            SetAgentState(agent, States.Idle);
        }
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
        // �����һ��֮ǰ�������Agent����������ΪIdle״̬
        if (activeAgent != null)
        {
            SetAgentState(activeAgent, States.Idle);
        }

        activeAgent = newActiveAgent;

        // ���µ�NavMeshAgent����ΪActive״̬
        if (activeAgent != null)
        {
            SetAgentState(activeAgent, States.Active);
        }
    }

    void SetAgentState(NavMeshAgent agent, States state)
    {
        switch (state)
        {
            case States.Idle:
                agent.isStopped = true;
                // ����������Idle״̬��ص����ԣ����������ɫ�ȣ�
                break;
            case States.Active:
                agent.isStopped = false;
                // ����������Active״̬��ص����ԣ����������ɫ�ȣ�
                break;
        }
    }
}
