using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class IKBoughController : MonoBehaviour
{
    #region Variables

    [Header("Locations")]
    [SerializeField] private Vector3 initPos;
    [SerializeField] private Vector3 awayPos;

    [Header("Dimensions")]
    [SerializeField] private GameObject player;
    [SerializeField] private float playerRadius = 2.5f;
    [SerializeField] private float S;

    #endregion Variables
    
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        awayPos = new Vector3(initPos.x, initPos.y + playerRadius, initPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        S = Vector3.Distance(initPos, player.transform.position);
        S /= playerRadius;
        S = 1 - S;
        S = Mathf.Clamp01(S);
        transform.position = Vector3.Lerp(initPos, awayPos, S);
    }
}
