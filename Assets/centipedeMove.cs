using UnityEngine;

public class centipedeMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int hitPoints = 1;
    public LevelManager levelMan;

    private Rigidbody2D rb;
    private Vector2 movement;
    private int currentHits = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    void Update()
    {
        RotateTowardsMovementDirection();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    private void ChangeDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        movement = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void RotateTowardsMovementDirection()
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    private void OnMouseDown()
    {
        currentHits++;
        if (currentHits >= hitPoints)
        {
            levelMan.TermiteClicked();
            Destroy(gameObject);
        }
    }
}
