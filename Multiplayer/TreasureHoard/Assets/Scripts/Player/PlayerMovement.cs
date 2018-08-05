using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 inputTransform;
    private Vector3 inputRotate;
    private float sailSpeed = 15f;
    private float rotationSpeed = 30f;

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        inputTransform.x = 0f;
        inputTransform.y = 0f;
        inputTransform.z = CrossPlatformInputManager.GetAxis("Vertical");

        inputRotate.x = 0f;
        inputRotate.y = CrossPlatformInputManager.GetAxis("Horizontal");
        inputRotate.z = 0f;

        transform.Translate(inputTransform * Time.deltaTime * sailSpeed);
        transform.Rotate(inputRotate * Time.deltaTime * rotationSpeed);
    }
}
