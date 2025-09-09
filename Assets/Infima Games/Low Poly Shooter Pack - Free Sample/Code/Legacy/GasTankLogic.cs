using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTankLogic : MonoBehaviour, IHaveProjectileReaction
{
    public void Reaction(Collision collision)
    {
        collision.transform.gameObject.GetComponent
            <GasTankScript>().isHit = true;
    }
}
