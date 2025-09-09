using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLogic : MonoBehaviour , IHaveProjectileReaction
{
    public void Reaction(Collision collision)
    {
        collision.transform.gameObject.GetComponent
            <TargetScript>().isHit = true;
    }
}
