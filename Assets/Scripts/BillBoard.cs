using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {
    
    private Camera targetCamera;

    // Use this for initialization
    void Start ()
    {
        targetCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (targetCamera.transform.position != transform.position)
        {
            transform.LookAt(targetCamera.transform.position);
        }
    }
}
