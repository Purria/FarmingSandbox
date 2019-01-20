using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDrone
{

    //THIS CLASS WILL BE INPLEMENTED SOON ON EACH DRONE
    [Serializable]
    public class Drone
    {
        public string DroneID;
        public string DronePlantIDAssignment;
        public string DroneInfo;
        public DateTime DroneStartedWorkingTime;
        public DateTime DroneStoppedWorkingTime;
    }

}
