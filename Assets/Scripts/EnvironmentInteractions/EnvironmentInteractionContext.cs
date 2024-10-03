using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnvironmentInteractionContext
{
    #region Variables

    private TwoBoneIKConstraint _leftIkConstraint;
    private TwoBoneIKConstraint _rightIkConstraint;
    private MultiRotationConstraint _leftMRConstraint;
    private MultiRotationConstraint _rightMRConstraint;
    private CharacterController _characterController;

    #endregion Variables

    // Constructor
    public EnvironmentInteractionContext(TwoBoneIKConstraint lIK, TwoBoneIKConstraint rIK,
        MultiRotationConstraint lMR, MultiRotationConstraint rMR, CharacterController cc)
    {
        _leftIkConstraint = lIK;
        _rightIkConstraint = rIK;
        _leftMRConstraint = lMR;
        _rightMRConstraint = rMR;
        _characterController = cc;
    }

    #region Getters

    public TwoBoneIKConstraint GetLeftIKConstraint => _leftIkConstraint;
    public TwoBoneIKConstraint GetRightIKConstraint => _rightIkConstraint;
    public MultiRotationConstraint GetLeftMRConstraint => _leftMRConstraint;
    public MultiRotationConstraint GetRightMRConstraint => _rightMRConstraint;
    public CharacterController GetCharacterController => _characterController;

    #endregion Getters
}
