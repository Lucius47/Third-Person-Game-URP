using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    public float rotationSpeed = 1.5f;

    private float rotY;
    private Vector3 offset;
    void Start()
    {
        rotY = transform.eulerAngles.y;

        offset = target.position - transform.position;
        
    }

    
    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        float horMouseInput = Input.GetAxisRaw("Mouse X");

        if (horMouseInput != 0)
		{
            //rotY += rotationSpeed * horInput;
            rotY += horMouseInput * rotationSpeed * 10f;
        }
        else
		{
            //rotY += horMouseInput * rotationSpeed * 3f;
            
            rotY += rotationSpeed * horInput;
        }

        Quaternion rotation = Quaternion.Euler(0, rotY, 0);

        transform.position = target.position - (rotation * offset);

        transform.LookAt(target);
    }
}
