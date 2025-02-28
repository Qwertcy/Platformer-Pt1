using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBlockHit : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No GameManager found in the scene. Make sure one exists.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Rock") || collision.collider.CompareTag("Question"))
        {
            if (rb.linearVelocity.y > 0f)
            {
                if (collision.collider.CompareTag("Rock"))
                {
                    Destroy(collision.collider.gameObject);
                }
 
                else if (collision.collider.CompareTag("Question"))
                {
                    if (gameManager != null)
                    {
                        gameManager.AddCoin();
                    }
                }
            }
        }
    }
}
