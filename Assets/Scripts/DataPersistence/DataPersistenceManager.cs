using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Data Persistence Manager detected in scene");
        }

        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        Debug.Log("No save detected. New save file created");
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(ref gameData);
        }

        // TODO - save that data to a file using the data handler
        dataHandler.Save(gameData);

        Debug.Log("Saved night: " + gameData.currentNight);
    }

    public void LoadGame()
    {
        // TODO - Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // If no data is loaded, create a new default save
        if (this.gameData == null)
        {
            Debug.Log("No save data found. Initializing a default save");
            NewGame();
        }

        // TODO - push the loaded data to all other scripts that need it
        foreach(IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }

        Debug.Log("Loaded night: " + gameData.currentNight);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
