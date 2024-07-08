using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerAI : Interactable
{
    public NavMeshAgent agent;
    public Transform cashierPoint;
    public Transform homePoint;
    public float waitDuration;
    private float waitTimer;
    public bool isWalking = false;
    public bool isBuying = false;
    private StateManager stateManager;
    [SerializeField] private DialogueBubble dialogueBubble;
    [SerializeField] private RectTransform boxHolder;
    [SerializeField] private Image patienceBar;
    public GameObject dialogueBubbleUI;
    private bool setupFlag = true;

    // Start is called before the first frame update
    private void Start()
    {
        waitTimer = waitDuration;
        
    }

    private void Update() {
        if (setupFlag)
        {
            if (ItemManager.instance.listGoodsOnSale.Count > 0)
            {
                Setup();
            } else {
                return;
            }
        }

        stateManager.currentState.UpdateState(this, stateManager);

        if (isWalking)
        {
            return;
        }

        waitTimer -= Time.deltaTime;
        patienceBar.fillAmount = waitTimer/waitDuration;
        if (waitTimer <= 0)
        {
            patienceBar.fillAmount = 1;
            waitTimer = waitDuration;
            isWalking = true;
        }
    }

    private Goods SetGoodsToBuy() {
        return ItemManager.instance.listGoodsOnSale[Random.Range(0, ItemManager.instance.listGoodsOnSale.Count-1)];
    }

    private int SetNumberOfGoods() {
        return Random.Range(1,3);
    }

    private void Setup() {
        stateManager = new StateManager();
        stateManager.StartState(this);
        int numberOfGoods = SetNumberOfGoods();
        for (int i = 0; i < numberOfGoods; i++)
        {
            DialogueBubble instantiatedDialogueBubble = Instantiate(dialogueBubble, boxHolder);
            instantiatedDialogueBubble.Setup(SetGoodsToBuy());
        }
        boxHolder.anchoredPosition = new Vector3(98, (114 * (numberOfGoods - 1)) -14, 0);
        setupFlag = false;
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (stateManager.currentState == stateManager.buy)
        {
            //logic pembelian
            Debug.Log("Interact with player");
        }
    }
}
