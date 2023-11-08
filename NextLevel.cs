using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class NextLevel : MonoBehaviour
{
    [SerializeField] private InputAction next;
    [SerializeField] private string MazeLevelToLoad;
    [SerializeField] private GameObject UI;

    private bool canProceed;
    private void OnEnable()
    {
        next.Enable();
    }

    private void OnDisable()
    {
        next.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        next.performed += context =>
        {
            if (canProceed)
            {
                SceneManager.LoadScene(MazeLevelToLoad);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canProceed = true;
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canProceed = false;
            UI.SetActive(false);
        }
    }
}
