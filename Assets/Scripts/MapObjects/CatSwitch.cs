using UnityEngine;

public class CatSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _cat;

    [SerializeField]
    private AudioClip _clip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(SoundManager.Instance != null && _clip != null)
            {
                SoundManager.Instance.PlaySfx(_clip);
                _cat.SetActive(true);
            }
        }
    }
}
