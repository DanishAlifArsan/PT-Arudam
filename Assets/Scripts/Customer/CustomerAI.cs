using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public bool isWalking;
    public bool isBuying;
    public bool isPaying;
    public int maxNumberOfGoods;
    public int buyAmountPerGoods;
    public bool isEvil;
    private StateManager stateManager;
    [SerializeField] private DialogueBubble dialogueBubble;
    [SerializeField] private RectTransform boxHolder;
    [SerializeField] private Image patienceBar;
    private List<DialogueBubble> dialogueBubbles = new List<DialogueBubble>();
    public Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
    public GameObject dialogueBubbleUI;
    private bool setupFlag;

    private void OnEnable() {
        waitTimer = waitDuration;
        isWalking = false;
        isBuying = false;
        isPaying = false;
        setupFlag = true;
        CustomerManager.instance.isSpawned = true;
    }

    private void OnDisable() {
        stateManager = null;
        CustomerManager.instance.isSpawned = false;
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

    private void Setup() {
        stateManager = new StateManager();
        stateManager.StartState(this);
        setupFlag = false;
    }

    public void SetGoodsToBuy() {
        goodsToBuy = CustomerManager.instance.SetGoodsToBuy(maxNumberOfGoods, buyAmountPerGoods);

        int numberOfGoods = goodsToBuy.Count;
        for (int i = 0; i < numberOfGoods; i++)
        {
            dialogueBubbles.Add(Instantiate(dialogueBubble, boxHolder));
            dialogueBubbles[i].Setup(goodsToBuy.ElementAt(i));
        }
        boxHolder.anchoredPosition = new Vector3(98, (114 * (numberOfGoods - 1)) -14, 0);
    }

    public void ClearGoodsToBuy() {
        foreach (var item in dialogueBubbles)
        {
            Destroy(item.gameObject);
        }
        dialogueBubbles.Clear();
    }

    public int CountTotalPrice() {
        int totalPrice = 0;
        for (int i = 0; i < goodsToBuy.Count; i++)
        {
            int price = goodsToBuy.ElementAt(i).Key.sellPrice;
            int amount = goodsToBuy[goodsToBuy.ElementAt(i).Key];
            totalPrice += price * amount;
        }
    
        return totalPrice;
    }
    
    public override void OnInteract(ItemInteract broadcaster)
    {
        if (stateManager.currentState == stateManager.buy)
        {            
            //logic pembelian
            Item item = broadcaster.itemInHand?.GetComponent<Item>();
            if (item!= null && SaleManager.instance.IsGridNull())
            {
                broadcaster.itemInHand = null;
                SaleManager.instance.PlaceItem(goodsToBuy, item);
                patienceBar.fillAmount = 1;
                waitTimer = waitDuration;
                if (SaleManager.instance.CompareItem())
                {
                    isPaying = true;
                }
            }
        }
    }
}
