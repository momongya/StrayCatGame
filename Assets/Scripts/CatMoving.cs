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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを動かす
        transform.position += transform.right * catSpeed * Time.deltaTime;

        //猫の移動する速さはランダムに設定する
        catSpeed = Random.Range(1.5f, 3.0f);


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
        //猫の角度は接する床面に依存する(z軸だけ)
        transform.Rotate(new Vector3(0, 0, collision.gameObject.transform.rotation.z));
    }
}
