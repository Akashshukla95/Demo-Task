using UnityEngine;

public class Gun1 : MonoBehaviour
{
	public float damage = 10f;
	public float range = 100f;

	public float fireRate = 15f;
	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	public float impactForce;

	private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}
    }

	void Shoot()
	{
		muzzleFlash.Play(); 
		RaycastHit hit;
		if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			Target target = hit.transform.GetComponent<Target>();
			if(target != null)
			{
				target.TakeDamage(damage);
			}

			if(hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * impactForce);
			}

			GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactGO, .5f);
		}
	}
}
