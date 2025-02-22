using UnityEngine;

namespace Managers
{
    
    [AddComponentMenu("Rocinante/PauseButton")]
    public class VfxManager : MonoBehaviour
    {

        //Particles must be child of this object
        private ParticleSystem _particle;
        void Start()
        {
            //CacheParticles();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            EventManager.Instance.ONPlayParticleHere += ONPlayParticleHere;
        }

        private void ONPlayParticleHere(Vector3 position)
        {
            _particle.transform.position = position;
            _particle.Play(true);
        }

        private void CacheParticles()
        {
            _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        }
        
    }
}
