using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class WoodCreator : MonoBehaviour
{
    public GameObject leaf;
    public GameObject wood;

    List<GameObject> leavesList = new List<GameObject>();
    List<GameObject> woodList = new List<GameObject>();

    Vector3 woodPoint;

    int indexOfLeaf;
    // 一番遠いもの
    double maxDistance;
    //一時的に保存する距離の情報
    double distance;
    GameObject centerLeaf;
    GameObject tmp1;
    GameObject tmp2;

    public Slider leafSlider;

    // Start is called before the first frame update
    void Start()
    {
        leafSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        // Z軸修正
        mousePos.z = 10f;

        if (Input.GetMouseButton(0))
        {
            if (-4 <= Camera.main.ScreenToWorldPoint(mousePos).y && Camera.main.ScreenToWorldPoint(mousePos).y <= 4)
            {
                if (leafSlider.value > 0)
                {
                    //葉っぱを生成する
                    GameObject Leaves = Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
                    leavesList.Add(Leaves);
                    leafSlider.value -= Time.deltaTime * 2;
                }
            }
        }else
        {
            leafSlider.value += 3 * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            distance = 0;
            maxDistance = 0;

            indexOfLeaf = leavesList.Count;

            // 描いた葉っぱの中から真ん中を探す
            centerLeaf = leavesList[(int)(indexOfLeaf/ 2 - 1)];

            //　真ん中の葉っぱから一番遠いものを見つける
            foreach (var str in leavesList)
            {
                // 真ん中の葉っぱからの距離の二乗を計算
                distance = Math.Pow(Math.Pow(centerLeaf.gameObject.transform.position.x - str.gameObject.transform.position.x, 2) + Math.Pow(centerLeaf.gameObject.transform.position.y - str.gameObject.transform.position.y, 2), 0.5);
                // 一番遠いものを一つ見つける
                if (maxDistance < distance)
                {
                    maxDistance = distance;
                    tmp1 = str;
                }
            }

            //見つけた一番遠い葉っぱから一番遠い葉っぱを発見する
            foreach (var str in leavesList)
            {
                // 真ん中の葉っぱからの距離の二乗を計算
                distance = Math.Pow(Math.Pow(tmp1.gameObject.transform.position.x - str.gameObject.transform.position.x, 2) + Math.Pow(tmp1.gameObject.transform.position.y - str.gameObject.transform.position.y, 2), 0.5);
                // 一番遠いものを一つ見つける
                if (maxDistance < distance)
                {
                    maxDistance = distance;
                    tmp2 = str;
                }
            }


            //y座標の小さい方を基準点にする
            if (tmp1.gameObject.transform.position.y > tmp2.gameObject.transform.position.y)
            {
                GameObject tmp = tmp1;
                tmp1 = tmp2;
                tmp2 = tmp;
            }

            //丸太の設置位置(葉っぱ生成の初めの地点と終わりの地点を繋いだところ)設定
            woodPoint = new Vector3((tmp1.gameObject.transform.position.x + tmp2.gameObject.transform.position.x) / 2, (tmp1.gameObject.transform.position.y + tmp2.gameObject.transform.position.y) / 2, 0);
            //丸太を生成
            GameObject scaffold = Instantiate(wood, woodPoint, Quaternion.Euler(0f, 0f, GetAngle(tmp1.gameObject.transform.position, tmp2.gameObject.transform.position)));
            //生成した丸太のサイズ変更(デフォルトサイズ(positionでいうと): x方向5)
            scaffold.transform.localScale = new Vector3(0.33f * Mathf.Abs(tmp1.gameObject.transform.position.x - tmp2.gameObject.transform.position.x) / 5, 0.25f * (Mathf.Abs(tmp1.gameObject.transform.position.x - tmp2.gameObject.transform.position.x) / 5) % 5, 0);

            //生成した丸太を一括消去用配列に追加
            woodList.Add(scaffold);
            // 葉っぱリストの中身を削除
            leavesList.Clear();
        }
    }

    public void DeleteWoods()
    {
        foreach (var s in woodList)
        {
            Destroy(s);
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
