using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevicesOperator : MonoBehaviour
{
    public float oprRadius = 1.5f;
    

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
		{
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, oprRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0)
				{
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
                
            }
        }
        
    }
}
