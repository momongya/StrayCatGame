using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMoving : MonoBehaviour
{

    int catSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを動かす
        transform.position += transform.right * catSpeed * Time.deltaTime;
    }
}
