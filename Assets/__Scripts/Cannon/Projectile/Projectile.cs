using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Projectile : MonoBehaviour
{
    public ProjectileSO Data => data;
    
    ProjectileSO data;
    Rigidbody rb;
    ConstantForce cForce;
    bool pointsApplied = false;
    bool underWater = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    private void Update()
    {
        if (transform.position.y < Systems.Instance.waterLevel && !underWater)
        {
            underWater = true;
            if (data.ProjectileType == ProjectileType.Collectible)
                return;
            ScoreManager.Instance.DecrementMultiplier();
        }

        if (transform.position.y < Systems.Instance.minHeight)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(ProjectileSO data, Vector3 velocity, float gravity)
    {
        this.data = data;
        rb.velocity = velocity;
        cForce.force = new Vector3(0, gravity - Physics.gravity.y, 0);
    }

    public Material GetCrossSectionMaterial()
    {
        if (data.CrossSectionMaterial == null)
        {
            Debug.LogWarning($"{gameObject.name} has no CrossSection material selected!");
            Material redMaterial = new Material(Shader.Find("Standard"));
            redMaterial.color = Color.red;
            return redMaterial;
        }

        return data.CrossSectionMaterial;
    }

    public void ApplyEffects(bool critical)
    {
        data.MutualEffects.ForEach(e => e.ApplyHitEffect(this));

        if(critical)
        {
            data.CriticalEffect?.ApplyHitEffect(this);
        }
    }

    public void ApplyPoints(bool negative = false, bool critical = false)
    {
        if (pointsApplied)
            return;

        float points;
        ScoreManager scoreManager = ScoreManager.Instance;

        if (data.AlwaysNegativePoints) negative = true;

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

        pointsApplied = true;
    }
}
