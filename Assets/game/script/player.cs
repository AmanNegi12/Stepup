using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Animator anim;
    enum PlayerState {idle,walking,runing,WeaponWalk,WeaponRun,wewaponIdle }
    enum PlayerHandState {weapon,noweapon }
    CharacterController characterController;
    [SerializeField] float Mspeed = 10f;
    [SerializeField] float rspeed = 10f;
    [SerializeField] float jump = 100f;
    [SerializeField] float  yspeed = 5f;
    Vector3 velocity=new Vector3();
    [SerializeField] Transform cameraadduprotation;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
       
      
    }
    void move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movedir=new Vector3(horizontal, 0f, vertical);
        movedir.Normalize();
        movedir = Quaternion.AngleAxis(cameraadduprotation.rotation.eulerAngles.y,Vector3.up)*movedir;
        if (movedir.magnitude>0)
        {
      
            Quaternion rot=Quaternion.LookRotation(movedir,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot,rspeed*Time.deltaTime);
            
        }
        if (!characterController.isGrounded)
        {
          yspeed += Physics.gravity.y*Time.deltaTime;

        }
        else
        {
            yspeed = -1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            yspeed = jump;
            
        }
       
        velocity = movedir * Mspeed;
        velocity.y = yspeed;
        
        characterController.Move(velocity*Time.deltaTime);

        

    }
  

}
