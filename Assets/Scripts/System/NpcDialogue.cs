using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject _talkBalloon;

    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            _talkBalloon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            _talkBalloon.SetActive(false);
        }
    }
}
