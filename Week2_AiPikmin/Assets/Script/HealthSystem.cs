using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class HealthSystem : MonoBehaviour
{
    public int hp = 10;
    public float detectionRadius = 5f; // SphereCast radius
    public string[] carryTags = { "x", "y", "z" };
    public TextMeshPro hpText;
    public NavMeshAgent agent;
    public int maxHp = 20;  // 新添加的生命值上限
    private enum States
    {
        Idle,
        Active
    }
    private States currentState = States.Idle;
    private HashSet<GameObject> objectsInRange = new HashSet<GameObject>();

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.isStopped = true;
        UpdateHpText();
    }

    void Update()
    {
        HandleState();
        HandleObjectsInRange();
    }



    void HandleState()
    {
        switch (currentState)
        {
            case States.Idle:
                if (hp > 0)
                {
                    agent.isStopped = true;
                }
                if (hp <= 0 && Input.GetMouseButtonDown(0) && IsMouseOverObject())
                {
                    currentState = States.Active;
                }
                break;

            case States.Active:
                agent.isStopped = false;
                if (Input.GetMouseButtonDown(0))
                {
                    if (IsMouseOverObject())
                    {
                        currentState = States.Idle;
                    }
                    else
                    {
                        // Check if clicked on another NavMeshAgent
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            NavMeshAgent hitAgent = hit.collider.GetComponent<NavMeshAgent>();
                            if (hitAgent != null && hitAgent != agent)
                            {
                                currentState = States.Idle;
                            }
                            else
                            {
                                MoveToClickPosition();
                            }
                        }
                    }
                }
                else if (hp > 0)
                {
                    currentState = States.Idle;
                }
                break;
        }
    }

    void HandleObjectsInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        HashSet<GameObject> newObjectsInRange = new HashSet<GameObject>();

        foreach (var hitCollider in hitColliders)
        {
            if (System.Array.IndexOf(carryTags, hitCollider.tag) != -1)
            {
                newObjectsInRange.Add(hitCollider.gameObject);
                if (!objectsInRange.Contains(hitCollider.gameObject))
                {
                    hp -= 1;
                    UpdateHpText();
                }
            }
        }

        foreach (var oldObject in objectsInRange)
        {
            if (!newObjectsInRange.Contains(oldObject))
            {
                hp += 1;
                UpdateHpText();
            }
        }

        objectsInRange = newObjectsInRange;
    }

    void UpdateHpText()
    {
        hp = Mathf.Clamp(hp, 0, maxHp); // 确保hp值在0和maxHp之间
        hpText.text = hp.ToString();
    }

    private bool IsMouseOverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == this.gameObject;
        }
        return false;
    }

    private void MoveToClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
