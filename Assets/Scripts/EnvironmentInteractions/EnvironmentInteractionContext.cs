using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnvironmentInteractionContext
{
    #region Variables

    public enum EBodySide
    {
        RIGHT,
        LEFT
    }

    private TwoBoneIKConstraint _leftIkConstraint;
    private TwoBoneIKConstraint _rightIkConstraint;
    private MultiRotationConstraint _leftMRConstraint;
    private MultiRotationConstraint _rightMRConstraint;
    private CharacterController _characterController;
    private Transform _rootTransform;

    #region Current

    public Collider CurrOtherCollider { get; set;}
    public TwoBoneIKConstraint CurrIkConstraint { get; private set; }
    public MultiRotationConstraint CurrMRConstraint { get; private set; }
    public Transform CurrIkTargetTransform { get; private set; }
    public Transform CurrShoulderTransform { get; private set; }
    public EBodySide CurrBodySide { get; private set; }

    #endregion Current

    #endregion Variables

    // Constructor
    public EnvironmentInteractionContext(TwoBoneIKConstraint lIK, TwoBoneIKConstraint rIK,
        MultiRotationConstraint lMR, MultiRotationConstraint rMR, CharacterController cc,
        Transform rootTransform)
    {
        _leftIkConstraint = lIK;
        _rightIkConstraint = rIK;
        _leftMRConstraint = lMR;
        _rightMRConstraint = rMR;
        _characterController = cc;
        _rootTransform = rootTransform;

        CharacterShoulderHeight = lIK.data.root.transform.position.y;
    }

    #region Getters

    public TwoBoneIKConstraint GetLeftIKConstraint => _leftIkConstraint;
    public TwoBoneIKConstraint GetRightIKConstraint => _rightIkConstraint;
    public MultiRotationConstraint GetLeftMRConstraint => _leftMRConstraint;
    public MultiRotationConstraint GetRightMRConstraint => _rightMRConstraint;
    public CharacterController GetCharacterController => _characterController;
    public Transform GetRootTransform => _rootTransform;

    #endregion Getters

    public Vector3 ClosestPointFromShoulder { get; set; } = Vector3.positiveInfinity;
    public float CharacterShoulderHeight { get; private set; }

    #region Setters

    public void SetCurrSide(Vector3 posToCheck)
    {
        Vector3 lShoulder = _leftIkConstraint.data.root.transform.position;
        Vector3 rShoulder = _rightIkConstraint.data.root.transform.position;

        bool lCloser = Vector3.Distance(posToCheck, lShoulder) < Vector3.Distance(posToCheck, rShoulder);

        // sets the Current variables
        if(lCloser)
        {
            Debug.Log("Left side is closer");
            CurrBodySide = EBodySide.LEFT;
            CurrIkConstraint = _leftIkConstraint;
            CurrMRConstraint = _leftMRConstraint;
        }
        else
        {
            Debug.Log("Right side is closer");
            CurrBodySide = EBodySide.RIGHT;
            CurrIkConstraint = _rightIkConstraint;
            CurrMRConstraint = _rightMRConstraint;
        }

        // Set Rig Weights
        CurrIkConstraint.weight = 1.0f;
        CurrMRConstraint.weight = 1.0f;

        CurrShoulderTransform = CurrIkConstraint.data.root.transform;
        CurrIkTargetTransform = CurrIkConstraint.data.target.transform;
    }

    // Resets the weight of the overall Rig
    public void ResetRigWeight()
    {
        _leftIkConstraint.weight = 0.0f;
        _leftMRConstraint.weight = 0.0f;
        _rightIkConstraint.weight = 0.0f;
        _rightMRConstraint.weight = 0.0f;
    }

    #endregion Setters
}
