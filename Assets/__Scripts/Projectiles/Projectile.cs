using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Projectile : MonoBehaviour
{
    public ProjectileSO Data => data;
    ProjectileSO data;
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

    public void Init(ProjectileSO data, Vector3 velocity, float gravity)
    {
        this.data = data;
        rb.velocity = velocity;
        cForce.force = new Vector3(0, gravity - Physics.gravity.y, 0);
        float randomSpinSpeed = Random.Range(10f, 50f); 
        Vector3 randomSpinAxis = Random.onUnitSphere;
        rb.angularVelocity = randomSpinAxis * randomSpinSpeed;

        foreach(Transform t in transform)
        {
            t.gameObject.layer = LayerMask.NameToLayer("Projectile");
        }   
    }

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

    public void ApplyEffects(bool critical)
    {
        data.MutualEffects.ForEach(e => e.ApplyHitEffect(this));

        GetComponent<Bomb>()?.Explode(); //if bomb explode here

        if( critical )
        {
            ApplyCritical();
        }
    }

    private static void ApplyCritical()
    {
        AudioManager.Instance.PlayGlobal(AudioManager.Instance.SFXLib.CriticalStrike);
    }

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
