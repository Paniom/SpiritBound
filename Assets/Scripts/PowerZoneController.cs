using UnityEngine;
using System.Collections;

public class PowerZoneController : MonoBehaviour 
{
    public enum PowerZoneType { FoxPower, WolfPower };
    public PowerZoneType thisPowerType;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && thisPowerType != null)
        {
            other.SendMessage("PowerZone", thisPowerType.ToString(), SendMessageOptions.DontRequireReceiver);

        }
    }
}
