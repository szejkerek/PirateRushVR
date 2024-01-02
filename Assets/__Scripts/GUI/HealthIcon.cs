using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the health icon's behavior, toggling between normal and grayscale representations.
/// </summary>
public class HealthIcon : MonoBehaviour
{
    [SerializeField] Image normalImage; // The normal health image
    [SerializeField] Image greyScaleImage; // The grayscale health image

    private void Awake()
    {
        greyScaleImage.gameObject.SetActive(false); // Initially, set grayscale image as inactive
    }

    /// <summary>
    /// Sets the health icon to a grayscale.
    /// </summary>
    public void Kill()
    {
        normalImage.gameObject.SetActive(false); // Hide the normal image
        greyScaleImage.gameObject.SetActive(true); // Show the grayscale image
    }

    /// <summary>
    /// Restores the health icon to a normal.
    /// </summary>
    public void Restore()
    {
        normalImage.gameObject.SetActive(true); // Show the normal image
        greyScaleImage.gameObject.SetActive(false); // Hide the grayscale image
    }
}
