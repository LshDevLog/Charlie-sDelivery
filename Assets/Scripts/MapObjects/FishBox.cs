using System.Collections;
using UnityEngine;

public class FishBox : MonoBehaviour
{
    [SerializeField]
    private Fish.eDIRECTION _dir;

    [SerializeField]
    private float _delayTime;

    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_delayTime);
        StartCoroutine(CRT_LaunchFish());
    }


    private IEnumerator CRT_LaunchFish()
    {
        while (true)
        {
            Fish fish = PoolManager.Instance._fishPool.GetObj();
            if(fish != null)
            {
                fish.transform.position = transform.position;
                fish._dir = _dir;
            }

            yield return _delay;
        }
    }
}
