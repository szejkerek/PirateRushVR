using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] Sound explosionSound;
    [SerializeField] Sound fuseSound;
    [SerializeField] Sound flamePutDownSound;
    [Header("Effects")]
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject explosionEffect;
    [Header("Explosion")]
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;

    AudioSource fuseAudioSource;
    private void Awake()
    {
        fuseAudioSource = GetComponent<AudioSource>();
        AudioManager.Instance.Play(fuseAudioSource, fuseSound, SoundType.SFX);
    }
    private void Update()
    {
        fireEffect.transform.rotation = Quaternion.LookRotation(Vector3.up, transform.up);
    }

    public void Explode()
    {
        if (!gameObject.scene.isLoaded) return;

        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        foreach (var obj in projectiles)
        {
            if (Vector3.Distance(obj.transform.position, transform.position) > explosionRadius)
                continue;

            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
                continue;
            
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
        var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        AudioManager.Instance.PlayAtPosition(transform.position, explosionSound);
    }

    public void PutDownFire()
    {
        fireEffect.gameObject.SetActive(false);
        fuseAudioSource.Stop();
        AudioManager.Instance.PlayAtPosition(transform.position, flamePutDownSound);
    }
}
