using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGlithFerry_Demo : MonoBehaviour
{
    public GridGlithFerryFX gridGlithFerryFX;
    public AnimationCurve curve1;
    public AnimationCurve curve2;


    IEnumerator Start()
    {
        gridGlithFerryFX.gridAlpha = 0f;
        gridGlithFerryFX.noiseMapRampSlider = 0f;
        gridGlithFerryFX.nextTextRampSlider = 0f;

        var beginTime = Time.time;
        for (float duration = 1f; Time.time - beginTime <= duration;)
        {
            yield return null;
        }

        beginTime = Time.time;
        for (float duration = 1.2f; Time.time - beginTime <= duration;)
        {
            var t = (Time.time - beginTime) / duration;
            t = curve1.Evaluate(t);

            gridGlithFerryFX.gridAlpha = Mathf.Lerp(0f, 1f, t);
            gridGlithFerryFX.noiseMapRampSlider = Mathf.Lerp(0f, 0.33f, t);

            yield return null;
        }

        beginTime = Time.time;
        for (float duration = 0.5f; Time.time - beginTime <= duration;)
        {
            var t = (Time.time - beginTime) / duration;

            gridGlithFerryFX.noiseMapRampSlider = Mathf.Lerp(0.33f, 0.55f, t);

            yield return null;
        }

        beginTime = Time.time;
        for (float duration = 1.8f; Time.time - beginTime <= duration;)
        {
            var t = (Time.time - beginTime) / duration;
            t = curve2.Evaluate(t);

            gridGlithFerryFX.noiseMapRampSlider = Mathf.Lerp(0.55f, 0.9f, t);
            gridGlithFerryFX.nextTextRampSlider = Mathf.Lerp(0f, 0.9f, t);
            gridGlithFerryFX.gridAlpha = Mathf.Lerp(1f, 0f, t);

            yield return null;
        }

        beginTime = Time.time;
        for (float duration = 0.6f; Time.time - beginTime <= duration;)
        {
            var t = (Time.time - beginTime) / duration;

            gridGlithFerryFX.noiseMapRampSlider = Mathf.Lerp(0.9f, 1f, t);
            gridGlithFerryFX.nextTextRampSlider = Mathf.Lerp(0.9f, 1f, t);

            yield return null;
        }

        gridGlithFerryFX.gridAlpha = 0f;
    }
}
