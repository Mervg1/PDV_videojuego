using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string level;
    public bool haveBrush;
    public bool haveGun;
    public float[] position;

    public PlayerData(Player player)
    {
        level = player.level;
        haveBrush = player.havebrush;
        haveGun = player.havePistol;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}
