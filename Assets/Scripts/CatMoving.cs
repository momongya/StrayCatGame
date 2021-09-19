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
        //プレイヤーを動かす
        transform.position += Quaternion.Euler(angles) * transform.right * catSpeed * Time.deltaTime;

        //猫の移動する速さはランダムに設定する
        catSpeed = Random.Range(1.5f, 3.0f);

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        //もし丸太の足場にあたったなら、丸太の角度に移動する
        if (collision.gameObject.CompareTag("wood"))
        {
            rb.velocity = new Vector3(0, 3, 0);
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
}
