using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public int totalCurrency;
    public int day;
    public int numberOfEvils = 0;
    public SerializableDictionary<Transform, Box> deliveredItem = new SerializableDictionary<Transform, Box>();
    public SerializableDictionary<Transform, Item> storageItem = new SerializableDictionary<Transform, Item>();
    // private GameData gameData;
    // private List<ISaveData> saveDataObjects;
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void NewGame() {
        SaveSystem.Delete();
    }

    public void SaveGame() {
        SaveSystem.Save(this);
    }

    public GameData LoadGame() {
        GameData data = SaveSystem.Load();
        if (data != null)
        {
            totalCurrency = data.totalCurrency;
            day = data.day;
            numberOfEvils = data.numberOfEvils;
            deliveredItem = data.deliveredItem;
            storageItem = data.storageItem;
        }   

        return data;
    }

    // private void Start() {
    //     saveDataObjects = FindAllSaveData();
    //     SetupGame();
    // }
    
    // public void NewGame() {
    //     gameData = new GameData();
    //     foreach (ISaveData obj in saveDataObjects)
    //     {
    //         obj.NewGame(ref gameData);
    //     }

    //     SaveSystem.Save(gameData);
    // }

    // public void SaveGame() {
    //     foreach (ISaveData obj in saveDataObjects)
    //     {
    //         obj.SaveGame(ref gameData);
    //     }
        
    //     SaveSystem.Save(gameData);
    // }

    // public void SetupGame() {
    //     gameData = SaveSystem.Load();

    //     if (gameData == null)
    //     {
    //         NewGame();
    //     }
    //     LoadGame();
    // }

    // public void LoadGame() {
    //     foreach (ISaveData obj in saveDataObjects)
    //     {
    //         obj.LoadGame(gameData);
    //     }
    // }

    // private List<ISaveData> FindAllSaveData() {
    //     IEnumerable<ISaveData> saveDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();

    //     return new List<ISaveData>(saveDataObjects);
    // }
}
