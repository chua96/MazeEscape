using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OpenDoor : MonoBehaviour
{
    private PickUp pick;
    [SerializeField] private InputActionReference insert;
    [SerializeField] private GameObject UI;

    private bool OnDoor;
    public bool openDoor;

    private void OnEnable()
    {
        insert.action.Enable();
    }

    private void OnDisable()
    {
        insert.action.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        insert.action.performed += context =>
        {
            if (OnDoor && !openDoor && pick.treasurePicked)
            {
                openDoor = true;
                Destroy(UI);
            }
            else if (pick.treasurePicked)
            {
                Debug.Log("Key required");
            }
        };
    }

    private void Update()
    {
        pick = GetComponent<PickUp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            OnDoor = true;

            if (UI != null)
            {
                UI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            OnDoor = false;

            if (UI != null)
            {
                UI.SetActive(false);
            }
        }
    }
}
