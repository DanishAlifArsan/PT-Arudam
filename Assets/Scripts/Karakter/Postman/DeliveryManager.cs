using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
    public List<Box>  deliveredItem = new List<Box>();

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
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            SpawnItemFromSave(data.deliveredItem);
        } else {
            SpawnNewItem();
        }

        spawnTimer = spawnInterval;

        for (int i = 0; i < deliveryPoint.Count; i++)
        {
            PostmanAI instantiatedPostman = Instantiate(postman,homePoint.position, Quaternion.identity, transform.parent);
            instantiatedPostman.id = i;
            instantiatedPostman.homePoint = homePoint;
            instantiatedPostman.deliverPoint = deliveryPoint[i];
            instantiatedPostman.packagePoint = packagePoint[i];
            instantiatedPostman.playerTransform = playerTransform;
            instantiatedPostman.gameObject.SetActive(false);
            postmanList.Add(instantiatedPostman);   
        }
    }

    public void SpawnItemFromSave(List<Box> deliveredItem) {
        this.deliveredItem = deliveredItem;
        for (int i = 0; i < this.deliveredItem.Count; i++)
        {
            Box item = this.deliveredItem[i];
            if (item != null)
            {
                Instantiate(item,packagePoint[i]);
            }
        }
    }

    public void SpawnNewItem() {
        for (int i = 0; i < packagePoint.Count; i++)
        {
            deliveredItem.Add(null);
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
    public void PlaceDelivery(int id, Box box) {
        deliveredItem[id] = box;
        Instantiate(box,packagePoint[id]);
    }

    public void TakeDelivery(Transform transform) {
        if (packagePoint.Contains(transform))
        {
            deliveredItem[packagePoint.IndexOf(transform)] = null;
        }
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
    }
}
