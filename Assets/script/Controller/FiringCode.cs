using System.Collections;
using UnityEngine;

public class FiringCode : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Barel;
    public float fireForce;
    public float FireRate;
    private bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        FreeFire();
    }

    public IEnumerator FireCoroutine()
    {
        while (isFiring)
        {
            FireBullet();
            yield return new WaitForSeconds(1f / FireRate);
        }
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(Bullet, Barel.position, Barel.rotation);
        bullet.GetComponent<Rigidbody>().velocity = Barel.forward * fireForce;
    }

    public void FreeFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            StartCoroutine(FireCoroutine());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
    }
}