using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform cashierPoint;
    public Transform homePoint;
    public float waitDuration;
    private float waitTimer;
    public bool isWalking = false;
    public bool isBuying = false;
    private StateManager stateManager;

    // Start is called before the first frame update
    private void Start()
    {
        waitTimer = waitDuration;
        stateManager = new StateManager();
        stateManager.StartState(this);
        // StartCoroutine(MoveAI());
    }

    private void Update() {
        stateManager.currentState.UpdateState(this, stateManager);

        if (isWalking)
        {
            return;
        }

        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = waitDuration;
            isWalking = true;
        }
    }

    // private IEnumerator MoveAI() {
    //     agent.SetDestination(cashierPoint.position);
    //     yield return new WaitUntil(() => IsArrived(cashierPoint.position));
    //     yield return new WaitForSeconds(waitDuration);
    //     StartCoroutine(BackToHome());
    // }  

    // private IEnumerator BackToHome() {
    //     agent.SetDestination(homePoint.position);
    //     yield return new WaitUntil(() => IsArrived(homePoint.position));
    //     yield return new WaitForSeconds(waitDuration);
    // } 

    // private bool IsArrived(Vector3 pos) {
    //     return agent.transform.position.x - pos.x < .01f;
    // }
}
