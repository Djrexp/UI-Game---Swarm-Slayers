using System.Collections;
using System.Collections.Generic;
// using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class flyMove : MonoBehaviour
{
    public LevelManager levelMan;
    public float moveSpeed = 5f; // Speed of the fly's movement
    public float directionChangeInterval = 1f; // How often the fly changes direction
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            ChangeDirection();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void ChangeDirection()
    {
        // Generate a random direction by choosing a random angle
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        movement = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Optionally, randomize the speed to make movement more erratic
        float speed = Random.Range(moveSpeed * 0.5f, moveSpeed * 1.5f);
        rb.velocity = movement * speed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 10f)
        {
            // If the fly moves too far from the center, gently steer it back
            Vector3 directionToCenter = (Vector3.zero - transform.position).normalized;
            rb.velocity = Vector3.Lerp(rb.velocity, directionToCenter * moveSpeed, Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        // Logic for when the ant is clicked
        levelMan. AntClicked();
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
