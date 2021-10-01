using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject[] flag;
    bool[] flagManage;
    List<Transform> warpPoint = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        flagManage = new bool[flag.Length];
        for (int i = 0; i < flag.Length; i++)
        {
            flagManage[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlagPoint();

        Vector3 mousePos = Input.mousePosition;
        // Z軸修正
        mousePos.z = 0f;
    }

    void CheckFlagPoint()
    {
        if (transform.position.x >= flag[0].gameObject.transform.position.x && flagManage[0] == false)
        {
            warpPoint.Add(flag[0].gameObject.transform);
            flag[0].SetActive(true);
            flagManage[0] = true;
        }
        if (transform.position.x >= flag[1].gameObject.transform.position.x && flagManage[1] == false)
        {
            warpPoint.Add(flag[1].gameObject.transform);
            flag[1].SetActive(true);
            flagManage[1] = true;
        }
        if (transform.position.x >= flag[2].gameObject.transform.position.x && flagManage[2] == false)
        {
            warpPoint.Add(flag[2].gameObject.transform);
            flag[2].SetActive(true);
            flagManage[2] = true;
        }
    }
}
