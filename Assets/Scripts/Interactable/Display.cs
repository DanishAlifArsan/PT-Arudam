using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Display : Interactable
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private DisplayList displayList;
    [SerializeField] private RectTransform canvas;
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

    private void OnButtonClick(int index, int price) {
        listGoods[index].buyPrice = price;
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
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInteract = true;
    }

    private void CloseDisplay() {
        director.Stop();
        EnableHighlight(true);
        broadcaster.controller.enabled = true;
        broadcaster.canInteract = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInteract = false;
    }
}
