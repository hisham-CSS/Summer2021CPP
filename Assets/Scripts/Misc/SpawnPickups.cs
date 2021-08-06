using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public Pickup[] pickupPrefabArray;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pickupPrefabArray[0], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
