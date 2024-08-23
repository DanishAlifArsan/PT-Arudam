using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PostmanAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform sprite;
    [SerializeField] private Animator anim;
    public Box box;
    public Transform deliverPoint;
    public Transform homePoint;
    public Transform packagePoint;
    public Transform playerTransform;
    private bool isDelivering;
    public int id;

    public void OnEnable() {
        agent.SetDestination(deliverPoint.position);
        isDelivering = true;
        anim.SetBool("deliver",true);
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
                agent.SetDestination(homePoint.position);    
                anim.SetBool("deliver",false);
                isDelivering = false;
                DeliveryManager.instance.PlaceDelivery(id, box);
            } else {
                DeliveryManager.instance.FinishDelivery(this);
            }
        }
    }
}
