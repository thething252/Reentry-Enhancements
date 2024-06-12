using System;
using UnityEngine;

namespace KSPReentryEnhancer
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ReentryEnhancer : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip reentrySound;
        private static Shader reentryShader;

        private void Start()
        {
            Debug.Log("[KSPReentryEnhancer] Reentry Enhancer Loaded");
            InitializeAudio();
            LoadShader();
        }

        private void Update()
        {
            ApplyReentryEffects();
        }

        private void InitializeAudio()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            reentrySound = Resources.Load<AudioClip>("ReentrySound");
            audioSource.clip = reentrySound;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        private void LoadShader()
        {
            reentryShader = Shader.Find("Custom/ReentryHeat");
            if (reentryShader == null)
            {
                Debug.LogError("[KSPReentryEnhancer] Reentry shader not found!");
            }
        }

        private void ApplyReentryEffects()
        {
            foreach (var vessel in FlightGlobals.Vessels)
            {
                if (vessel.mainBody == null) continue;

                if (vessel.mainBody.atmosphere)
                {
                    float reentryEffect = CalculateReentryEffect(vessel);
                    if (reentryEffect > 0)
                    {
                        EnhanceReentryVisuals(vessel, reentryEffect);
                        ApplyHeatGlow(vessel, reentryEffect);
                        PlayReentrySound(reentryEffect);
                    }
                    else
                    {
                        audioSource.Stop();
                    }
                }
            }
        }

        private float CalculateReentryEffect(Vessel vessel)
        {
            // Simplified calculation of reentry effect based on speed and altitude
            float altitude = (float)vessel.altitude;
            float speed = (float)vessel.srfSpeed;

            if (altitude < 70000 && speed > 1500)
            {
                return (speed - 1500) / 1000; // Example calculation
            }

            return 0;
        }

        private void EnhanceReentryVisuals(Vessel vessel, float reentryEffect)
        {
            // Implementation to enhance visual effects
        }

        private void ApplyHeatGlow(Vessel vessel, float reentryEffect)
        {
            // Implementation to apply heat glow effect
        }

        private void PlayReentrySound(float reentryEffect)
        {
            // Implementation to play reentry sound effect
        }
    }
}
