using UnityEngine;

public abstract class EnvironmentInteractionState : BaseState<EnvironmentInteractionStateMachine.EEIS>
{
    #region Variables

    protected EnvironmentInteractionContext Context;
    protected Vector3 LIK_DefaultPos, RIK_DefaultPos;

    #endregion Variables

    // Constructor
    public EnvironmentInteractionState(EnvironmentInteractionContext context,
        EnvironmentInteractionStateMachine.EEIS stateKey) : base(stateKey)
    {
        Context = context;
        LIK_DefaultPos = Context.GetLeftIKConstraint.data.target.transform.localPosition;
        RIK_DefaultPos = Context.GetRightIKConstraint.data.target.transform.localPosition;
    }

    // Determines the point on the mesh of another object that is closest
    private Vector3 GetClosestPoint(Collider other, Vector3 posToCheck)
    { return other.ClosestPoint(posToCheck); }

    // Begins tracking where the hand should reach for
    protected void StartIkTargetPosTrack(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Interactable") &&
            Context.CurrOtherCollider == null)
        {
            Context.CurrOtherCollider = other;
            Vector3 closestPointFromRoot = GetClosestPoint(other, Context.GetRootTransform.position);
            Context.SetCurrSide(closestPointFromRoot);

            SetIkTargetPos();
        }
    }

    // Makes sure that the position of the hand is updated
    protected void UpdateIkTargetPos(Collider other)
    {
        if(other == Context.CurrOtherCollider)
        {
            SetIkTargetPos();
        }
    }

    // Stops IK Tracking, and returns to normal animation
    protected bool ResetIkTargetPosTrack(Collider other)
    {
        if(other == Context.CurrOtherCollider)
        {
            Context.CurrOtherCollider = null;
            Context.ClosestPointFromShoulder = Vector3.positiveInfinity;
            Context.GetLeftIKConstraint.data.target.transform.localPosition = LIK_DefaultPos;
            Context.GetRightIKConstraint.data.target.transform.localPosition = RIK_DefaultPos;
            Context.ResetRigWeight();
            return true;
        }

        return false;
    }

    // Determines where on the surface being touched the IK Target should be
    private void SetIkTargetPos()
    {
        float xPos = Context.CurrShoulderTransform.position.x;
        float yPos = Context.CurrShoulderTransform.position.y;
        float zPos = Context.CurrShoulderTransform.position.z;
        Vector3 shoulderPos = new Vector3(xPos, yPos, zPos);
        
        Context.ClosestPointFromShoulder = GetClosestPoint(Context.CurrOtherCollider, shoulderPos);

        Vector3 rayDirection = Context.CurrShoulderTransform.position - Context.ClosestPointFromShoulder;
        Vector3 normalizedRayDirection = rayDirection.normalized;
        float offsetDist = .05f;
        Vector3 offset = normalizedRayDirection * offsetDist;

        Vector3 offsetPos = Context.ClosestPointFromShoulder + offset;
        /* Vector3 midway = (offsetPos + shoulderPos) / 2.0f; */
        Context.CurrIkTargetTransform.position = offsetPos;
    }
}
