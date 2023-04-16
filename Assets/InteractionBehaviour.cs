using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    public delegate void ActionFn(Vector3Int location);
    public ActionFn action = null;

    public void Action(Vector3Int location) {
        if (action != null) {
            action.Invoke(location);
        }
    }
}
