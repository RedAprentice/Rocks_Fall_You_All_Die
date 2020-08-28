using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // might move hit() and heal() to utils class.
    public static void hit(ref int hp, int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public static void heal(ref int hp, int healing, int maxhp)
    {
        hp += healing;
        if (hp > maxhp)
        {
            hp = maxhp;
        }
    }
}
