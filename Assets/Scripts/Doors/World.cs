using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : SceenSwitch
{

    public Transform player;
    public float positionX;
    public float positionY;
    public string previous;

    public override void Start() 
    {

        base.Start();

        if (previousScene == previous) 
        {
            player.position = new Vector2(positionX, positionY);
        }
    
    }


}
