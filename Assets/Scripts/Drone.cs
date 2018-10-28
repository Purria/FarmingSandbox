using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour {

    [SerializeField] Text currentlyWorkingPlant;
    int TempPlantID;
    bool hasStarted;
   public static bool canMove;
    string tempValue1 = "New Testing Values  10/15/18";
    string tempValue2 = "New Testing Values 10/15/18";
    float timer;
    GameObject ParentHolder;


    private void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Hex");
        if(temp != null)
        {
            AddCollidersToHex(temp);
            Debug.Log("adding collider to " + temp);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "plant")
        {

            hasStarted = true;
            TempPlantID = other.GetComponent<SinglePlant>().PlantID;
            ParentHolder = other.transform.parent.gameObject;
        }
        else { currentlyWorkingPlant.text = "The drone has completed the work"; }

        //this will be changed in the future
        if(TempPlantID == 0 )
        {
            canMove = false;
            Debug.Log(" the drone is currently under the plant with ID " + TempPlantID); // update the exact plant id in the firebase database
            PopulatePlantData("Plants", TempPlantID);
            PopulateHexData("Hexses", TempPlantID);
        }

        if (TempPlantID == 1)
        {
            canMove = false;
            StopAllCoroutines();
            Debug.Log(" the drone is currently under the plant with ID " + TempPlantID); // update the exact plant id in the firebase database
            PopulatePlantData("Plants", TempPlantID);
            PopulateHexData("Hexses", TempPlantID);
        }


    }
    void AddCollidersToHex(GameObject[] Hex)
    {
        foreach(var item in Hex)
        {
            item.AddComponent<BoxCollider>().isTrigger = true;
        }
    }

    public void PopulatePlantData(string referenceName, int referenceChildName)
    {
        var RootRef = FireBase.DBreference = FirebaseDatabase.DefaultInstance.GetReference("Plant");
        Plant p = new Plant();
        p.ID = TempPlantID;
        p.value1 = tempValue1;
        p.value2 = tempValue2;
        string res = JsonUtility.ToJson(p);
        RootRef.Child(referenceName).Child(referenceChildName.ToString()).SetRawJsonValueAsync(res);
        ParentHolder.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void PopulateHexData(string referenceName, int referenceChildName)
    {
        var HexRootRef = FireBase.DBreference = FirebaseDatabase.DefaultInstance.GetReference("Hex");
        Hex hex = new Hex();
        hex.ID = TempPlantID;
        hex.value1 = "testing value for hex with plant ID " + hex.ID;
        Debug.Log(hex.ID);
        string res = JsonUtility.ToJson(hex);
        HexRootRef.Child(referenceName).Child(referenceChildName.ToString()).SetRawJsonValueAsync(res);
    }

    private void Update()
    {
        MoveTheDrone();
        if(hasStarted)
        {
            StartCoroutine(Delayed());
            currentlyWorkingPlant.text = "The drone is currently working on a plant with ID " + TempPlantID;
        }
    }


    void MoveTheDrone()
    {

       // timer++;
        if(timer >= 100) { canMove = true; timer = 0; }
        if(canMove)
        transform.position += Vector3.right * 1 * Time.deltaTime;

    }

    IEnumerator Delayed()
    {
        yield return new WaitForSeconds(2f);
        canMove = true;


    }   
}
