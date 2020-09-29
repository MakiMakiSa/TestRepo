using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    Inputter_NPC inputter;
    // Start is called before the first frame update
    void Start()
    {
        inputter = GetComponentInParent<Inputter_NPC>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(inputter.attackTarget == null)
        {
            if(inputter.battleWait <= 0f)
            {
                inputter.attackTarget = other.transform;
            }
        }
    }
}
