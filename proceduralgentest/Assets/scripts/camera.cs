using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public float mouse = 150f;

     float xrot = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse * Time.deltaTime;


        xrot -= mouseY;

        xrot = Mathf.Clamp(xrot, -90f,90f);

        transform.localRotation = Quaternion.Euler(xrot, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
