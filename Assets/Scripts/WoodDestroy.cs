using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteWood()
    {
        transform.position = new Vector3(-1000, -1000, -1000);
    }
}
