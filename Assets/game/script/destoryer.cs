using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryer : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    float force=100f;
    [SerializeField] Transform parent;
    private void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    private void Update()
    {
        GameObject find = GameObject.FindGameObjectWithTag("point");
        rb.AddForce(find.transform.forward*force);
        Destroy(this.gameObject,5f);
       
    }
   
   
}
