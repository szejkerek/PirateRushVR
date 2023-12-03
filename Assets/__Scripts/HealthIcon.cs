using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcon : MonoBehaviour
{
    [SerializeField] Image normalImage;
    [SerializeField] Image greyScaleImage;

    private void Awake()
    {
        greyScaleImage.gameObject.SetActive(false);
    }

    public void Kill()
    {
        normalImage.gameObject.SetActive(false);
        greyScaleImage.gameObject.SetActive(true);
    }

    public void Restore()
    {
        normalImage.gameObject.SetActive(true);
        greyScaleImage.gameObject.SetActive(false);
    }
}
