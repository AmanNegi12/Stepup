using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject Impacteffect;
    [SerializeField] Vector3 RayOffset;
    [SerializeField] LayerMask OBsMask;
    [SerializeField] List<ParkourSystem> ParkourAction;
    [SerializeField] GameObject Something;
   
    Animator anim;
    Animation AnimClip;
    CharacterController characterController;
    public RaycastHit hitForward;
    public RaycastHit hitHeight;
    public bool InAction=false;
    private void Start()
    {
       
        AnimClip = GetComponent<Animation>();
        anim = GetComponent<Animator>();
        characterController=GetComponent<CharacterController>();
        _particleSystem.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)&& Input.GetKey(KeyCode.Mouse1))
        {
            shoot();
        }
        obstacleheight();
        climber();
        //Debug.Log(InAction);
        Something.transform.position = hitHeight.point;
      
    }
    private void shoot()
    {
         RaycastHit hit;
        _particleSystem.Play();
        if (Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit,100f))
        {
            if (hit.transform.gameObject!=null)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
            GameObject Imp=  Instantiate(Impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            Destroy(Imp,2f);
        }
    }
    public void obstacleheight()
    {
       if( Physics.Raycast(transform.position + RayOffset,transform.forward,out hitForward,0.8f, OBsMask))
        {

            Debug.DrawRay(transform.position + RayOffset, transform.forward*0.8f,Color.green);
            if (hitForward.transform!=null)
            {
                Physics.Raycast(hitForward.point+new Vector3(0,5,0),Vector3.down,out hitHeight,5f);
                Debug.DrawRay(hitForward.point + new Vector3(0, 5, 0), Vector3.down * 5f, Color.green);
            }
        }
        else
        {
            Debug.DrawRay(transform.position + RayOffset, transform.forward * 0.8f, Color.red);

        }
        if (hitHeight.transform!=null)
        {
            //Debug.Log(hitForward.point.z+"Forwardz");

        }
    }
    public void climber()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!InAction)
        {
            if (hitForward.transform!=null)
            {
                foreach (var item in ParkourAction)
                {
                    if (item.checkIfPossible(hitHeight,hitForward,this.transform))
                    {
                        StartCoroutine(DelayAct(item));
                        break;
                    }
                }
            }
           
            
        }

    }
    IEnumerator DelayAct(ParkourSystem action)
    {
                InAction = true;                 
                characterController.enabled=false;
                anim.CrossFade(action.Animname(),0.2f);
                yield return null;
               
                var duration = anim.GetNextAnimatorStateInfo(0);
                           
                //yield return new WaitForSeconds(duration.length);
                float Timer=0f;
                while (Timer<=duration.length)
                {
                  Timer += Time.deltaTime;
                    if (action.canrotate())
                    {
                      transform.rotation = Quaternion.RotateTowards(transform.rotation,action.RotateValue,500f*Time.deltaTime);

                    }
                    if (action.targetcanmatch())
                    {
                       MatchTarget(action);
                    }
                  yield return null;
                }
                yield return new WaitForSeconds(action.DelayAnimation());
                InAction = false;
                characterController.enabled=true;
                

    }
    void MatchTarget(ParkourSystem action)
    {
        if (anim.isMatchingTarget || anim.IsInTransition(0)) return;
        anim.MatchTarget(action.Matchpos,transform.rotation,action.matchbodypart(),new MatchTargetWeightMask(action.MatchPosWright(),0),action.matchstarttime(),action.targettime());
        
    }
}
