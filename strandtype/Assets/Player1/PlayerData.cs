using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int lastFire;
    //public float[] firePosition;

    public PlayerData (WarmthBar player)
    {
        lastFire = player.lastFire;

        /*
        firePosition = new float[3];
        firePosition[0] = fire.transform.position.x;
        firePosition[1] = fire.transform.position.y;
        firePosition[2] = fire.transform.position.z;
        */
    }

}
