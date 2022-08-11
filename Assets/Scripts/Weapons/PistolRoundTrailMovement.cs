using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRoundTrailMovement : MonoBehaviour
{
    public int moveSpeed = 5;
    public void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*moveSpeed);
        ///Destroy(this.gameObject, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if(collision.gameObject.TryGetComponent<BoxBehavior>(out BoxBehavior boxComponet))
        {
            boxComponet.TakeDamage(1);

        }
        
        Destroy(this.gameObject, 1);
    }


}
