using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Display : Interactable
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private DisplayList displayList;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private float priceRate;
    private bool isInteract = false;
    private ItemInteract broadcaster;
    private List<Goods> listGoods = new List<Goods>();

    private void Update() {
        if (isInteract)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseDisplay();
            }
        }
    }

    private int OnButtonClick(int index, int price) {
        int buyPrice = listGoods[index].buyPrice;
        int minPrice = (int) (buyPrice - (buyPrice * priceRate));
        int maxPrice = (int) (buyPrice + (buyPrice * priceRate));

        if (price <= maxPrice && price >= minPrice)
        {
            listGoods[index].sellPrice = price;
            return price;
        } else {
            listGoods[index].sellPrice = buyPrice;
            return buyPrice;
        }
    }

    public void GenerateList(Item item) {
        if (!listGoods.Contains(item.goods))
        {
            DisplayList instantiatedDisplayList =  Instantiate(displayList, canvas);
            instantiatedDisplayList.Setup(item.goods, listGoods.Count);
            instantiatedDisplayList.OnButtonClick += OnButtonClick;
            listGoods.Add(item.goods);
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        this.broadcaster = broadcaster;
        director.Play();
        ToggleHighlight(false);
        EnableHighlight(false);
        broadcaster.canvas.SetActive(false);
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInteract = true;
    }

    private void CloseDisplay() {
        director.Stop();
        EnableHighlight(true);
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
}
