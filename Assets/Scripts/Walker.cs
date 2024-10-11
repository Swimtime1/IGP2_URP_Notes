using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
