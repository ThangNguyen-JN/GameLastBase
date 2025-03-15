using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public Material glowMaterial;
    public Color glowColor = Color.cyan;
    public float intensity = 10f;

    private void Start()
    {
        if (glowMaterial != null)
        {
            glowMaterial.EnableKeyword("_EMISSION");
            glowMaterial.SetColor("_EmissionColor", glowColor * intensity);
        }
    }
}
