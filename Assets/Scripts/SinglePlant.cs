using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlant : MonoBehaviour {

    [SerializeField] Text PlantInformations;
    [SerializeField] Canvas InformationMenu;
    public int PlantID;
    public string value1;
    public string value2;


    private void OnMouseOver()
    {
        InformationMenu.enabled = true;
        PlantInformations.color = Color.white;
        PlantInformations.text = string.Format("{0} {1} {2}", "You are currently seeing informations about Plant with ID " + PlantID, value1, value2);

    }

    private void OnMouseExit()
    {
        InformationMenu.enabled = false;

    }
}
