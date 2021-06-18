using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] Vector3 doorOpenPosition;

    private bool isOpen;
    public void Operate()
	{
        if (isOpen)
		{
			Vector3 positon = transform.position - doorOpenPosition;
			transform.position = positon;
		}
		else
		{
			Vector3 position = transform.position + doorOpenPosition;
			transform.position = position;
		}
		isOpen = !isOpen;
	}

	public void Activate()
	{
		if (!isOpen)
		{
			Vector3 pos = transform.position + doorOpenPosition;
			transform.position = pos;
			isOpen = true;
		}
	}
	public void Deactivate()
	{
		if (isOpen)
		{
			Vector3 pos = transform.position - doorOpenPosition;
			transform.position = pos;
			isOpen = false;
		}
	}
}
