using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]//入力受付は一番最初、他オブジェクトは入力を元に移動をする
abstract public class Inputter : MonoBehaviour
{
    public bool touchEnter;
    public bool touchExit;
    public bool touchStay;

    public Vector2 startTouchPos;
    public Vector2 currentTouchPos;
    public Vector2 touchVel;





    //-----------------------------------------------------



    /// <summary>
    /// パラメーターを全て0に初期化する
    /// </summary>
    public void Reset()
    {
        touchEnter = false;
        touchEnter = false;
        touchStay = false;
        startTouchPos = Vector2.zero;
        currentTouchPos = Vector2.zero;
        touchVel = Vector2.zero;
    }

    virtual public void PowerUp()
    {

    }
}
