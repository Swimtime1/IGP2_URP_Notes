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
        leftFootTarget.localPosition = leftOffset + 
            (this.transform.InverseTransformVector(leftFootTarget.forward) * horizontalCurve.Evaluate(Time.time)) + 
            (this.transform.InverseTransformVector(leftFootTarget.up) * VerticalCurve.Evaluate(Time.time + 0.5f));
        rightFootTarget.localPosition = rightOffset + 
            (this.transform.InverseTransformVector(rightFootTarget.forward) * horizontalCurve.Evaluate(Time.time - 1)) + 
            (this.transform.InverseTransformVector(rightFootTarget.up) * VerticalCurve.Evaluate(Time.time - 0.5f));
    }
}
