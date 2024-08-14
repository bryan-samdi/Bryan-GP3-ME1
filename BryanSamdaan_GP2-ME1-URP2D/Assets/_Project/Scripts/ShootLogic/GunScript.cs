using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    public enum FireMode { Single, Auto, Charge }
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileForce = 20f;
    public int maxAmmo = 10;
    public float fireRate = 0.2f;
    public FireMode fireMode = FireMode.Single;
    public float chargeTime = 1.0f;
    public GameObject chargedProjectilePrefab;

    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isCharging = false;
    private float chargeStartTime = 0f;

    private PlayerInput playerInput;
    private InputAction fireAction;

    void Start()
    {
        currentAmmo = maxAmmo;
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
    }

    void Update()
    {
        if (fireMode == FireMode.Auto && fireAction.ReadValue<float>() > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
        else if (fireMode == FireMode.Single && fireAction.triggered)
        {
            Shoot();
        }
        else if (fireMode == FireMode.Charge)
        {
            if (fireAction.ReadValue<float>() > 0)
            {
                if (!isCharging)
                {
                    isCharging = true;
                    chargeStartTime = Time.time;
                }
            }
            else if (isCharging)
            {
                isCharging = false;
                if (Time.time - chargeStartTime >= chargeTime)
                {
                    ShootCharged();
                }
                else
                {
                    Shoot();
                }
            }
        }

        if (currentAmmo <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
            currentAmmo--;
        }
    }

    void ShootCharged()
    {
        if (currentAmmo > 0)
        {
            GameObject projectile = Instantiate(chargedProjectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileForce * 2, ForceMode2D.Impulse); 
            currentAmmo--;
        }
    }

    public void Reload(int ammo)
    {
        currentAmmo = Mathf.Min(currentAmmo + ammo, maxAmmo);
    }
}

