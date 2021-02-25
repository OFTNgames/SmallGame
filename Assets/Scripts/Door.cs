using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event System.Action ExitDoorReached = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>()) ExitDoorReached?.Invoke();
    }

}
