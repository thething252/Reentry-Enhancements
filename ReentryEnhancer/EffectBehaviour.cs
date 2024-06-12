using UnityEngine;

namespace KSPReentryEnhancer
{
    public class EffectBehaviour : MonoBehaviour
    {
        public float Intensity { get; set; }
        private ParticleSystem particleSystem;
        private Material reentryMaterial;

        private void Start()
        {
            if (particleSystem == null)
            {
                particleSystem = gameObject.AddComponent<ParticleSystem>();

                var main = particleSystem.main;
                main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.red);
                main.startSize = new ParticleSystem.MinMaxCurve(0.5f, 1.5f);
                main.startSpeed = new ParticleSystem.MinMaxCurve(5f, 10f);
                main.simulationSpace = ParticleSystemSimulationSpace.World;
                main.maxParticles = 1000;

                var emission = particleSystem.emission;
                emission.rateOverTime = 0;
                // Other configurations...
            }

            reentryMaterial = new Material(Shader.Find("Custom/ReentryHeat"));
        }

        private void Update()
        {
            var emission = particleSystem.emission;
            emission.rateOverTime = Intensity * 200; // Increase particle emission rate with intensity

            var main = particleSystem.main;
            main.startColor = Color.Lerp(Color.yellow, Color.red, Intensity);

            // Other updates...
        }
    }
}
