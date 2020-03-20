using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public static string GROUND = "Ground";
    public static string ENEMY = "Enemy";
    public static string PLAYER = "Player";
    public static string WEAPON = "Weapon";
    public static string PROJECTILE = "Projectile";

    public static bool IsPlayer(Collision2D collision)
    {
        if(collision != null)
        {
            return collision.gameObject.tag.Equals(Tags.PLAYER);
        } else
        {
            return false;
        }
    }
}
