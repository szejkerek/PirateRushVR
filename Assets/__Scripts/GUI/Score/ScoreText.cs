/// <summary>
/// Handles the behavior of the score text displayed in the game.
/// </summary>
using UnityEngine;
using TMPro;
using System.Collections;
using System;

/// <summary>
/// Handles the behavior of the score text displayed in the game.
/// </summary>
public class ScoreText : MonoBehaviour
{
    /// <summary>
    /// Speed at which the text flies and fades.
    /// </summary>
    [SerializeField] float flySpeed;

    /// <summary>
    /// Duration of time before the text disappears.
    /// </summary>
    [SerializeField] float timerCount;

    [Space]
    /// <summary>
    /// Points at which the text reaches full color saturation.
    /// </summary>
    [SerializeField] float fullSaturationPoints;

    /// <summary>
    /// Color for positive points.
    /// </summary>
    [SerializeField] Color goodColor;

    /// <summary>
    /// Color for negative points.
    /// </summary>
    [SerializeField] Color badColor;

    /// <summary>
    /// Color for neutral points.
    /// </summary>
    [SerializeField] Color neutralColor;

    [Space]
    /// <summary>
    /// Text object for displaying the "Perfect!" message.
    /// </summary>
    [SerializeField] TMP_Text perfectText;

    /// <summary>
    /// Text object for displaying the points earned.
    /// </summary>
    [SerializeField] TMP_Text pointsText;

    float timer;
    Transform mainCamera;
    Color targetColor;
    float currentSaturation = 0;

    /// <summary>
    /// Initializes the score text with the given points and perfect status.
    /// </summary>
    /// <param name="points">The points earned.</param>
    /// <param name="isPerfect">Status indicating if it's a perfect score.</param>
    public void Init(float points, bool isPerfect)
    {
        perfectText.gameObject.SetActive(isPerfect);
        pointsText.gameObject.SetActive(true);
        SetPointsText(points);
        StartCoroutine(FlyAndFade());
    }

    private void Awake()
    {
        perfectText.gameObject.SetActive(false);
        pointsText.gameObject.SetActive(false);
        mainCamera = Camera.main.transform;
    }

    /// <summary>
    /// Sets the text of the points and adjusts its color based on the given points.
    /// </summary>
    /// <param name="points">The points to display in the text.</param>
    private void SetPointsText(float points)
    {
        // Set the text to display points with sign and round it to the nearest integer.
        pointsText.text = $"{DetermineSign(points)}{Mathf.CeilToInt(points)} points";

        // Calculate the saturation of color based on the points earned.
        currentSaturation = Mathf.Clamp01(Math.Abs(points) / Math.Abs(fullSaturationPoints));

        // Determine the color based on the points' sign.
        if (points > 0)
        {
            targetColor = goodColor;
        }
        else if (points < 0)
        {
            targetColor = badColor;
        }
        else
        {
            // Use neutral color for zero points and set saturation to full.
            targetColor = neutralColor;
            currentSaturation = 1;
        }

        // Apply the color to the points text with respect to saturation.
        pointsText.color = Color.Lerp(Color.white, targetColor, currentSaturation);
    }


    /// <summary>
    /// Determines the sign of the points and returns the corresponding string.
    /// </summary>
    /// <param name="points">The points to evaluate.</param>
    /// <returns>The sign of the points as a string ('+' for positive, empty for zero or negative).</returns>
    private string DetermineSign(float points)
    {
        if (points > 0)
        {
            return "+";
        }
        return "";
    }

    /// <summary>
    /// Coroutine controlling the flying and fading of the score text.
    /// </summary>
    private IEnumerator FlyAndFade()
    {
        perfectText.alpha = 1.0f;
        pointsText.alpha = 1.0f;
        timer = 0.0f;

        while (timer < timerCount)
        {
            MoveCanvas();

            RainbowShift(perfectText);
            FadeAway(perfectText);

            FadeAway(pointsText);

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Gradually shifts the color of the text through a rainbow spectrum.
    /// </summary>
    /// <param name="text">The text object whose color is to be shifted.</param>
    private void RainbowShift(TMP_Text text)
    {
        float initialHue = 0f;
        float hue = initialHue + (timer / timerCount * 360f);
        Color rainbowColor = Color.HSVToRGB(hue / 360f, 1f, 1f);
        text.color = rainbowColor;
    }

    /// <summary>
    /// Moves the canvas text towards the main camera and adjusts its rotation.
    /// </summary>
    private void MoveCanvas()
    {
        Vector3 directionToCamera = mainCamera.position - transform.position;

        transform.rotation = Quaternion.LookRotation(-directionToCamera);
        transform.Translate(Vector3.up * flySpeed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Fades away the given text gradually.
    /// </summary>
    /// <param name="text">The text object to fade away.</param>
    private void FadeAway(TMP_Text text)
    {
        text.alpha = Mathf.Lerp(1.0f, 0.0f, timer / timerCount);
    }
}
