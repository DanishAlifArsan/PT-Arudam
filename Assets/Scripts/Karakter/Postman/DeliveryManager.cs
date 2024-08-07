using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private PostmanAI postman;
    [SerializeField] private Transform homePoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Transform> deliveryPoint;
    [SerializeField] private List<Transform> packagePoint;
    [SerializeField] private float spawnInterval;
    private float spawnTimer = 0;
    public bool isCanSpawn = false;
    private List<PostmanAI> postmanList = new List<PostmanAI>();
    private Queue<Box> boxQueue = new Queue<Box>();

    public static DeliveryManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        spawnTimer = spawnInterval;

        for (int i = 0; i < deliveryPoint.Count; i++)
        {
            PostmanAI instantiatedPostman = Instantiate(postman,homePoint.position, Quaternion.identity, transform.parent);
            instantiatedPostman.homePoint = homePoint;
            instantiatedPostman.deliverPoint = deliveryPoint[i];
            instantiatedPostman.packagePoint = packagePoint[i];
            instantiatedPostman.playerTransform = playerTransform;
            instantiatedPostman.gameObject.SetActive(false);
            postmanList.Add(instantiatedPostman);   
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!CheckAvailability())
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && CanDeliver())
        {
            for (int i = 0; i < postmanList.Count; i++)
            {
                if (postmanList[i].packagePoint.childCount == 0)
                {
                    postmanList[i].gameObject.SetActive(true);
                    postmanList[i].box = boxQueue.Dequeue();
                    postmanList.RemoveAt(i);
                    spawnTimer = spawnInterval;
                    break;
                }
            }
        }
    }

    public void StartDelivery(Box box) {
        boxQueue.Enqueue(box);
        
    }

    public bool CanCheckout() {
        return packagePoint.Any(p =>
            p.childCount == 0
        );
    }

    public bool CanDeliver() {
        return postmanList.Count > 0 && boxQueue.Count > 0;
    }

    private bool CheckAvailability() {
        return postmanList.Any(p =>
            p.deliverPoint.childCount == 0
        );
    }

    public void FinishDelivery(PostmanAI postman) {
        postmanList.Add(postman);
        postman.gameObject.SetActive(false);
        foreach (var item in boxQueue)
        {
            Debug.Log(item);
        }
    }
}
