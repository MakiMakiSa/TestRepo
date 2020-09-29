using Exploder;
using Exploder.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Box : MonoBehaviour,I_Damageable
{
    enum Type
    {
        Default,
        Item,
        Goal
    }
    [SerializeField]
    Type powerUpFlag;

    ExploderOption exp;


    public void Damage(PlayerManager player)
    {
        if (exp != null)
        {
            ExploderSingleton.ExploderInstance.ExplodeObject(gameObject);

            switch(powerUpFlag)
            {
                case Type.Default:
                    {
                        EffectManager.Create(0, transform.position, 3f);
                    }
                    break;
                case Type.Item:
                    {
                        EffectManager.Create(1, transform.position, 3f);
                        player.PowerUp();
                    }
                    break;
                case Type.Goal:
                    {
                        EffectManager.Create(2, transform.position+(Vector3.up*3f));
                    }
                    break;
                default:
                    {

                    }break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        exp = GetComponent<ExploderOption>();

    }

}
