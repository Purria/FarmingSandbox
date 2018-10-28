using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBase : MonoBehaviour
{

    // Use this for initialization
    public static FireBase Instance;
    public static DatabaseReference DBreference;
    public AllPlants allPlants;
    public AllHexes allHexes;
    string json;
    public string DatabaseChanges;
    [SerializeField] SinglePlant[] singlePlant;
    [SerializeField] SingleHex[] singleHex;
    [SerializeField] GameObject[] ActiveHexes;
    [SerializeField] InputField HowMuchHexes;
    int HexesValue;
    [SerializeField] Canvas Menu;
    [SerializeField] Text warrning;
    private void Awake()
    {
        Instance = this;
        if (Instance == null)
            Instance = this;
        else
            Instance = null;
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://farming-in-purria.firebaseio.com/");
    }

    private void Start()
    {

        FirebaseDatabase.DefaultInstance
        .GetReference("Plant")
        .ValueChanged += HandleValueChanged;
    }

    public void GetValueFromInput()
    {
        var r = HowMuchHexes.text;
        if (r.Equals("21")) { warrning.text = " only 20 hexes can be drawn"; }
        else
        {
            Drone.canMove = true;
            int.TryParse(r, out HexesValue);
            Debug.Log(HexesValue);
            Menu.enabled = false;
            for (int i = 0; i < HexesValue; i++)
            {
                ActiveHexes[i].SetActive(true);
            }
        }

    }

    private void HexValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            if (args.Snapshot.Value != null)
            {
                Debug.Log("Last added items were " + args.Snapshot.GetRawJsonValue().ToString());
                json = args.Snapshot.GetRawJsonValue();
                allHexes = JsonUtility.FromJson<AllHexes>(json);
                DatabaseChanges = json;
                PopulateHexData();
            }

            else
            {
                Debug.Log("There are no records in the DB yet!");
            }
        }
    }

    void PopulatePlantData()
    {
        for (int i = 0; i < singlePlant.Length; i++)
        {
            singlePlant[i].PlantID = allPlants.Plants[i].ID;
            singlePlant[i].value1 = allPlants.Plants[i].value1;
            singlePlant[i].value2 = allPlants.Plants[i].value2;
        }

    }

    void PopulateHexData()
    {
        for (int i = 0; i < singleHex.Length; i++)
        {
            singleHex[i].HexID = allPlants.Plants[i].ID;
            singleHex[i].value1 = allPlants.Plants[i].value1;
        }

    }


    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            if (args.Snapshot.Value != null)
            {
                Debug.Log("Last added items were " + args.Snapshot.GetRawJsonValue().ToString());
                json = args.Snapshot.GetRawJsonValue();
                allPlants = JsonUtility.FromJson<AllPlants>(json);
                DatabaseChanges = json;
                PopulatePlantData();
                PopulateHexData();
            }

            else
            {
                Debug.Log("There are no records in the DB yet!");
            }
        }
    }
}
