using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("Destination Xpos")]
    private float _leftXvalue;
    [SerializeField]
    private float _rightXvalue;
    [SerializeField]
    private float _duration;

    private void Start()
    {
        _leftXvalue = transform.position.x;
        StartCoroutine(AutoMove());
    }

    private IEnumerator AutoMove()
    {
        while (true)
        {
            yield return MoveTo(_rightXvalue);

            yield return MoveTo(_leftXvalue);
        }
    }

    private IEnumerator MoveTo(float targetX)
    {
        Tween tween = transform.DOMoveX(targetX, _duration).SetEase(Ease.Linear);

        yield return tween.WaitForCompletion();
    }
}
