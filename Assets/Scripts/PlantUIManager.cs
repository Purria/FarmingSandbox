using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantUIManager : MonoBehaviour {

    [Header("PlantUI")]
    public Text PlantID, Height, LeavesQuantity, LeavesWidth, ColorofPlant, Weight, HeatofPlant, SoilCover;
    public static PlantUIManager _Instance;
 

    private void Awake()
    {
        _Instance = this;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void CurrentlyOpenedMenu(int index)
    {
        UIManager.Instance.CurrentMenuSection = "Plants";
        UIManager.Instance.CurrentSelectedUIMenu.text = "Current Section - " +  UIManager.Instance.CurrentMenuSection;
        UIManager.Instance.ManageUIMenus(index);
    }

    public void ShowGroupInfoForPlants()
    {
        // the calculation for the group statistics of plants will be implemented soon 
        UIManager.Instance.GroupPlantsInfo.text = "The Plants Health is 100%";
    }


    public void UpdateUIData(Plant single)
    {
        PlantID.text = "ID :" +single.PlantID.ToString();
        Height.text = "HEIGHT :" + single.Height.ToString();
        LeavesQuantity.text = "LEAVES QUANTITY :" + single.LeavesQuantity.ToString();
        LeavesWidth.text = "LEAVES WIDTH :" + single.LeavesWidth.ToString();
        ColorofPlant.text = "COLOR OF PLANT :" + single.ColorofPlant.ToString();
        Weight.text = "WEIGHT :" + single.Weight.ToString();
        HeatofPlant.text = "HEAT OF PLANT :" + single.HeatofPlant.ToString();
        SoilCover.text = "SOIL COVER :" + single.SoilCover.ToString();
    }
}
