using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] float mouseRotationSpeed = 0.2f;
    [SerializeField] float keyboardRotationSpeed = 2f;

    [SerializeField] GameObject focalPoint;

    float screenX = Screen.width;
    float screenY = Screen.height;

    float yaw = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Mouse.current.position.x.value;
        float mouseY = Mouse.current.position.y.value;
        if (mouseX < 0 || mouseX > screenX || mouseY < 0 || mouseY > screenY)
            return;
    }

    public void RotateKeyboard(float value)
    {
        yaw += keyboardRotationSpeed * value;
        Rotate();
    }

    public void RotateMouse(float value)
    {
        yaw += mouseRotationSpeed * value;
        Rotate();
    }

    private void Rotate()
    {
        Quaternion rot = Quaternion.Euler(0, yaw, 0);
        focalPoint.transform.rotation = rot;
    }

}
