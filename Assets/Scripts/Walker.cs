using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Walker : MonoBehaviour
{
    #region Variables

    // Transform Variables
    public Transform leftFootTarget, rightFootTarget;

    // AnimationCurve Variables
    public AnimationCurve horizontalCurve, VerticalCurve;

    // Vector3 Variables
    private Vector3 leftOffset, rightOffset;

    // Float Variables
    private float leftLegLast = 0;
    private float rightLegLast = 0;
    [SerializeField] private float runSpeed, walkSpeed, runTime, walkTime;
    [SerializeField] private float horVal, vertVal;

    // Animator Variables
    public Animator animator;

    // Rig Variables
    public Rig rig;

    #endregion Variables
    
    // Start is called before the first frame update
    void Start()
    {
        leftOffset = leftFootTarget.localPosition;
        rightOffset = rightFootTarget.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        
        float fEnd = horizontalCurve[1].time;
        float uEnd = VerticalCurve[1].time;
        
        float leftLegForwardMovement = horizontalCurve.Evaluate(Time.time);
        float rightLegForwardMovement = horizontalCurve.Evaluate(Time.time - fEnd);
        float leftLegUpwardMovement = VerticalCurve.Evaluate(Time.time + uEnd);
        float rightLegUpwardMovement = VerticalCurve.Evaluate(Time.time - uEnd);
        
        // Moves the local position of the feet
        leftFootTarget.localPosition = leftOffset + 
            (this.transform.InverseTransformVector(leftFootTarget.forward) * leftLegForwardMovement) + 
            (this.transform.InverseTransformVector(leftFootTarget.up) * leftLegUpwardMovement);
        rightFootTarget.localPosition = rightOffset + 
            (this.transform.InverseTransformVector(rightFootTarget.forward) * rightLegForwardMovement) + 
            (this.transform.InverseTransformVector(rightFootTarget.up) * rightLegUpwardMovement);
        
        // Determines if each leg is moving forward or backward
        float leftLegDirection = leftLegForwardMovement - leftLegLast;
        float rightLegDirection = rightLegForwardMovement - rightLegLast;
        
        // Makes sure that both legs are grounded while moving backward
        RaycastHit hit;
        if(leftLegDirection < 0 &&
            Physics.Raycast(leftFootTarget.position + leftFootTarget.up, -leftFootTarget.up, out hit, Mathf.Infinity))
        { leftFootTarget.position = hit.point; }
        if(rightLegDirection < 0 &&
            Physics.Raycast(rightFootTarget.position + rightFootTarget.up, -rightFootTarget.up, out hit, Mathf.Infinity))
        { rightFootTarget.position = hit.point; }

        // Updates the last known direction of each leg
        leftLegLast = leftLegForwardMovement;
        rightLegLast = rightLegForwardMovement;
    }

    // Determine how quickly the player is moving, and updates the animation
    private void ChangeSpeed()
    {
        // assumes running
        if(animator.GetFloat("Speed") >= runSpeed)
        {
            rig.weight = 1.0f;
            UpdateCurveEnd(runTime);
        }

        // assumes walking
        else if(animator.GetFloat("Speed") >= walkSpeed)
        {
            rig.weight = 1.0f;
            UpdateCurveEnd(walkTime);
        }

        // assumes idle
        else { rig.weight = 0.0f; }
    }

    // Updates the leg movement speed to match the player speed
    private void UpdateCurveEnd(float time)
    {
        // update horizontalCurve
        int index = horizontalCurve.AddKey(time, horVal);
        if(index == 2) { horizontalCurve.RemoveKey(1); }
        else if(index == 1) { horizontalCurve.RemoveKey(2); }
        
        // update VerticalCurve
        index = VerticalCurve.AddKey(time, vertVal);
        if(index == 2) { VerticalCurve.RemoveKey(1); }
        else if(index == 1) { VerticalCurve.RemoveKey(2); }
    }
}
