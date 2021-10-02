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
    public GameObject sky;

    //変数を作る
    Rigidbody2D rb;

    Vector3 angles;
    Transform myTransform;
    Vector3 worldAngle;

    float maxX = -1;
    float minX = 10000;
    float sumMove = 100;

    float timeleft;
    float timing;

    bool startCatDown = false;

    //猫の進んでいる位置で止まっているか否かの判別
    //falseなら進んでいる
    bool stopCat = false;

    private ItemController itemController;

    private void Awake()
    {
        itemController = foodCreator.gameObject.GetComponent<ItemController>();

        timeleft = 1.0f;
        timing = 0f;
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

        float checkMoving = CheckMoving();

        if (0 < checkMoving && checkMoving <= 0.5)
        {
            stopCat = true;
        }
     
        //if (stopCat == true && startCatDown == false)
        //{
        //    timing += Time.deltaTime;

        //    //0.5秒間下がる
        //    if (timing < 0.5)
        //    {
        //        transform.position -= Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;
        //    }

        //    //ねこは少し下がって3秒待機
        //    StartCoroutine(WaitCatMoving());

        //    //1秒間は確実に進ませる
        //    if (timing > 1.0)
        //    {
        //        startCatDown = false;
        //        timing = 0f;
        //    }
        //    else
        //    {
        //        timing += Time.deltaTime;
        //    }
        //}
        //else
        if (food == -1)
        {
            //プレイヤーを動かす
            transform.position += Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;
        }
        else if (food >= 0)
        {
            // 一定の秒数餌の元にいたら猫缶は消え、前を向く
            StartCoroutine(DestroyCatFood(food));
            timeleft = 1.0f;
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

        //空の移動を設定
        sky.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        catSpeed = 3.5f;

        //猫の角度は接する床面に依存する(z軸だけ)
        if (50 > collision.gameObject.transform.localEulerAngles.z && collision.gameObject.transform.localEulerAngles.z >= -60)
        {
            transform.rotation = Quaternion.AngleAxis(collision.gameObject.transform.localEulerAngles.z, new Vector3(0, 0, 1));
        }
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
        if (i == 0)
        {
            worldAngle.y = 180.0f; // ワールド座標を基準に、y軸を軸にした回転を180度に変更
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

        worldAngle.y = 0.0f; // ワールド座標を基準に、y軸を軸にした回転を0度に変更
        myTransform.eulerAngles = worldAngle; // 回転角度を設定

    }

    //猫が1秒間にどれだけの距離(x軸方向)進んでいるのか調べる(基本的にはupdate関数内で使う想定)
    //猫がほとんどその場で止まっているよう(つまり壁や障害物にぶつかっている時)
    float CheckMoving()
    {
        //だいたい1秒ごとに動く
        timeleft -= Time.deltaTime;

        if (maxX < transform.position.x)
        {
            maxX = transform.position.x;
        }
        if (minX > transform.position.x)
        {
            minX = transform.position.x;
        }

        if (timeleft <= 0.0)
        {
            sumMove = maxX - minX;
            timeleft = 2.0f;
            maxX = -1;
            minX = 10000;
        }

        return sumMove;
    }

    //猫さんは壁にぶつかって前に進めない時は少し下がって3秒待つ
    //少し下がって2秒待つ動作
    IEnumerator WaitCatMoving()
    {
        // 秒停止
        yield return new WaitForSeconds(2);

        timing = 0;
        startCatDown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            worldAngle.y = 0.0f;
            worldAngle.x = 0.0f;
            //myTransform.eulerAngles = worldAngle; // 回転角度を設定

        }
    }
}
