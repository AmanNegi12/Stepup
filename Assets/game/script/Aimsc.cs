using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimsc : MonoBehaviour
{
    [SerializeField] Camera cam;
    Vector3 aimPos;


    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
       

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            aimPos= hit.point;
            // Use aimPos for aiming
        }
        else
        {
           aimPos = ray.GetPoint(999f);

            // Use aimPos for aiming
        }
        transform.position = aimPos;

    }
}
