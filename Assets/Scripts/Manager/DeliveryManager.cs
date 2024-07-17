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
            PostmanAI instantiatedPostman = Instantiate(postman,homePoint.position, Quaternion.identity);
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
        if (!isCanSpawn)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && CanDeliver() && CanCheckout())
        {
            postmanList[0].gameObject.SetActive(true);
            postmanList[0].box = boxQueue.Dequeue();
            postmanList.RemoveAt(0);
            spawnTimer = spawnInterval;
            isCanSpawn = CanDeliver() && CanCheckout();
        }
    }

    public void StartDelivery(Box box) {
        isCanSpawn = true;
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

    public void FinishDelivery(PostmanAI postman) {
        postman.gameObject.SetActive(false);
        postmanList.Add(postman);
    }
}
