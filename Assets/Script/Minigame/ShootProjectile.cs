using UnityEngine;

public class ShootProjectile : MonoBehaviour {

    [SerializeField] private GameObject bulletSprite;

    // Spawn a bullet
    public void Shoot()
    {
        Instantiate(bulletSprite, transform.position + transform.up * 0.9f, transform.rotation);
    }
}
