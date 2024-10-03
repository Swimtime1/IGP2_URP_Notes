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
}
