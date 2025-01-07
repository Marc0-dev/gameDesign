using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ostile : Entity
{
    private float damage;
    private float crit;
    public abstract void Move();
    public abstract void Attack();
}
