using UnityEngine;
public class Rock : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}