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
        // 初始时设置所有agents为Idle状态
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
        // 如果有一个之前被激活的Agent，将其设置为Idle状态
        if (activeAgent != null)
        {
            SetAgentState(activeAgent, States.Idle);
        }

        activeAgent = newActiveAgent;

        // 将新的NavMeshAgent设置为Active状态
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
                // 设置其他与Idle状态相关的属性（比如材质颜色等）
                break;
            case States.Active:
                agent.isStopped = false;
                // 设置其他与Active状态相关的属性（比如材质颜色等）
                break;
        }
    }
}
