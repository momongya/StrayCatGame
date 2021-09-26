using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCreator : MonoBehaviour
{
    public GameObject leaf;
    public GameObject wood;

    List<GameObject> woodList = new List<GameObject>();

    Vector3 startPoint, endPoint, woodPoint;

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

        if (mousePos.y < 900)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //葉っぱを生成する
                Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
                //葉っぱ生成のスタート地点を記録する
                startPoint = Camera.main.ScreenToWorldPoint(mousePos);
            }

            if (Input.GetMouseButton(0))
            {
                //葉っぱを生成する
                Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
            }

            if (Input.GetMouseButtonUp(0))
            {
                //葉っぱ生成の終わりの地点を記録する
                endPoint = Camera.main.ScreenToWorldPoint(mousePos);

                //丸太の設置位置(葉っぱ生成の初めの地点と終わりの地点を繋いだところ)設定
                woodPoint = new Vector3((startPoint.x + endPoint.x) / 2, (startPoint.y + endPoint.y) / 2, 0);
                //丸太を生成
                GameObject scaffold = Instantiate(wood, woodPoint, Quaternion.Euler(0f, 0f, GetAngle(startPoint, endPoint)));
                //生成した丸太のサイズ変更(デフォルトサイズ(positionでいうと): x方向5)
                scaffold.transform.localScale = new Vector3(0.33f * Mathf.Abs(startPoint.x - endPoint.x) / 5, 0.25f * (Mathf.Abs(startPoint.x - endPoint.x) / 5) % 5, 0);

                //生成した丸太を一括消去用配列に追加
                woodList.Add(scaffold);
            }
        }
    }

    public void DeleteWoods()
    {
        foreach (var str in woodList)
        {
            Destroy(str);
        }
    }

    //角度を求めて返すメゾット
    float GetAngle(Vector3 start, Vector3 target)
    {
        Vector3 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
