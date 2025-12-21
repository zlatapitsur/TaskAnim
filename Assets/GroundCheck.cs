using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//public class GroundCheck : MonoBehaviour
//{
//    public bool isGrounded;
// 
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        isGrounded = true;
//    }
//
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        isGrounded = false;
//    }
//}
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded { get; private set; }

    private int groundContacts = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) == 0) return;

        groundContacts++;
        isGrounded = groundContacts > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) == 0) return;

        groundContacts = Mathf.Max(0, groundContacts - 1);
        isGrounded = groundContacts > 0;
    }
}