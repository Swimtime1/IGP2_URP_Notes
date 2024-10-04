using UnityEngine;

public abstract class EnvironmentInteractionState : BaseState<EnvironmentInteractionStateMachine.EEIS>
{
    #region Variables

    protected EnvironmentInteractionContext Context;

    #endregion Variables

    // Constructor
    public EnvironmentInteractionState(EnvironmentInteractionContext context,
        EnvironmentInteractionStateMachine.EEIS stateKey) : base(stateKey)
    {
        Context = context;
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
        }
    }

    //
    private void SetIkTargetPos()
    {
        Context.ClosestPointFromShoulder = GetClosestPoint(Context.CurrOtherCollider,
        Context.CurrShoulderTransform.position);
    }
}
