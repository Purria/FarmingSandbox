using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlant : MonoBehaviour {


    public Plant singlePlant;
    public static SinglePlant _Instance;

    private void Awake()
    {
        _Instance = this;
    }


   // CHECK FOR THE PLANT STATUS, THERE WILL BE ENUMS THAT WILL CHACK THAT, TOO HIGH, TO LOW, NORMAL, OPTIMAL
    public void AssignDroneToPlant(Button plant)
    {
        plant.GetComponent<Button>().image.sprite = UIManager.Instance.AssignedDroneSprite;
    }


    public void ShowDataForSelectedPlantInUIManager()
    {
        PlantUIManager._Instance.UpdateUIData(singlePlant);    
    }

    // setting the drone to selected plant from the PlantUIManager called OnTap on Drone button above the plant button
    public void SetDroneForSelectedPlant(Button selectedDrone)
    {
        Drone._Instance.ActivateDrneOnPlant(singlePlant,selectedDrone);
    }
}
