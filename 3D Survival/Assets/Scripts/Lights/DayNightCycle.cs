using System;
using UnityEngine;

namespace Lights
{
    public class DayNightCycle : MonoBehaviour
    {
        [Range(0.0f, 1.0f)] [SerializeField] private float time;
        [SerializeField] private float fullDayLength;
        [SerializeField] private float startTime;
        [SerializeField] private float timeRate;
        [SerializeField] private Vector3 noon;

        [Header("Sun")] [SerializeField] private Light sun;
        [SerializeField] private Gradient sunColor;
        [SerializeField] private AnimationCurve sunIntensity;

        [Header("Moon")] [SerializeField] private Light moon;
        [SerializeField] private Gradient moonColor;
        [SerializeField] private AnimationCurve moonIntensity;

        [Header("Other Lighting")] [SerializeField]
        private AnimationCurve lightingIntensityMultiplier;

        [SerializeField] private AnimationCurve reflectionIntensityMultiplier;


        private void Start()
        {
            timeRate = 1.0f / fullDayLength;
            time = startTime;
        }


        private void Update()
        {
            time = (time + timeRate * Time.deltaTime) % 1.0f;
            UpdateLighting(sun, sunColor, sunIntensity);
            UpdateLighting(moon, moonColor, moonIntensity);

            RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
            RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
        }


        private void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve)
        {
            float intensity = intensityCurve.Evaluate(time);
            lightSource.transform.eulerAngles = noon * ((time - (lightSource == sun ? 0.25f : 0.75f)) * 4f);
            lightSource.color = gradient.Evaluate(time);
            lightSource.intensity = intensity;

            GameObject go = lightSource.gameObject;
            if (lightSource.intensity == 0 && go.activeInHierarchy) go.SetActive(false);
            else if (lightSource.intensity > 0 && !go.activeInHierarchy) go.SetActive(true);
        }
    }
}
