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

        //猫の回転角を取得
    }

    // Update is called once per frame
    void Update()
    {

        //猫の移動する速さはランダムに設定する
        catSpeed = Random.Range(1.5f, 3.0f);

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
            Vector2 myGravity = new Vector2(0, 9.81f);

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
        angles = new Vector3(0, 180, 0);

        if (1.4 <= transform.position.x - catFood[i].gameObject.transform.position.x)
        {
            transform.position += Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;
        }

        // 3秒停止
        yield return new WaitForSeconds(3);

        // 猫缶消去
        itemController.DeleteCatFood(i);

        // 存在の抹消
        itemController.SetFoodStatus(i, false);

        //前を向く
        angles = new Vector3(0, 0, 0);
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
