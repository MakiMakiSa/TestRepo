using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager main;

    public ParticleSystem[] effects;



    // Start is called before the first frame update
    void Start()
    {
        if(!main)
        {
            main = this;
        }

    }


    static public void Create(int effectNum , Vector3 position , float destroyTime = 0f)
    {
        Debug.Log("DDD");
        if (effectNum >= main.effects.Length) return;
        Debug.Log("DDDOK");

        Transform eff = Instantiate(main.effects[effectNum]).transform;
        eff.position = position;
        if(destroyTime != 0f)
        {
            Destroy(eff.gameObject, destroyTime);
        }
    }
}
