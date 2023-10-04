using UnityEngine;
using UnityEngine.AI;

public class NewPikMinMoveTest : MonoBehaviour
{
    public enum States
    {
        Idle,
        Active,
        CarryingFollow
    }

    public NavMeshAgent agent;
    public States currentState = States.Idle;
    public string[] triggerTags = { "x", "y", "z" };
    public bool canActivateTrigger = true;
    public float followingMinDistance = 5.0f;

    private GameObject followedObject;

    void Update()
    {
        switch (currentState)
        {
            case States.Idle:
                if (Input.GetMouseButtonDown(0) && IsMouseOverObject())
                    currentState = States.Active;
                break;

            case States.Active:
                HandleStateChangeOnOtherAgentClick();
                if (Input.GetMouseButtonDown(0))
                {
                    if (IsMouseOverObject())
                        currentState = States.Idle;
                    else
                        MoveToClickPosition();
                }
                break;

            case States.CarryingFollow:
                if (followedObject == null || !followedObject.activeInHierarchy)
                {
                    currentState = States.Idle;
                    followedObject = null;
                }
                else
                {
                    if (Vector3.Distance(transform.position, followedObject.transform.position) > followingMinDistance)
                        agent.SetDestination(followedObject.transform.position);
                    else
                        agent.SetDestination(transform.position);

                    if (Input.GetMouseButtonDown(1) && IsMouseOverObject())
                    {
                        currentState = States.Idle;
                        followedObject = null;
                    }
                }
                break;
        }
    }

    private void HandleStateChangeOnOtherAgentClick()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && currentState == States.Active)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                NavMeshAgent hitAgent = hit.collider.GetComponent<NavMeshAgent>();
                if (hitAgent != null && hitAgent != agent)
                {
                    currentState = States.Idle;
                }
            }
        }
    }

    private bool IsMouseOverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject == this.gameObject;

        return false;
    }

    private void MoveToClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            agent.SetDestination(hit.point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canActivateTrigger && System.Array.IndexOf(triggerTags, other.tag) != -1)
        {
            followedObject = other.gameObject;
            currentState = States.CarryingFollow;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followingMinDistance);
    }
}
