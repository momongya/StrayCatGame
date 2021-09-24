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
    Transform myTransform;
    Vector3 worldAngle;

    private ItemController itemController;

    private void Awake()
    {
        itemController = foodCreator.gameObject.GetComponent<ItemController>();
    }

    // Start is called before the first frame update
    void Start()
    {
       // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // transformを取得
        myTransform = this.transform;

        // ワールド座標を基準に、回転を取得
        worldAngle = myTransform.eulerAngles;

        //猫の移動する速さ設定
        catSpeed = 2.0f;

        int food = FoodChecker();
        if (food == -1)
        {
            //プレイヤーを動かす
            transform.position += Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;
        }
        else if (food >= 0)
        {
            // 一定の秒数餌の元にいたら猫缶は消え、前を向く
            StartCoroutine(DestroyCatFood(food));
        }

        //画面遷移
        if (time.GetComponent<TimerController>().TimeManager() <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (transform.position.x >= goal.gameObject.transform.position.x)
        {
            SceneManager.LoadScene("Goal");
        }

        //カメラの移動を設定
        came.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        //猫が前にあまり進んでいない時の設定
        if (CheckMoving() > 2.0)
        {
            //猫の位置
            myTransform.position = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        catSpeed = 3.5f;

        //猫の角度は接する床面に依存する(z軸だけ)
        transform.rotation = Quaternion.AngleAxis(collision.gameObject.transform.localEulerAngles.z, new Vector3(0, 0, 1));
    }

    // 猫の餌が近くにあるかどうか判別し、猫の餌が近くにあっても籠がかかっていれば近寄らない
    //FoodCheck <=　正の数 猫は近寄る FoodCheck <= -1 猫は近寄らない
    int FoodChecker()
    {
        int j = -1;
        for (int i = 0; i < catFood.Length; i++)
        {
            bool status = itemController.GetFoodStatus(i);
            //猫缶に籠がかかっていなくて存在していれば
            if (status == false)
            {
                // 猫缶が近くにあれば
                if (transform.position.y <= -3 && (catFood[i].gameObject.transform.position.x - transform.position.x) < 5)
            　　 {
                    j = i;
                }
            }
        }
        return j;
    }

    // 一定の秒数餌の元にいたら猫缶は消え、猫は前に進み出す
    IEnumerator DestroyCatFood(int i)
    {
        if (transform.position.x > catFood[i].gameObject.transform.position.x)
        {
            worldAngle.y = 180.0f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }

        if (1.4 <= Mathf.Abs(transform.position.x - catFood[i].gameObject.transform.position.x))
        {
            transform.position += Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;
        }

        // 3秒停止
        yield return new WaitForSeconds(3);

        // 猫缶消去
        itemController.DeleteCatFood(i);

        // 存在の抹消
        itemController.SetFoodStatus(i, true);

        worldAngle.y = 0.0f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
        myTransform.eulerAngles = worldAngle; // 回転角度を設定

    }

    //猫が1秒間にどれだけの距離(x軸方向)進んでいるのか調べる(基本的にはupdate関数内で使う想定)
    //猫がほとんどその場で止まっているよう(つまり壁や障害物にぶつかっている時)
    float CheckMoving()
    {
        float time = 0;
        float maxX = -1;
        float minX = 0;

        time += Time.deltaTime;
        while(time >= 1.0f)
        {
            if (maxX > transform.position.x)
            {
                maxX = transform.position.x;
            }
            if (minX < transform.position.x)
            {
                minX = transform.position.x;
            }
        }

        return maxX - minX;
    }
}
