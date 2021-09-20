using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject cage;
    public GameObject[] catFood;

    bool[] foodStatus;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        foodStatus = new bool[catFood.Length];
        for (i = 0; i < catFood.Length; i++)
        {
            //falseのとき籠なし
            foodStatus[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        // Z軸修正
        mousePos.z = 10f;

        //右クリックされたとき
        if (Input.GetMouseButtonDown(1))
        {
            //猫缶から場所がそう離れていなければ
            for (i = 0; i < catFood.Length; i++)
            {
                if ((catFood[i].gameObject.transform.position.x - mousePos.x) < 10)
                {
                    //籠を生成する
                    Instantiate(cage, new Vector3(catFood[i].gameObject.transform.position.x, catFood[i].gameObject.transform.position.y + 0.38f, 10), Quaternion.identity);
                    foodStatus[i] = true;
                }
            }
        }
    }

    //プロパティー
    public bool GetFoodStatus(int i)
    {
        return foodStatus[i];
    }
}