using UnityEngine;

public class EnemyHeavy : EnemyMovement {

    [SerializeField] private float fireRate;
    private float fireDelay = 0;
    private bool runOnce = false;

    private void Update()
    {

        if (canShoot && Time.time > fireDelay)
        {
            fireDelay = Time.time + fireRate;
            if (runOnce)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<ShootProjectile>().Shoot();
                }
            }
            else
                runOnce = true;
        }
    }
}
