using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a pistol with shooting capabilities.
/// </summary>
public class Pistol : MonoBehaviour
{
    [SerializeField] private Sound gunShotSound;
    [SerializeField] private GameObject muzzleFlashEffect;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private InputActionReference shootInputLeft;
    [SerializeField] private InputActionReference shootInputRight;
    [SerializeField] private float shootForce;
    [SerializeField] private float shootCooldown = 0.25f; // Adjust the cooldown time as needed

    private float nextShootTime = 0f;
    private bool canShoot = true;

    /// <summary>
    /// Subscribes to the correct shoot input action based on the equipped Sabre.
    /// </summary>
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

    /// <summary>
    /// Unsubscribes from shoot input actions.
    /// </summary>
    private void OnDisable()
    {
        shootInputLeft.action.performed -= Shoot;
        shootInputRight.action.performed -= Shoot;
    }

    /// <summary>
    /// Updates the shooting cooldown.
    /// </summary>
    private void Update()
    {
        if (!canShoot && Time.time >= nextShootTime)
        {
            canShoot = true;
        }
    }

    /// <summary>
    /// Initiates the shooting sequence upon receiving the shoot input action.
    /// </summary>
    /// <param name="context">The callback context of the shoot input.</param>
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

        AudioManager.Instance.PlayAtPosition(transform.position, gunShotSound);
    }
}
