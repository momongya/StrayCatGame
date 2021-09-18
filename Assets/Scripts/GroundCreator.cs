using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    public GameObject leaf;
    public GameObject wood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        // Z軸修正
        mousePos.z = 10f;
        if (Input.GetMouseButtonDown(0))
        {
            //葉っぱを生成する
            Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
            //葉っぱ生成のスタート地点を記録する
            Vector3 startPoint = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButton(0))
        {
            //葉っぱを生成する
            Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //葉っぱを生成する
            Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
            //葉っぱ生成の終わりの地点を記録する
            Vector3 endPoint = Camera.main.ScreenToWorldPoint(mousePos);

            //葉っぱ生成の初めの地点と終わりの地点を繋いだところに丸太を設置

        }
    }
}
