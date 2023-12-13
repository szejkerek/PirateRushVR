using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlashEffect;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private InputActionReference shootInputLeft;
    [SerializeField] private InputActionReference shootInputRight;
    [SerializeField] private float shootForce;
    [SerializeField] private float shootCooldown = 0.25f; // Adjust the cooldown time as needed
    private float nextShootTime = 0f;
    private bool canShoot = true;

    private void OnEnable()
    {
        if (Systems.Instance.KatanaRight)
        {
            shootInputLeft.action.performed += Shoot;
        }
        else
        {
            shootInputRight.action.performed += Shoot;
        }
    }

    private void OnDisable()
    {
        shootInputLeft.action.performed -= Shoot;
        shootInputRight.action.performed -= Shoot;
    }

    private void Update()
    {
        if (!canShoot && Time.time >= nextShootTime)
        {
            canShoot = true;
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (!canShoot)
            return;

        Bullet newBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(shootingPoint.forward * shootForce, ForceMode.Impulse);

        var effect = Instantiate(muzzleFlashEffect, shootingPoint.transform);
        effect.transform.rotation = Quaternion.LookRotation(shootingPoint.forward);
        Destroy(effect, 8f);

        nextShootTime = Time.time + shootCooldown;
        canShoot = false;



    }
}
