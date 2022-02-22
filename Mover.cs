using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 acceleration;
    [SerializeField] private Vector2 limit;
    [SerializeField, Range(0f,1f)] private float friction;
    [SerializeField] private bool checkBounds;
    private bool _wall = false;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
        if (checkBounds) CheckBounds();
        velocity += acceleration * Time.deltaTime;
    }

    private void CheckBounds()
    {
        if (Mathf.Abs(transform.position.x)>limit.x && !_wall)
        {
            _wall = true;
            var position = transform.position;
            position = new Vector2(limit.x * Mathf.Sign(position.x), position.y);
            transform.position = position;
            velocity = new Vector2(Mathf.Abs(velocity.x)*-Mathf.Sign(velocity.x)*friction, velocity.y);
        }
        else if (Mathf.Abs(transform.position.y)>limit.y&& !_wall)
        {
            _wall = true;
            var position = transform.position;
            position = new Vector2(position.x, limit.y * Mathf.Sign(position.y));
            transform.position = position;
            velocity = new Vector2(velocity.x, Mathf.Abs(velocity.y)*-Mathf.Sign(velocity.y)*friction);
        }
        else
        {
            _wall = false;
        }
    }
}
