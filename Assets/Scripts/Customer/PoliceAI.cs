using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform policePoint;
    private bool isChasing = false;

    public void StartChasing() {
        agent.SetDestination(policePoint.position);
        isChasing = true;
    }

    private void Update() {

        if (isChasing) {
            float dist = agent.remainingDistance;
            if (dist!=Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
            {
                ScrollingText.instance.Show("Terima kasih atas kerjasamanya");
                isChasing = false;
            }
        }
    }
}
