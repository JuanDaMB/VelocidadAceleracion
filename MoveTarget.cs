using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private Vector2 limit;
    [SerializeField] private Transform target;
    [SerializeField, Range(0f,1f)] private float friction;
    [SerializeField, Range(0f,100f)] private float limitSpeed;
    [SerializeField] private bool checkBounds, checkSpeed;
    private Vector2 _direction;
    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
        if (checkBounds) CheckBounds();
        var position = transform.position;
        _direction = (target.position - position).normalized;
        // Debug.DrawLine(position,position+(Vector3)_direction,Color.red);
        // Debug.DrawLine(position,position+(Vector3)velocity,Color.blue);
        velocity += _direction*(acceleration * Time.deltaTime);
        // Debug.DrawLine(position,position+(Vector3)velocity,Color.green);
        if (checkSpeed) LimitSpeed();
    }

    private void CheckBounds()
    {
        if (transform.position.x>limit.x)
        {
            velocity = new Vector2(-Mathf.Abs(velocity.x)*friction, velocity.y);
        }
        if (transform.position.y>limit.y)
        {
            velocity = new Vector2(velocity.x, -Mathf.Abs(velocity.y)*friction);
        }
        if (transform.position.x<-limit.x)
        {
            velocity = new Vector2(Mathf.Abs(velocity.x)*friction, velocity.y);
        }
        if (transform.position.y<-limit.y)
        {
            velocity = new Vector2(velocity.x, Mathf.Abs(velocity.y)*friction);
        }
    }

    private void LimitSpeed()
    {
        if (velocity.magnitude > limitSpeed)
        {
            velocity = velocity.normalized * limitSpeed;
        }
    }
}
