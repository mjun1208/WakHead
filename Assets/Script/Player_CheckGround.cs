using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CheckGround : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "SkyBlock")
        {
            player.IsGround = true;
            player.JumpCount = 0;
        }
        else
        {
            player.IsGround = false;
            player.JumpCount = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SkyBlock")
            player.IsGround = false;
    }
}
