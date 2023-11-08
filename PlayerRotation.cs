using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private Vector2 _MousePos;
    private Rigidbody rb;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(new Vector3(_MousePos.x, _MousePos.y, _cam.transform.position.y));

        transform.LookAt(mousePos + Vector3.up * transform.position.y);


    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        _MousePos = context.ReadValue<Vector2>();
    }
}
