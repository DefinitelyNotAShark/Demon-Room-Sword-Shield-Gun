using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public float bulletSpeed;

	void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
	}
}
