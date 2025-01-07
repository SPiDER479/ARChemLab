using UnityEngine;
public class Drop : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Liquid"))
            Destroy(gameObject);
    }
}