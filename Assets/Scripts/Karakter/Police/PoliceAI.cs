using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    [SerializeField] private Transform policePoint;
    [SerializeField] private Transform policeHome;
    public bool isWalking = false;

    public void MoveToShop() {
        isWalking = true;
        Walk(false);
        agent.SetDestination(policePoint.position);
    }

    public void BackToHome() {
        agent.SetDestination(policeHome.position);
        Walk(true);
        isWalking = true;
    }

    private void Walk(bool toHome) {
        anim.SetBool("walking", true);
        anim.SetBool("toHome", toHome);
    }

    private void Update() {
        if (isWalking)
        {
            float dist = agent.remainingDistance;
            if (dist!=Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
            {
                anim.SetBool("walking", false);
                isWalking = false;
            }
        }
    }
}
