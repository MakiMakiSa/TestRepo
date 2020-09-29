using Exploder;
using Exploder.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hammer : MonoBehaviour
{
    Collider col;   //攻撃判定用コライダ

    PlayerManager parentPlayer;






    //-----------------------------------------------------



    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        col = GetComponent<Collider>();
        parentPlayer = GetComponentInParent<PlayerManager>();
    }

    /// <summary>
    /// 攻撃開始フレーム、MoveManagerから呼ばれる
    /// </summary>
    public void Attack_Enter()
    {
        col.enabled = true;

    }
    /// <summary>
    /// 攻撃終了フレーム、MoveManagerから呼ばれる
    /// </summary>
    public void Attack_Exit()
    {
        col.enabled = false;
    }

    /// <summary>
    /// 自分のコライダが何かに接触した場合、そのオブジェクトにI_Damageableインターフェースが実装されていた場合、そのダメージ処理を呼ぶ
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        I_Damageable damage = other.gameObject.GetComponent<I_Damageable>();
        if (damage != null)
        {
            damage.Damage(parentPlayer);
        }
    }


}
