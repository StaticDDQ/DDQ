using UnityEngine;
using System.Collections;

public class EnemyShooter : EnemyMovement {

    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate;
    [SerializeField] private float delayTime;
    private float fireDelay = 0;
    private bool runOnce = false;
    private bool shootProj = false;

    private void Update() {
		if(canShoot && Time.time > fireDelay)
        {
            if (shootProj)
            {
                fireDelay = Time.time + fireRate;
                ShootProjectile();
            }
            else if(!runOnce)
            {
                runOnce = true;
                StopAllCoroutines();
                StartCoroutine(Delay());
            }
        }
	}

    private IEnumerator Delay()
    {
        float startTime = Time.time;
        while (Time.time < startTime + delayTime)
            yield return null;
        shootProj = true;
    }

    private void ShootProjectile()
    {
        var bullet = (GameObject)Instantiate(bulletSprite, transform.position + transform.up * 0.9f, transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        Destroy(bullet, 1.1f);
    }

    public override void SetCanShoot(bool canShoot)
    {
        base.SetCanShoot(canShoot);
        runOnce = false;
        shootProj = false;
    }
}
