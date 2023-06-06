using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputControls inputActions;

    public float jup;


    private void Awake()
    {
        InitActions();
        BindActions();
    }

    private void InitActions()
    {
        inputActions = new InputControls();
        inputActions.Player.Enable();
    }


    private void BindActions()
    {
        inputActions.Player.Jump.performed +=(InputAction.CallbackContext ctx) => {
            Debug.Log("Press");
        };

        inputActions.Player.Jump.canceled += (InputAction.CallbackContext ctx) => {
            Debug.Log("Release");
        };
    }

    private void Update()
    {
        //jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);

        //jup = inputActions.Player.Move.ReadValue<Vector2>().y>0 ? 1.0f : 0 - inputActions.Player.Move.ReadValue<Vector2>().y < 0 ? 1.0f : 0;

        //jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);

        jup = inputActions.Player.Move.ReadValue<Vector2>().y;


        Debug.Log(inputActions.Player.Move.ReadValue<Vector2>());


        
        //Debug.Log(jup);


        
    }

}
