using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { LEFT, RIGHT, UP, DOWN }
public class Pac : MonoBehaviour
{

    public Animator a;
    public Collider2D boxCollider;
    public bool lastLeftRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        bool hasMoved = false;
        if (!hasMoved && Input.GetKey(KeyCode.RightArrow))
        {
            hasMoved = tryMove(Direction.RIGHT);
        }
        if (!hasMoved && Input.GetKey(KeyCode.LeftArrow))
        {
            hasMoved = tryMove(Direction.LEFT);
        }
        if (!hasMoved && Input.GetKey(KeyCode.UpArrow))
        {
            hasMoved = tryMove(Direction.UP);
        }
        if (!hasMoved && Input.GetKey(KeyCode.DownArrow))
        {
            hasMoved = tryMove(Direction.DOWN);
        }



    }

    Vector2 nearestCenterPoint()
    {
        return Vector2.zero;
    }

    bool tryMove(Direction d)
    {
        //var nearestCenter = nearestCenterPoint();
        //bool canChangeDirection = true;
        //bool didChangeDirection = false;

        //if (lastLeftRight && d == Direction.UP || d == Direction.DOWN && canChangeDirection)
        //{
        //    didChangeDirection = true;
        //}
        //else
        //{
        //    return false;
        //}

        //if (!lastLeftRight && d == Direction.LEFT || d == Direction.RIGHT && canChangeDirection)
        //{
        //    didChangeDirection = true;
        //}
        //else
        //{
        //    return false;
        //}


        var originalPosition = transform.position;
        var speed = 1.0f;
        var directionVector = Vector3.zero;
        if (d == Direction.RIGHT)
        {
            directionVector = transform.right;
        }
        if (d == Direction.LEFT)
        {
            directionVector = -transform.right;
        }
        if (d == Direction.UP)
        {
            directionVector = transform.up;
        }
        if (d == Direction.DOWN)
        {
            directionVector = -transform.up;
        }

        transform.position += directionVector * Time.deltaTime * speed;

        var results = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, directionVector, new ContactFilter2D(), results, 0.5f);
        foreach(var r in results)
        {
            if (r.collider.gameObject.CompareTag("Wall")){
                transform.position = originalPosition;
                return false;
            }
        }


        return true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PacTrigHIT");
    }

}
