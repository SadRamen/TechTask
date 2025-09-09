using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelLogic : MonoBehaviour, IHaveProjectileReaction
{
    public void Reaction(Collision collision)
    {
        collision.transform.gameObject.GetComponent
            <ExplosiveBarrelScript>().explode = true;
    }
}
