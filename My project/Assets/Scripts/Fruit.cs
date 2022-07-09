using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void Start() => Destroy(gameObject, 4);


    void Update() => transform.Rotate(transform.forward * 350 * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
           Destroy(gameObject);
    }

}
