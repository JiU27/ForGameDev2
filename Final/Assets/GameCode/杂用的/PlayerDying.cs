using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDying : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Destroy(this.gameObject);
        }
    }
}
