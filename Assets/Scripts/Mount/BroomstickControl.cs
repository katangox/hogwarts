using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomstickControl : MonoBehaviour
{
    public float curSpeed = 0.0f;
    public float maxSpeed = 25.0f;
    public float rotationSpeed = 0.05f;
		public Vector3 movement = new Vector3(0, 0, 0);
		public float horizontalInput = 0f;
		public float verticalInput = 0f;

    void Update()
    {
				Camera.main.GetComponent<CameraController>().mouseXSpeedMod = 1.5f;
				Camera.main.GetComponent<CameraController>().mouseYSpeedMod = 1.5f;
				Camera.main.GetComponent<CameraController>().isMounted = true;
        // Get user input for movement
        horizontalInput = Mathf.Lerp(horizontalInput, Input.GetAxis("Horizontal") * 0.6f, 0.1f);
        verticalInput = Mathf.Lerp(verticalInput, Input.GetAxis("Vertical"), 0.1f);

				if (Input.GetKey(KeyCode.LeftShift)) {
					curSpeed = Mathf.Lerp(curSpeed, maxSpeed + 35.0f, 0.02f);
					Camera.main.GetComponent<CameraController>().desiredDistance = Mathf.Lerp(Camera.main.GetComponent<CameraController>().desiredDistance, Camera.main.GetComponent<CameraController>().distance + 3, 0.005f);
				} else {
					curSpeed = Mathf.Lerp(curSpeed, maxSpeed, 0.005f);
					Camera.main.GetComponent<CameraController>().desiredDistance = Mathf.Lerp(Camera.main.GetComponent<CameraController>().desiredDistance, Camera.main.GetComponent<CameraController>().distance, 0.03f);
				}

				Debug.Log(curSpeed);

        // Calculate movement and rotation
        movement = new Vector3(horizontalInput, 0.0f, verticalInput) * curSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Make the broomstick always face the camera direction
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + transform.position - Camera.main.transform.position + new Vector3(0, 2, 0));
        }
    }

		void onDestroy(){
			Camera.main.GetComponent<CameraController>().mouseXSpeedMod = 5.0f;
			Camera.main.GetComponent<CameraController>().mouseYSpeedMod = 3.0f;
			Camera.main.GetComponent<CameraController>().isMounted = false;
		}
}
