using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleHex : MonoBehaviour {



    [SerializeField] Text PlantInformations;
    [SerializeField] Canvas InformationMenu;
    public int HexID;
    public string value1;

   
    private void OnMouseOver()
    {
        InformationMenu.enabled = true;
        PlantInformations.color = Color.white;
        PlantInformations.text = string.Format("{0} {1}", "You are currently seeing informations about Hex with with ID " , HexID);
    }

    private void OnMouseExit()
    {
        InformationMenu.enabled = false;

    }
}
