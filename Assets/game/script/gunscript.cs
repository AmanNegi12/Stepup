using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunscript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletpoint;
  
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        gunfire();
    }
    void gunfire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject released = Instantiate(bullet, bulletpoint.position,Quaternion.identity);
           
        }
    }
}
