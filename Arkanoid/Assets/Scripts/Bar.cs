using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bar : MonoBehaviour
{
    private float xLimit;
    private Vector3 initialMousePos;
    private Vector3 initialBarPos;
    private bool isDragging = false;

    private InputControls inputControls;

    private void Awake()
    {
        xLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - transform.localScale.x / 1.5f;

        inputControls = new InputControls();
        inputControls.Bar.Enable();

        inputControls.Bar.Click.started += ctx => StartDragging();
        inputControls.Bar.Click.canceled += ctx => StopDragging();
    }

    private void Update()
    {
        if (isDragging)
            HandleMouseMovement();
    }

    private void StartDragging()
    {
        isDragging = true;
        initialMousePos = GetMouseWorldPosition();
        initialBarPos = transform.position;
    }

    private void StopDragging()
    {
        isDragging = false;
    }

    private void HandleMouseMovement()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        float offsetX = mousePos.x - initialMousePos.x;
        Vector3 newPosition = initialBarPos + new Vector3(offsetX, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.nearClipPlane));
    }
}