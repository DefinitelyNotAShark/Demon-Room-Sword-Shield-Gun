using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;

    private void Start()
    {
        StartCoroutine(bulletLifetime());//start the bullet life. At the end, it will destroy
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
	}

    IEnumerator bulletLifetime()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(this.gameObject);//destroy after our lifetime float
    }


}
