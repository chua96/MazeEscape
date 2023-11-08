using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    private OpenDoor openDoor;
    [SerializeField] private Collider doorCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        openDoor = GameObject.FindWithTag("Player").GetComponent<OpenDoor>();

        if (openDoor.openDoor)
        {
            doorCollider.enabled = false;
        }
    }
}
