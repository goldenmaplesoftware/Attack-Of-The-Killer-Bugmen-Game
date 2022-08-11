using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexMainMenu : MonoBehaviour
{
  
    Vector2 StartPosition;
    [SerializeField] int moveModifier;

    private void Start()
    {
        StartPosition = transform.position;
    }

    private void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float positionX = Mathf.Lerp(transform.position.x, StartPosition.x + (pz.x * moveModifier), 2f * Time.deltaTime);
        float positionY = Mathf.Lerp(transform.position.y, StartPosition.y + (pz.y * moveModifier), 2f * Time.deltaTime);
        transform.position = new Vector3(positionX, positionY, 0);
    }
}
