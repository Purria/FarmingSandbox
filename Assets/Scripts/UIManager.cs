using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Application UI'S")]
    public Canvas HexesMenu, RegisterMenu, LoginMenu;
    public Text Log, Version;
    public static UIManager Instance;
    public InputField RegistrationEmail, RegistrationPassword, LoginEmail, LoginPassword;
    public Color Success, Fail;
    public string PurriaVersion;
    public Canvas[] UIMenus;
    public  string CurrentMenuSection;
    public Text CurrentSelectedUIMenu;
    public SinglePlant[] singlePlant;
    public GameObject[] ActiveHexes;
    public InputField HowMuchHexes;
    public int HowMuchPlantsArePlented;
    public Sprite AssignedDroneSprite, UnAssignedDroneSprite;
    public Text GroupPlantsInfo;
    int IDIncrementer;

    void Start()
    {
        Instance = this;
        Version.text = PurriaVersion;
        AssignGenericIDToEachPlant();
        
    }

    //DEACTIVATE THE DRONES ALSO IF A PLANT IS DEACTIVATED USER CANT PLACE DRONE THERE
    public void ShowHowMuchPlantsAreActiveInUIManager()
    {
        for (int i = 0; i < singlePlant.Length; i++)
        {
            singlePlant[i].GetComponent<Button>().interactable = false ;
        }
        if(HowMuchPlantsArePlented != 0)
        {
            for (int i = 0; i < HowMuchPlantsArePlented; i++)
            {
                singlePlant[i].GetComponent<Button>().interactable = true;

            }
        }
    }

    void AssignGenericIDToEachPlant()
    {
        foreach (var item in singlePlant)
        {
            IDIncrementer++;
            item.singlePlant.PlantID = IDIncrementer;
        }
    }

    public void ManageUIMenus(int menuIndex)
    {

        for (int i = 0; i < UIMenus.Length; i++)
        {
            UIMenus[i].enabled = false;
            UIMenus[menuIndex].enabled = true;
            UIMenus[UIMenus.Length - 1].enabled = true;
        }
    }

    public void BackToField()
    {
        for (int i = 0; i < UIMenus.Length; i++)
        {
            UIMenus[i].enabled = false;
        }
    }

}
