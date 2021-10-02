using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarasuMoving : MonoBehaviour
{
    Transform myTransform;
    Vector3 worldAngle;

    // Start is called before the first frame update
    void Start()
    {
        // transformを取得
        myTransform = this.transform;

        // ワールド座標を基準に、回転を取得
        worldAngle = myTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
            transform.position -= transform.right * 5 * Time.deltaTime;
   
        if (-10 >= transform.position.x)
        {
            worldAngle.y = 180.0f; // ワールド座標を基準に、y軸を軸にした回転を180度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }
        if (transform.position.x >= 130)
        {
            worldAngle.y = 0.0f; // ワールド座標を基準に、y軸を軸にした回転を180度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }
    }
}
