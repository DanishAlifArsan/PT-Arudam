using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform cashierPoint;
    [SerializeField] private Transform homePoint;
    [SerializeField] private float waitDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAI());
    }

    private IEnumerator MoveAI() {
        agent.SetDestination(cashierPoint.position);
        yield return new WaitUntil(() => IsArrived(cashierPoint.position));
        yield return new WaitForSeconds(waitDuration);
        StartCoroutine(BackToHome());
    }  

    private IEnumerator BackToHome() {
        agent.SetDestination(homePoint.position);
        yield return new WaitUntil(() => IsArrived(homePoint.position));
        yield return new WaitForSeconds(waitDuration);
    } 

    private bool IsArrived(Vector3 pos) {
        return agent.transform.position.x - pos.x < .01f;
    }
}
