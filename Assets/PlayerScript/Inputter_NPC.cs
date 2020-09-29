using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-1000)]//入力受付は一番最初、他オブジェクトは入力を元に移動をする
public class Inputter_NPC : Inputter
{
    static GameObject navsParent;//このクラスが生成されるごとにNavMesh用オブジェクトが生成されるので、それをまとめるための親オブジェクト
    [SerializeField]
    Transform goalTarget;//最終的な目的地

    NavMeshAgent nav;//初期化時に生成したNavMeshAgentを保持する為のポインタ


    public Transform attackTarget;
    float attackCT = 0.5f;
    float attackTimer = 0.5f;

    float battleCT = 3f;
    public float battleWait = 1f;

    bool targetIsBox => attackTarget.gameObject.layer == LayerMask.NameToLayer("BreakObj");

    Sensor sensor;


//-----------------------------------------------------


    /// <summary>
    /// NavMeshに多少のランダムを持たせてゴールを設定する
    /// </summary>
    void SetGoal()
    {
        Vector3 goalPos = goalTarget.position;
        goalPos.x += Random.Range(-3f, 3f);
        nav.SetDestination(goalPos);
    }

    /// <summary>
    /// ゲームオブジェクトを生成し、それにNavMeshAgentをアタッチし、それに目的地を多少ランダムでずらして設定する
    /// </summary>
    void Start()
    {
        if (!navsParent) navsParent = new GameObject("NPCNavs");

        GameObject navObj = new GameObject("navObj"+ gameObject.name);
        navObj.transform.parent = navsParent.transform;
        
        nav = navObj.AddComponent<NavMeshAgent>();
        SetGoal();


        sensor = GetComponentInChildren<Sensor>();
    }

    /// <summary>
    /// 主な処理
    /// ゴールまでのパスが生成されている場合は、それに応じて移動用パラメーターを変動させる
    /// ゴールまでのパスが生成されていない場合はエラーなので、再度ゴールを設定する
    /// </summary>
    void Update()
    {
        if(attackTarget)
        {
            if(targetIsBox || battleWait <= 0f)
            {
                attackTimer -= Time.deltaTime * (targetIsBox ? 10f : 1f);

                if (attackTimer > 0f)//攻撃するために歩く
                {
                    touchStay = true;
                    touchExit = false;
                    Vector3 vel = (attackTarget.position - transform.position) * 100f;
                    touchVel = new Vector3(vel.x, vel.z, 0f);
                    nav.transform.position = transform.position;
                }
                else//攻撃する
                {
                    attackTarget = null;
                    attackTimer = attackCT;
                    touchStay = false;
                    touchExit = true;

                    battleWait = battleCT;
                }
                return;
            }
            else
            {
                battleWait -= Time.deltaTime;
            }
        }
        else
        {
            battleWait -= Time.deltaTime;
        }

        if(nav.path.corners.Length > 1)
        {
            touchStay = true;
            touchExit = false;
            Vector3 vel = (nav.path.corners[1] - transform.position) * 100f;
            touchVel = new Vector3(vel.x, vel.z, 0f);
            nav.transform.position = transform.position;
        }
        else
        {
            SetGoal();
            touchVel = Vector2.zero;
            if(touchStay)
            {
                touchStay = false;
                touchExit = true;
            }
        }
    }

    public override void PowerUp()
    {
        sensor.transform.localScale *= 3f;
        base.PowerUp();
    }
}
