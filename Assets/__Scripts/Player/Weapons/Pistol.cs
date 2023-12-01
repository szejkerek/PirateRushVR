using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab; // Prefab of the bullet object
    [SerializeField] private Transform shootingPoint; // Point from where bullets are shot
    [SerializeField] private InputActionReference shootInputLeft; // Input action for shooting (left)
    [SerializeField] private InputActionReference shootInputRight; // Input action for shooting (right)
    [SerializeField] private float shootForce; // Input action for shooting (right)

    private void OnEnable()
    {
        if(Systems.Instance.KatanaRight)
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

    private void Shoot(InputAction.CallbackContext context)
    {
        Bullet newBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(shootingPoint.forward * shootForce, ForceMode.Impulse);     
    }
}
