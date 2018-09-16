using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridGlithFerryFX : MonoBehaviour
{
    public Texture nextTex;
    [Range(0f, 1f)]
    public float gridAlpha;
    [Range(0f, 1f)]
    public float noiseMapRampSlider;
    [Range(0f, 1f)]
    public float nextTextRampSlider;

    Texture mNoiseMap;

    Material mMaterial;
    Material Material
    {
        get
        {
            if (!mMaterial)
                mMaterial = new Material(Shader.Find("Hidden/Hont/GridGlithFerryFX"));

            return mMaterial;
        }
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mNoiseMap == null)
        {
            var texture2D = new Texture2D(256, 256);
            for (int y = 0; y < 256; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    var randomValue = UnityEngine.Random.value;
                    texture2D.SetPixel(x, y, new Color(randomValue, randomValue, randomValue));
                }
            }

            texture2D.Apply();

            mNoiseMap = texture2D;
        }

        Material.SetTexture("_NextTex", nextTex);
        Material.SetTexture("_NoiseMap", mNoiseMap);
        Material.SetFloat("_GridAlpha", gridAlpha);
        Material.SetFloat("_NoiseMapRampSlider", noiseMapRampSlider);
        Material.SetFloat("_NextTexRampSlider", nextTextRampSlider);
        Graphics.Blit(source, destination, Material);
    }
}
