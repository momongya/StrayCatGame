using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class CatMoving : MonoBehaviour
{

    float catSpeed;
    public GameObject goal;
    public GameObject time;

    public GameObject[] catFood;
    public GameObject foodCreator;
    public GameObject came;

    //変数を作る
    Rigidbody2D rb;

    Vector3 angles;

    // Start is called before the first frame update
    void Start()
    {
       // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //猫の移動する速さはランダムに設定する
        catSpeed = Random.Range(1.5f, 3.0f);

        if (FoodChecker() == -1)
        {
            //プレイヤーを動かす
            transform.position += Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;
        } else
        {
            ////プレイヤーを動かす
            //transform.LookAt(catFood[FoodChecker()].transform);
            //transform.position += transform.right * catSpeed * Time.deltaTime;
        }

        //自分で作った重力
        Vector2 myGravity = new Vector2(0, -9.81f);

        //Rigidbody2Dに重力を加える
        rb.AddForce(myGravity);


        if (time.GetComponent<TimerController>().TimeManager() <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (transform.position.x >= goal.gameObject.transform.position.x)
        {
            SceneManager.LoadScene("Goal");
        }

        came.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //もし丸太の足場にあたったら
        if (collision.gameObject.CompareTag("wood"))
        {
            //rb.velocity = new Vector3(0, 3, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //猫の角度は接する床面に依存する(z軸だけ)
        transform.rotation = Quaternion.AngleAxis(collision.gameObject.transform.localEulerAngles.z, new Vector3(0,0,1));

        //もし丸太の足場にあたったなら、丸太の角度に移動する
        if (collision.gameObject.CompareTag("wood"))
        {
            angles = new Vector3(0, 0, collision.gameObject.transform.localEulerAngles.z);

            //自分で作った重力
            Vector2 myGravity = new Vector2(0, -9.81f);

            //Rigidbody2Dに重力を加える
            rb.AddForce(myGravity);
        }
    }

    // 猫の餌が近くにあるかどうか判別し、猫の餌が近くにあっても籠がかかっていれば近寄らない
    //FoodCheck <=　正の数 猫は近寄る FoodCheck <= -1 猫は近寄らない
    int FoodChecker()
    {
        int j = -1;
        for (int i = 0; i < catFood.Length; i++)
        {
            //bool status = ItemController.itemController.GetFoodStatus(i);
            ////猫缶に籠がかかっていなければ
            //if (status == false)
            //{
                // 猫缶が近くにあれば
                if (transform.position.y == catFood[i].gameObject.transform.position.y && (catFood[i].gameObject.transform.position.x - transform.position.x) < 10)
            　　 {
                    j = i;
                }
            //}
        }
        return j;
    }
}
