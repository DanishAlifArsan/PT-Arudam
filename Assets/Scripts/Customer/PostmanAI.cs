using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PostmanAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform sprite;
    public Box box;
    public Transform deliverPoint;
    public Transform homePoint;
    public Transform packagePoint;
    public Transform playerTransform;
    private bool isDelivering;

    public void OnEnable() {
        agent.SetDestination(deliverPoint.position);
        isDelivering = true;
    }

    // Update is called once per frame
    void Update()
    {
        sprite.LookAt(playerTransform);

        float dist = agent.remainingDistance;
        if (dist!=Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            if (isDelivering)
            {
                Instantiate(box,packagePoint);
                agent.SetDestination(homePoint.position);
                isDelivering = false;
            } else {
                DeliveryManager.instance.FinishDelivery(this);
            }
        }
    }
}
