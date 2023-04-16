using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public delegate void PickUpFn();
    public PickUpFn pickUp = null;

    public delegate void PutDownFn(Vector3Int location);
    public PutDownFn putDown = null;

    public delegate Sprite GetSpriteFn();
    public GetSpriteFn getSprite = null;

    public void PickUp() {
        if (pickUp != null) {
            pickUp.Invoke();
        }
    }

    public void PutDown(Vector3Int location) {
        if (putDown != null) {
            putDown.Invoke(location);
        }
    }

    public Sprite GetSprite() {
        if (getSprite != null) {
            return getSprite.Invoke();
        } else {
            return null;
        }
    }
}
