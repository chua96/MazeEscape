using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private PickUp pickUp;
    private OpenDoor openDoor;
    [SerializeField] private Animator doorAnim;

    [SerializeField] private GameObject player;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject mission2;


    // Start is called before the first frame update
    private void Awake()
    {
        Instantiate(player, GameObject.FindWithTag("SpawnPoint").transform.position, GameObject.FindWithTag("SpawnPoint").transform.rotation);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickUp = GameObject.FindWithTag("Player").GetComponent<PickUp>();
        openDoor = GameObject.FindWithTag("Player").GetComponent<OpenDoor>();
        doorAnim = GameObject.FindWithTag("Door").GetComponent<Animator>();

        if (pickUp.treasurePicked)
        {
            text.text = "Done";
            text.color = Color.red;
            mission2.SetActive(true);
        }
        else
        {
            text.text = "In Progress";
            text.color = Color.black;
            mission2.SetActive(false);
        }

        if (openDoor.openDoor)
        {
            OpenStoneDoor();
        }
    }

    private void OpenStoneDoor()
    {
        doorAnim.SetBool("Open", true);
    }
    
}
