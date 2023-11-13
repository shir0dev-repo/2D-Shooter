using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScrollableBackground : MonoBehaviour
{
    private const int _TEXTURE_SIZE_X = 54;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        else
        {
            if (other.transform.position.x > transform.position.x)
            {
                transform.position += Vector3.right * _TEXTURE_SIZE_X;
            }
            else
            {
                transform.position += Vector3.left * _TEXTURE_SIZE_X;
            }
        }
    }
}