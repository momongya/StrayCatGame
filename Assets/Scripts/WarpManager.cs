using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpManager : MonoBehaviour
{
    public GameObject[] flag;
    bool[] flagManage;
    Transform[] warpPoint = new Transform[3];

    Sprite spriteMae;
    public Sprite spriteAto;
    public Image warpButton;

    // Start is called before the first frame update
    void Start()
    {
        flagManage = new bool[flag.Length];
        for (int i = 0; i < flag.Length; i++)
        {
            flagManage[i] = false;
        }
         spriteMae = warpButton.gameObject.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlagPoint();
    }

    void CheckFlagPoint()
    {
        if (transform.position.x >= flag[0].gameObject.transform.position.x && flagManage[0] == false)
        {
            warpPoint[0] = flag[0].gameObject.transform;
            flag[0].SetActive(true);
            flagManage[0] = true;
            warpButton.gameObject.GetComponent<Image>().sprite = spriteAto;
        }
        if (transform.position.x >= flag[1].gameObject.transform.position.x && flagManage[1] == false)
        {
            warpPoint[1] = flag[1].gameObject.transform;
            flag[1].SetActive(true);
            flagManage[1] = true;
        }
        if (transform.position.x >= flag[2].gameObject.transform.position.x && flagManage[2] == false)
        {
            warpPoint[2] = flag[2].gameObject.transform;
            flag[2].SetActive(true);
            flagManage[2] = true;
        }
    }

    public void WarpCat()
    {
        int j = -1;
        for (int i = 0; i < flagManage.Length; i++)
        {
            if (flagManage[i] == true)
            {
                j = i;
            }
        }
        if (j >= 0)
        {
            // 一番近くのチェックポイントにワープ
            transform.position = warpPoint[j].position;
        }
    }
}
