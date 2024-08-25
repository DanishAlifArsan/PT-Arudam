using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.Playables;

public class Display : Interactable
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private DisplayList displayList;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private float priceRate;
    [SerializeField] private GameObject displayUI;
    private bool isInteract = false;
    private ItemInteract broadcaster;
    private List<Goods> listGoods = new List<Goods>();

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CloseDisplay();
            }
        }
    }

    private int OnButtonClick(int index, int price, bool isPlus) {
        int buyPrice = listGoods[index].buyPrice;
        int minPrice = (int) (buyPrice - (buyPrice * priceRate));
        int maxPrice = (int) (buyPrice + (buyPrice * priceRate));

        if(isPlus){
            int tempPrice = Mathf.Clamp(listGoods[index].sellPrice += 500, minPrice, maxPrice);
            ItemManager.instance.goodsWithPrice[ItemManager.instance.goodsWithPrice.ElementAt(index).Key] = tempPrice;
            return tempPrice;
        } else {
            int tempPrice = Mathf.Clamp(listGoods[index].sellPrice -= 500, minPrice, maxPrice);
           ItemManager.instance.goodsWithPrice[ItemManager.instance.goodsWithPrice.ElementAt(index).Key] = tempPrice;
            return tempPrice;
        }
    }

    public void GenerateList(Item item) {
        if (!listGoods.Contains(item.goods))
        {
            DisplayList instantiatedDisplayList =  Instantiate(displayList, canvas);
            instantiatedDisplayList.Setup(item.goods, listGoods.Count);
            instantiatedDisplayList.OnButtonClick += OnButtonClick;
            listGoods.Add(item.goods);
            ItemManager.instance.goodsWithPrice.Add(item.goods, item.goods.sellPrice);
        }
    }

    public void GenerateList(Goods goods, int price) {
        DisplayList instantiatedDisplayList =  Instantiate(displayList, canvas);
        goods.sellPrice = price;
        instantiatedDisplayList.Setup(goods, listGoods.Count);
        instantiatedDisplayList.OnButtonClick += OnButtonClick;
        listGoods.Add(goods);
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        base.OnInteract(broadcaster);
        displayUI.SetActive(true);
        this.broadcaster = broadcaster;
        string indicator = LocalizationManager.Localize("Menu Back");
        broadcaster.SetIndicator(true,indicator);
        director.Play();
        ToggleHighlight(broadcaster.centerIndicator, false, "Interact Display");
        broadcaster.canvas.SetActive(false);
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInteract = true;
    }

    private void CloseDisplay() {
        broadcaster.SetIndicator(false);
        displayUI.SetActive(false);
        director.Stop();
        ToggleHighlight(broadcaster.centerIndicator, true, "Interact Display");
        broadcaster.canvas.SetActive(true);
        broadcaster.controller.enabled = true;
        broadcaster.canInteract = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInteract = false;
    }

    public List<Goods> GetGoodsOnSale() {
        return listGoods;
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        ToggleHighlight(broadcaster.centerIndicator, status, "Interact Display");
    }
}
