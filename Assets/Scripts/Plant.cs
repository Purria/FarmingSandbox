using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plant
{
    public int PlantID;
    public float Height;
    public float LeavesQuantity;
    public float LeavesWidth;
    public float Weight;
    public float HeatofPlant;
    public float ColorofPlant;
    public float SoilCover;
}

[Serializable]
public class Hex
{
    public string UserId;
    public int HexID;
    public string value1;
}

[Serializable]
public class AllHexes
{
    public List<Hex> Hexes;
}

[Serializable]
public class AllPlants
{
    public List<Plant> Plants;
}

