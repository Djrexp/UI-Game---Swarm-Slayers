using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class antMove : MonoBehaviour
{
    public float moveForce = 1f;  
    public float changeDirectionTime = 1f;  
    public LevelManager levelMan;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float directionTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomizeMovement(); 
    }

    void Update()
    {
        directionTimer += Time.deltaTime;

        
        if (directionTimer >= changeDirectionTime)
        {
            directionTimer = 0f;
            RandomizeMovement();  
        }

        RotateInMovementDirection();
        HandleBoundaries();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveForce; 
    }

    private void RandomizeMovement()
    {
      
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        movement = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void RotateInMovementDirection()
    {
        if (movement != Vector2.zero) 
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void HandleBoundaries()
    {
        if (transform.position.x < -10 || transform.position.x > 10 || transform.position.y < -10 || transform.position.y > 10)
        {
           
            movement = -movement + Random.insideUnitCircle; 
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
