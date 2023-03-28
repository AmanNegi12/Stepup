using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;

public class MainPlayer : MonoBehaviour
{
    GunFire AnimDisabler;
    [SerializeField] Rig AimRig;
    float AimRigWeight;
   [SerializeField] CinemachineVirtualCamera ChineCamera;
    [SerializeField] GameObject crosshair;
    [SerializeField] LayerMask Aimmask;
    [SerializeField] LayerMask glayer;
    [SerializeField] float Aimspeed;
    enum hands { none,weapon,noweapon }
    [SerializeField] Animator anim;
    [SerializeField] Transform camForawrd;
    [SerializeField] Transform TargetRotation;
    CharacterController characterController;
    [SerializeField]float Mspeed=5f;
    [SerializeField]float Rspeed=1000f;
    [SerializeField]float jump=5f;
    [SerializeField]float yspeed=3f;
    [SerializeField]float AnimSpeed;
    Quaternion rotater;
   [SerializeField] Vector3 offset;
    float horizontal;
    float vertical;
    float Rhorizontal;
    float Rvertical;
    bool camdetection;
    bool Aim;
    bool Isground;
    GameObject Gun;
   // Start is called before the first frame update
   hands decider =hands.none;
    [SerializeField] float rotspeed=50f;

    void Start()
    {
        AnimDisabler = GetComponent<GunFire>();
        decider = hands.noweapon;
        Gun = GameObject.FindGameObjectWithTag("M416");
        Gun.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        Animation();
        decid();
        Trotat();
        Gcheck();
        AimRig.weight = Mathf.Lerp(AimRig.weight,AimRigWeight,20f*Time.deltaTime);

    }
    void move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 Mdir = new Vector3(horizontal,0f,vertical);
        Mdir.Normalize();
        AnimSpeed = Mdir.magnitude;
       
        Mdir = Quaternion.AngleAxis(camForawrd.rotation.eulerAngles.y,Vector3.up)*Mdir; 

     
        if (Mdir.magnitude>0f)
        {
            Quaternion rot = Quaternion.LookRotation(Mdir, Vector3.up);
            if (AnimDisabler.InAction == false)
            {

             transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Rspeed * Time.deltaTime);
            }
        }

        if (camdetection==true)
        {
            Vector3 playerRot = camForawrd.eulerAngles;
            playerRot.x = 0;
            playerRot.z = 0;
           
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(playerRot), 3000f * Time.deltaTime);

        }



        if (!Isground)
        {
            yspeed += Physics.gravity.y*Time.deltaTime;
        }
        else
        {
            yspeed = -1f*Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&Isground)
        {
            yspeed = jump;
        }
       

        Vector3 velocity = Mdir;
        velocity.y = yspeed;
        if (AnimDisabler.InAction==false)
        {
             characterController.Move(velocity*Mspeed*Time.deltaTime);

        }
       
        if (Input.GetKey(KeyCode.Mouse1)&& decider == hands.weapon)
        {
            ChineCamera.gameObject.SetActive(true);
            crosshair.gameObject.SetActive(true);
            camdetection = true;
            anim.SetBool("Aiming",true);
            Aim = true;
            AimRigWeight = 1f;

        }
        else
        {
            camdetection = false;

            ChineCamera.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(false);
            anim.SetBool("Aiming",false);
            Aim = false;
            AimRigWeight = 0f;
        }
        if (Input.GetKey(KeyCode.Mouse1) && decider == hands.weapon && Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("Fire",true);
        }
        else
        {
            anim.SetBool("Fire",false);

        }
    }
    void GunEnabler()
    {   
        Gun.SetActive(true);
  
    }
    void GunDiasble()
    {
        Gun.SetActive(false);

    }
    void Animation()
    {
        float CV = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        AnimSpeed = Mathf.Clamp01(CV);

        if (Input.GetKey(KeyCode.LeftShift) && AnimSpeed >= 0.2f && Aim==false)
        {
            anim.SetFloat("Speed", 2, 0.1f, Time.deltaTime);
            Mspeed = 8f;
        }
        else
        {
            anim.SetFloat("Speed", AnimSpeed, 0.1f, Time.deltaTime);
            Mspeed = 5f;

        }

        if (decider == hands.weapon)
        {
            anim.SetBool("weapon", true);

        }
        if (decider == hands.noweapon)
        {
            anim.SetBool("weapon", false);

        }
    }
    void decid()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           
            if (decider==hands.noweapon)
            {
                decider = hands.weapon;
              
            }
            else
            {
                decider=hands.noweapon;
            }
        }
    }
    void Trotat()
    {
        Rhorizontal += Input.GetAxis("Mouse X") * rotspeed * Time.deltaTime;
        Rvertical += Input.GetAxis("Mouse Y") * rotspeed * Time.deltaTime;
        Rvertical = Mathf.Clamp(Rvertical, -90f, 20f);
     
         rotater = Quaternion.Euler(-Rvertical, Rhorizontal, 0);


        TargetRotation.rotation = rotater;
    }
    void Gcheck()
    {
        Isground = Physics.CheckSphere(transform.TransformPoint(offset), 0.2f, glayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.TransformPoint(offset), 0.2f);
    }
}
