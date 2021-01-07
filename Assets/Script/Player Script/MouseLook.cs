using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool can_unlock=true;
    [SerializeField]
    private float sensivity = 5f;
    [SerializeField]
    private int smoth_Steps=10;
    [SerializeField]
    private float smooth_weigth = 0.04f;
    [SerializeField]
    private float role_Angle = 10f;
    [SerializeField]
    private float role_speed = 3f;

    [SerializeField]
    private Vector2 default_look_Limits=new Vector2(-70f, 80f);
    private Vector2 look_Angles;
    private Vector2 current_Mouse_look;
    private Vector2 smooth_Move;

    private float current_role_angle;

    private int last_look_frame;




    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LockAndUnlockCursor();

        if(Cursor.lockState==CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState==CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
    }


    void LookAround()
    {
        current_Mouse_look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        look_Angles.x += current_Mouse_look.x * sensivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_look.y * sensivity;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_look_Limits.x, default_look_Limits.y);
        current_role_angle = Mathf.Lerp(current_role_angle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * role_Angle, Time.deltaTime * role_speed);

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_role_angle);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    }
}
