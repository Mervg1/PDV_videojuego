using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZoneLevel2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.transform.position = new Vector3(-64f, 0, 0);
            }
        }
    }
}
