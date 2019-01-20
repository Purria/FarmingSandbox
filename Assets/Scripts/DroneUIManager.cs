using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUIManager : MonoBehaviour {


  	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CurrentlyOpenedMenu(int index)
    {
        UIManager.Instance.CurrentMenuSection = "Drones";
        UIManager.Instance.CurrentSelectedUIMenu.text = "Current Section - " + UIManager.Instance.CurrentMenuSection;
        UIManager.Instance.ManageUIMenus(index);

    }


    // METHOD FOR BUYING DRONES FROM THE SHOP IN THE DRONE UI MANAGER
}
