using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] private GameObject objectToBeSpawned2;
    [SerializeField] private GameObject objectToBeSpawned3;
    [SerializeField] private GameObject objectToBeSpawned4;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objectToBeSpawned);
    }

    // Update is called once per frame
    
}
