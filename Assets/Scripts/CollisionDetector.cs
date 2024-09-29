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

    // Transform Variables
    [SerializeField] private Transform cam1, cam2;

    // Script Variables
    [SerializeField] private proceduralPyramid procPyr;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cubeChanged = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Changes the cube to a pyramid
        if((other.gameObject.CompareTag("Dissolve")) && (!cubeChanged))
        {
            cubeChanged = true;
            StartCoroutine(Dissolve());
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Entered");
        
        // Teleports Player to Portal 2
        if(other.gameObject.CompareTag("Portal 1"))
        { Debug.Log("Going to Portal 2");gameObject.transform.position = cam2.position; }

        // Teleports Player to Portal 1
        else if(other.gameObject.CompareTag("Portal 2"))
        { Debug.Log("Going to Portal 1");gameObject.transform.position = cam1.position; }
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

    #endregion
}
