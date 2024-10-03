using UnityEngine;

public abstract class EnvironmentInteractionState : BaseState<EnvironmentInteractionStateMachine.EEIS>
{
    #region Variables

    protected EnvironmentInteractionContext Context;

    #endregion Variables

    // Constructor
    public EnvironmentInteractionState(EnvironmentInteractionContext _context,
        EnvironmentInteractionStateMachine.EEIS stateKey) : base(stateKey)
    {
        Context = _context;
    }

    // Determines the point on the mesh of another object that is closest
    private Vector3 GetClosestPoint(Collider other, Vector3 posToCheck)
    { return other.ClosestPoint(posToCheck); }

    // 
    protected void StartIkTargetPosTrack(Collider other)
    {
        Vector3 closestPointFromRoot = GetClosestPoint(other, Context.GetRootTransform.position);
        Context.SetCurrSide(closestPointFromRoot);
    }

    // 
    protected void UpdateIkTargetPosTrack(Collider other)
    {
        
    }

    // 
    protected void ResetIkTargetPosTrack(Collider other)
    {
        
    }
}
