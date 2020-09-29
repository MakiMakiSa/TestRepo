using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Damageable
{
    /// <summary>
    /// ダメージを受ける為の処理
    /// </summary>
    /// <param name="player">ダメージを与えたプレイヤー</param>
    void Damage(PlayerManager player);
}
