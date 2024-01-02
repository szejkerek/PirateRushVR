using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Partial class representing the Projectile behavior.
/// </summary>
public partial class Projectile : MonoBehaviour
{
    /// <summary>
    /// Retrieves the data associated with the projectile.
    /// </summary>
    public ProjectileSO Data => data;
    ProjectileSO data;

    /// <summary>
    /// Indicates if the points have been changed.
    /// </summary>
    public bool PointsChanged => pointsChanged;
    bool pointsChanged = false;

    Rigidbody rb;
    ConstantForce cForce;

    float aliveTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    private void Update()
    {
        aliveTime += Time.deltaTime;
        Despawn();
    }

    private void Despawn()
    {
        if (aliveTime >= 10f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the projectile with provided data, velocity, and gravity.
    /// </summary>
    /// <param name="data">Projectile data.</param>
    /// <param name="velocity">Initial velocity of the projectile.</param>
    /// <param name="gravity">Gravity to apply to the projectile.</param>
    public void Init(ProjectileSO data, Vector3 velocity, float gravity)
    {
        this.data = data;
        rb.velocity = velocity;
        cForce.force = new Vector3(0, gravity - Physics.gravity.y, 0);
        float randomSpinSpeed = Random.Range(10f, 50f);
        Vector3 randomSpinAxis = Random.onUnitSphere;
        rb.angularVelocity = randomSpinAxis * randomSpinSpeed;

        foreach (Transform t in transform)
        {
            t.gameObject.layer = LayerMask.NameToLayer("Projectile");
        }
    }

    /// <summary>
    /// Retrieves the cross-section material of the projectile.
    /// </summary>
    /// <returns>The cross-section material of the projectile.</returns>
    public Material GetCrossSectionMaterial()
    {
        if (data.OptionalData.CrossSectionMaterial == null)
        {
            Debug.LogWarning($"{gameObject.name} has no CrossSection material selected!");
            Material redMaterial = new Material(Shader.Find("Standard"));
            redMaterial.color = Color.red;
            return redMaterial;
        }

        return data.OptionalData.CrossSectionMaterial;
    }

    /// <summary>
    /// Applies hit effects to the projectile.
    /// </summary>
    /// <param name="critical">Indicates if the effect is critical.</param>
    public void ApplyEffects(bool critical)
    {
        data.MutualEffects.ForEach(e => e.ApplyHitEffect(this));

        GetComponent<Bomb>()?.Explode(); // If bomb, explode here

        if (critical)
        {
            ApplyCritical();
        }
    }

    private static void ApplyCritical()
    {
        AudioManager.Instance.PlayGlobal(AudioManager.Instance.SFXLib.CriticalStrike);
    }

    /// <summary>
    /// Applies points to the score based on certain conditions.
    /// </summary>
    /// <param name="negative">Indicates if the points are negative.</param>
    /// <param name="critical">Indicates if the points are critical.</param>
    public void ApplyPoints(bool negative = false, bool critical = false)
    {
        if (pointsChanged)
            return;

        float points;
        ScoreManager scoreManager = ScoreManager.Instance;

        if (data.OptionalData.AlwaysNegativePoints) negative = true;

        if (negative)
        {
            points = scoreManager.CalculatePoints(data.Points, isNegative: true);
            scoreManager.ResetMultiplier();
        }
        else
        {
            points = scoreManager.CalculatePoints(data.Points, isCritical: critical);
            scoreManager.IncrementMultiplier();
        }

        scoreManager.AddPoints(points);
        ScoreText text = Instantiate(scoreManager.ScoreText, transform.position, Quaternion.identity);
        text.Init(points, critical);

        pointsChanged = true;
    }
}
