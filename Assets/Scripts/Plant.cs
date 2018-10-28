using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plant
{
    public int ID;
    public string value1;
    public string value2;
}

[Serializable]
public class Hex
{
    public int ID;
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
