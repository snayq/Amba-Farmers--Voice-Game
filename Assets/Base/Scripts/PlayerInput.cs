using UnityEngine;
using UnityEngine.InputSystem;

// Processes player input.
public class PlayerInput : MonoBehaviour
{
    public Camera cam;
    public Movement movement;
    public Interact interact;
    public Inventory inventory;

    private Vector2 move_input;
    private bool moving;

    private void Start()
    {
        move_input = new Vector2();
        moving = false;
    }
    private void Update()
    {
        if (moving)
        {
            movement.Move(move_input, cam.transform.eulerAngles.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving = true;
            move_input = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moving = false;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var obj = interact.Use();
            //interact.Use(cam.ScreenPointToRay(Mouse.current.position.ReadValue()));

            if (obj != null)
            {
                inventory.Equip(obj);
            }
        }
    }
    public void OnToolUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.Tool.Use();
        }
    }
    public void OnItemUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.Item.Use();
        }
    }
    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.Open();
        }
    }
}
