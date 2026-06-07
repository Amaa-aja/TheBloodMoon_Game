using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. LOGIKA JALAN
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveInput = 1f;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveInput = -1f;

        if (moveInput != 0f)
        {
            transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0), Space.World);
        }

        // 2. LOGIKA BALIK BADAN
        if (moveInput > 0 && !isFacingRight) Flip();
        else if (moveInput < 0 && isFacingRight) Flip();

        // 3. LOGIKA LOMPAT
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}