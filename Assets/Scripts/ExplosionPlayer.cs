using UnityEngine;

public class ExplosionPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject ExplosionVfx = default;

    private void Start()
    {
        GameObject explosion = Instantiate(ExplosionVfx, transform.position, Quaternion.identity);
        Destroy(explosion, 1);
    }
}
