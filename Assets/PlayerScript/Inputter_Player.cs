using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]//入力受付は一番最初、他オブジェクトは入力を元に移動をする
public class Inputter_Player : Inputter
{






    //-----------------------------------------------------



    /// <summary>
    /// 主な処理
    /// ユーザーの入力をキャラクター移動用パラメーターに変換する
    /// </summary>
    void Update()
    {
        touchEnter = Input.GetMouseButtonDown(0);
        touchExit = Input.GetMouseButtonUp(0);
        touchStay = Input.GetMouseButton(0);


        if (touchEnter)
        {
            startTouchPos = Input.mousePosition;
        }
        if (touchStay)
        {
            Vector2 currentTouchPos = Input.mousePosition;
            touchVel = currentTouchPos - startTouchPos;
        }
    }
}
