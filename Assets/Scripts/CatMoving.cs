using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class CatMoving : MonoBehaviour
{

    int catSpeed = 2;
    public GameObject goal;
    TimerController time = new TimerController();

    //public Transform goal;
    //NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        //nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを動かす
        transform.position += transform.right * catSpeed * Time.deltaTime;
        //nav.SetDestination(goal.transform.position);

        if (time.timer <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (transform.position.x >= goal.gameObject.transform.position.x)
        {
            SceneManager.LoadScene("Goal");
        }
    }
}
