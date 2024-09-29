using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    #region Variables

    // Material Variables
    [SerializeField] private Material dissolveMat;

    // Boolean Variables
    [SerializeField] private bool cubeChanged;
    private bool canTeleport;

    // Transform Variables
    [SerializeField] private Transform port1, port2;

    // Script Variables
    [SerializeField] private proceduralPyramid procPyr;

    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        cubeChanged = false;
        canTeleport = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Changes the cube to a pyramid
        if((other.gameObject.CompareTag("Dissolve")) && (!cubeChanged))
        {
            cubeChanged = true;
            StartCoroutine(Dissolve());
        }
        
        // Teleports Player to Portal 2
        if(other.gameObject.CompareTag("Portal 1") && canTeleport)
        {
            canTeleport = false;
            this.gameObject.transform.position = port2.position;
            StartCoroutine(TeleportCooldown());
        }

        // Teleports Player to Portal 1
        else if(other.gameObject.CompareTag("Portal 2") && canTeleport)
        {
            canTeleport = false;
            this.gameObject.transform.position = port1.position;
            StartCoroutine(TeleportCooldown());
        }
    }

    #region Coroutines

    // Dissolves anything with the dissolveMat Material, changes it to a 
    // pyramid, and undissolves it
    private IEnumerator Dissolve()
    {
        // dissolves
        for(float i = 0f; i <= 1.0f; i += 0.1f)
        {
            dissolveMat.SetFloat("_Alpha_Clip_Threshold", i);
            yield return new WaitForSeconds(0.1f);
        }

        // change to pyramid
        procPyr.TransformToPyramid();

        // undissolves
        for(float i = 1.0f; i >= 0.0f; i -= 0.1f)
        {
            dissolveMat.SetFloat("_Alpha_Clip_Threshold", i);
            yield return new WaitForSeconds(0.1f);
        }
        dissolveMat.SetFloat("_Alpha_Clip_Threshold", 0.0f);
    }

    // Gives the player a cooldown on teleportation so that they aren't stuck in 
    // an infinite portal jump
    private IEnumerator TeleportCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canTeleport = true;
    }

    #endregion Coroutines
}
