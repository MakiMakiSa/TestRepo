using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour,I_Damageable
{

    Inputter inputter;  //このキャラクターへのインプットクラス、_Playerがコントローラー入力、_NPCがNavMeshを使用したＡＩ入力
    Animator animator;  //アニメーション管理用

    //アニメーションパラメーター
    int anm_Speed = Animator.StringToHash("Speed");//移動アニメーション速度、この値が0.1を上回ると移動アニメーションへ遷移し、その速度がこの値で決まる
    int anm_AttackTrigger = Animator.StringToHash("AttackTrigger");//攻撃トリガー
    int anm_Damage = Animator.StringToHash("DamageTrigger");//ダメージトリガー

    Hammer hammer;//ハンマーオブジェクトにアタッチされたハンマー管理用クラス

    float damageCT = 2.5f;//ダメージを受けてから次のダメージを受け付けるまでのクールタイム
    float damageTimer = 0f;//ダメージを受けてから次のダメージを受ける為のタイマー
    public bool isDamageable => damageTimer > 0f;

    float attackCT = 1f;
    float attackTimer = 1f;

    [SerializeField]
    ParticleSystem powerUpEffect;





//-----------------------------------------------------


    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        inputter = GetComponent<Inputter>();
        hammer = GetComponentInChildren<Hammer>();
        
    }

    /// <summary>
    /// 主な処理
    /// Inputterから入力を受け、その値に応じてアニメーションを遷移させる
    /// ダメージタイマーを動かす
    /// </summary>
    void Update()
    {
        if(damageTimer <= damageCT*0.2f && attackTimer <= 0f)
        {
            if (inputter.touchStay)
            {
                float walkSpeed = inputter.touchVel.magnitude * 0.01f;

                if (walkSpeed > 0.1f)
                {
                    animator.SetFloat(anm_Speed, Mathf.Clamp(walkSpeed, 0.5f, 1f));
                    transform.LookAt(transform.position + new Vector3(inputter.touchVel.x, 0f, inputter.touchVel.y));
                }
            }

            if (inputter.touchExit)
            {
                animator.SetTrigger(anm_AttackTrigger);
                animator.SetFloat(anm_Speed, 0f);
                attackTimer = attackCT;
            }
        }


        damageTimer -= Time.deltaTime;
        damageTimer = Mathf.Clamp(damageTimer, 0f, damageCT);
        attackTimer -= Time.deltaTime;
        attackTimer = Mathf.Clamp(attackTimer, 0f, attackCT);
    }

    /// <summary>
    /// 攻撃開始フレーム、アニメーションイベントから呼ばれる
    /// </summary>
    void Attack_Enter()
    {
        hammer.Attack_Enter();
    }


    /// <summary>
    /// 攻撃終了フレーム、アニメーションイベントから呼ばれる
    /// </summary>
    void Attack_Exit()
    {
        hammer.Attack_Exit();
    }

    /// <summary>
    /// インターフェースにより実装したダメージ処理、ハンマークラスから呼ばれる
    /// </summary>
    public void Damage(PlayerManager player)
    {
        if(damageTimer <= 0f && !player.isDamageable)
        {
            animator.SetTrigger(anm_Damage);
            damageTimer = damageCT;

            EffectManager.Create(0, transform.position, 3f);

        }
    }

    public void PowerUp()
    {
        hammer.transform.localScale *= 3f;
        powerUpEffect.gameObject.SetActive(true);

        inputter.PowerUp();

    }
}
