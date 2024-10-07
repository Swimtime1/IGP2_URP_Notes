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

    // 
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

        Context.SetRigWeight(1.0f);
    }

    // 
    protected void UpdateIkTargetPos(Collider other)
    {
        if(other == Context.CurrOtherCollider)
        {
            SetIkTargetPos();
        }
    }

    // 
    protected void ResetIkTargetPosTrack(Collider other)
    {
        if(other == Context.CurrOtherCollider)
        {
            Context.CurrOtherCollider = null;
            Context.ClosestPointFromShoulder = Vector3.positiveInfinity;
            Context.GetLeftIKConstraint.data.target.transform.localPosition = LIK_DefaultPos;
            Context.GetRightIKConstraint.data.target.transform.localPosition = RIK_DefaultPos;
        }

        Context.SetRigWeight(0.0f);
    }

    //
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
        Context.CurrIkTargetTransform.position = offsetPos;
    }
}
