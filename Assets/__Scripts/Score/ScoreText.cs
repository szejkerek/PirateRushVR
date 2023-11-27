using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class ScoreText : MonoBehaviour
{
    [SerializeField] float flySpeed;
    [SerializeField] float timerCount;
    [Space]
    [SerializeField] float fullSaturationPoints;
    [SerializeField] Color goodColor;
    [SerializeField] Color badColor;
    [SerializeField] Color neutralColor;
    [Space]
    [SerializeField] TMP_Text perfectText;
    [SerializeField] TMP_Text pointsText;

    float timer;
    Transform mainCamera;
    Color targetColor;
    float currentSaturation = 0;

    void Awake()
    {
        perfectText.gameObject.SetActive(false);
        pointsText.gameObject.SetActive(false);
        mainCamera = Camera.main.transform;
    }

    public void Init(float points, bool isPerfect)
    {
        perfectText.gameObject.SetActive(isPerfect);
        pointsText.gameObject.SetActive(true);
        SetPointsText(points);
        StartCoroutine(FlyAndFade());
    }

    private void SetPointsText(float points)
    {
        pointsText.text = $"{DetermineSign(points)}{Mathf.CeilToInt(points)} points";

        currentSaturation = Mathf.Clamp01(Math.Abs(points) / Math.Abs(fullSaturationPoints));
        if (points > 0)
        {
            targetColor = goodColor; 
        }
        else if(points < 0)
        {
            targetColor = badColor;
        }
        else
        {
            targetColor = neutralColor;
            currentSaturation = 1;
        }
        pointsText.color = Color.Lerp(Color.white, targetColor, currentSaturation);
    }

    private string DetermineSign(float points)
    {
        if(points > 0)
        {
            return "+";
        }
        return "";
    }

    IEnumerator FlyAndFade()
    {
        perfectText.alpha = 1.0f;
        pointsText.alpha = 1.0f;
        timer = 0.0f;
        

        while (timer < timerCount)
        {
            MoveCanvas();

            FadeAway(perfectText);



            FadeAway(pointsText);

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void MoveCanvas()
    {
        Vector3 directionToCamera = mainCamera.position - transform.position;

        transform.rotation = Quaternion.LookRotation(-directionToCamera);
        transform.Translate(Vector3.up * flySpeed * Time.deltaTime, Space.World);
    }

    private void FadeAway(TMP_Text text)
    {
        text.alpha = Mathf.Lerp(1.0f, 0.0f, timer / timerCount);
    }
}
