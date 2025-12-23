using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // ziemia
    public bool isGrounded; // zmianna ferifikująca czy gracz stoi na ziemi

    private int groundContacts = 0; // zmianna o ilości obiektów ziemi które dotyka gracz

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // warunek czy obiekt, którego dotknęliśmy, jest ziemią
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContacts++;
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // warunek czy opuszczamy obiekt ziemi
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                groundContacts = 0;
                isGrounded = false;
            }
        }
    }
}
