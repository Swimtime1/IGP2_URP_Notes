using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    #region Variables

    // Animator Variables
    [SerializeField] private Animator anim;

    // LayerMask Variables
    [SerializeField] private LayerMask layerMask;

    // Float Variables
    [Range (0, 1f)]
    [SerializeField] private float DistanceToGround;

    #endregion Variables

    // Called by Animator layers with IK Pass every frame
    private void OnAnimatorIK(int layerIndex)
    {
        // Only runs if anim is set
        if(anim)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            // Left Foot
            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {
                Vector3 footPos = hit.point;
                footPos.y += DistanceToGround;
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);
                anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }

            // Right Foot
            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {
                Vector3 footPos = hit.point;
                footPos.y += DistanceToGround;
                anim.SetIKPosition(AvatarIKGoal.RightFoot, footPos);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
