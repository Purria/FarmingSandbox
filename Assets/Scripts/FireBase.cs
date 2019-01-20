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
    int HexesValue;
    [SerializeField] Canvas Menu;
    [SerializeField] Text warrning;
    int PlantIDIncrementer;
    int HexIdIncrementer;
    public static string CurrentUserId;

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
        CurrentUserId = PlayerPrefs.GetString("user");
        FirebaseDatabase.DefaultInstance
        //.GetReference("Plant")
       .GetReference("Plant" + " for user " + CurrentUserId)
       .ValueChanged += HandleValueChanged;

    }

    public void GetValueFromInput()
    {
        var r = UIManager.Instance.HowMuchHexes.text;
        if (r.Equals("21")) { warrning.text = " only 20 hexes can be drawn"; }
        else
        {
            int.TryParse(r, out HexesValue);
            Debug.Log(HexesValue);
            UIManager.Instance.HowMuchPlantsArePlented = HexesValue;
            Menu.enabled = false;
            for (int i = 0; i < HexesValue; i++)
            {
                UIManager.Instance.ActiveHexes[i].SetActive(true);
                UIManager.Instance.ShowHowMuchPlantsAreActiveInUIManager();
            }
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
            }

            else
            {
                Debug.Log("There are no records in the DB yet!");
            }
        }
    }
}
