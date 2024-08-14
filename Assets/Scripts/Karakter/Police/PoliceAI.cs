using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    [SerializeField] private Transform policePoint;
    private bool isChasing = false;
    public bool isWalking = false;

    public void StartChasing() {
        agent.SetDestination(policePoint.position);
        isChasing = true;
        Walk(false);
    }

    public void Walk(bool toHome) {
        anim.SetBool("walking", true);
        anim.SetBool("toHome", toHome);
    }

    private void Update() {

        if (isChasing) {
            float dist = agent.remainingDistance;
            if (dist!=Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
            {
                anim.SetBool("walking", false);
                ScrollingText.instance.Show("Terima kasih atas kerjasamanya");
                isChasing = false;
            }
        }

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
