using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="ParkourSysem/newParkouAnimation")]
public class ParkourSystem : ScriptableObject
{

    

    [SerializeField] string AnimName;
    [SerializeField] float MinHeight;
    [SerializeField] float MaxHeight;
    [SerializeField] bool CanRotate;
    [SerializeField] bool TargetCanMatch;
    [SerializeField] AvatarTarget MatchBodyPart;
    [SerializeField] float MatchStartTime;
    [SerializeField] float TargetTime;
    [SerializeField] float Delayanimation;
    [SerializeField] Vector3 MatchposWeight;
    public Quaternion RotateValue { get; set; }
    public Vector3 Matchpos { get; set; }


    public bool checkIfPossible(RaycastHit Height,RaycastHit Forward,Transform player)
    {
        float ObstacleHeight=Height.point.y-player.position.y;
        if (ObstacleHeight>MinHeight&&ObstacleHeight<MaxHeight)
        {
            if (CanRotate)
            {
                RotateValue = Quaternion.LookRotation(-Forward.normal);
            }
            if (TargetCanMatch)
            {
                Matchpos = Height.point;
            }

            return true;
           
        }
        else
        {
            return false;
        }

    }

    public string Animname()
    {
        return AnimName;
    }
    public Vector3 MatchPosWright() 
    {
        return MatchposWeight;
    }
    public bool targetcanmatch()
    {
        return TargetCanMatch;
    } 
    public bool canrotate()
    {
        return CanRotate;
    }
    public float matchstarttime()
    {
        return MatchStartTime;
    } 
    public float targettime()
    {
        return TargetTime;
    }
    public AvatarTarget matchbodypart()
    {
        return MatchBodyPart;
    }
    public float DelayAnimation()
    {
        return Delayanimation;
    }

}
