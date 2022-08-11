using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSoliderPatrol : MonoBehaviour
{
    /// Patrol Point Limits
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    /// Enemy
    [SerializeField] private Transform enemy;

    /// Movement Paramaters
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    /// Stationary Behavior
    [SerializeField] private float idelDuration;
    private float idelTimer;


    /// Animators
    [SerializeField] private Animator anim;






    private void Awake()
    {
        initScale = enemy.localScale;

    }

    private void OnDisable() ///On disable destorys the player
    {
        anim.SetBool("moving_bugSolider", false);
 
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();

        }

        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving_bugSolider", false);
        idelTimer += Time.deltaTime;

        if (idelTimer > idelDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idelTimer = 0;
        ///anim.SetBool("moving_bugSolider", true);
        ///Enemy faces direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);
        ///Move in this direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
