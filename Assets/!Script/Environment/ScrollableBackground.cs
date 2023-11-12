using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScrollableBackground : MonoBehaviour
{
    private const float _BACKGROUND_DISTANCE_THRESHOLD = 6.75f;
    private const int _TEXTURE_SIZE_X = 54;
    private const int _TEXTURE_SIZE_Y = 32;


    private void Awake()
    {
        BoxCollider2D c = GetComponent<BoxCollider2D>();

        float xColliderSize = Camera.main.orthographicSize;
        c.size = new Vector2(xColliderSize, _TEXTURE_SIZE_Y);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        else
        {
            if (other.transform.position.x > transform.position.x)
            {
                transform.position += Vector3.right * _TEXTURE_SIZE_X;
                Debug.Log("MOVING RIGHT");
            }
            else
            {
                Debug.Log("MOVING LEFT");
                transform.position += Vector3.left * _TEXTURE_SIZE_X;
            }
        }
    }

/*    private void OnDrawGizmos()
    {
        foreach (Collider2D c in GetComponents<BoxCollider2D>())
        {
            Gizmos.color = new Color(0, 1, 0, 0.4f);
            Gizmos.DrawCube(c.bounds.center, c.bounds.size);
        }
    }*/
}

/*


    void Example()
    {
        _anythingLiterallyGrapes = 25; //is 25
        PassByCopy(_anythingLiterallyGrapes); //is 1
        Debug.Log(_anythingLiterallyGrapes); //still 25

        _anythingLiterallyGrapes = 32; //now its 32
        PassByReference(ref _anythingLiterallyGrapes); //is 5
        Debug.Log(_anythingLiterallyGrapes); //is now 5
    }

    void PassByCopy(float myFloat)
    {
        myFloat = 1;
        Debug.Log("By Copy: " + myFloat);
    }

    void PassByReference(ref float myFloat)
    {
        myFloat = 5;
        Debug.Log("By Reference: " + myFloat);
    }
    void GiveMeAnotherOne(out float newFloat)
    {
        newFloat = 12;
    }


    "ref" which passes REFERENCE, lets you change variable
    "out" returns a NEW variable
    NO KEYWORD creates a copy of passed in variable, DOES NOT CHANGE IT OUTSIDE SCOPE
*/

/*
    
- reference to the 4 background sprites
- to tile them, we need to know when to repeat the image
- individual scroll speeds for sprites
*/