using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("RedButton"))
        {
            RedButton redBtn = collision.collider.GetComponent<RedButton>();
            if (redBtn != null)
            {
                redBtn.Explosion();
            }
            ReturnToPool();
        }
        else if (collision.collider.CompareTag("DeleteObj"))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        PoolManager.Instance._bombPool.ReturnObj(this);
    }
}
