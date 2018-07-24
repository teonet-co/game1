using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

    private float dampX = 0.2f; //amount of camera offset on x
    private float dampY = 0.2f; //amount of camera offset on y
    float velocityX = 0f;
    float velocityY = 0f; 
    public Transform target;

    private GameObject cursor;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {

            Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position).normalized * 0.8f; //how to calculate vec3 direction and add in direction

            float offset = 0f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                offset = 7f;
            }

            Vector2 targetOffset = target.position + target.up * 0.5f * offset;

            float posX = Mathf.SmoothDamp(transform.position.x, targetOffset.x + mouseDir.x, ref velocityX, dampX);
            float posY = Mathf.SmoothDamp(transform.position.y, targetOffset.y + mouseDir.y, ref velocityY, dampY);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
