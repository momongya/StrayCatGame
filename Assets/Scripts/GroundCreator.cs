using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    public GameObject leaf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        // Z軸修正
        mousePos.z = 10f;
        if (Input.GetMouseButton(0))
        {
            Instantiate(leaf, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
        }
    }
}
