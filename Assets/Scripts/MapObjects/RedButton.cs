using UnityEngine;

public class RedButton : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _explosionParticle;

    [SerializeField]
    private GameObject _underground;

    [SerializeField]
    private AudioClip _clip;

    public void Explosion()
    {
        if( _explosionParticle != null && _underground != null)
        {
            if(_underground.activeSelf)
            {
                SoundManager.Instance.PlaySfx(_clip);
                _explosionParticle.Play();
                _underground.SetActive(false);
            }
        }
    }
}
