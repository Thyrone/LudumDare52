using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public Transform earth;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(earth);
    }
}
