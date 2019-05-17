using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.transform.position = new Vector3(590, 16, 0);
            }
        }
    }
}
