using UnityEngine;
using System.Collections;
using CnControls;

namespace ShooterAR
{
    public class PlayerController : MonoBehaviour
    {
        public Camera playerCamera;
        public Transform cameraPosition;
        public float speed = 2f;
        public float rotationBound = 25f;

        // Use this for initialization
        void Start()
        {
            
        }

        void FixedUpdate()
        {
            Rotation(CnInputManager.GetAxis("Horizontal"));        
        }

        void Rotation(float move)
        {
            Vector3 playerRot = transform.localRotation.eulerAngles;
            playerRot.y += (move * Time.deltaTime) * speed;
            
            playerRot.y = ClampAngle(playerRot.y, -45f, 45f);
            transform.localRotation = Quaternion.Euler (playerRot);
        }

        float ClampAngle (float angle, float min, float max)
        {
            if (angle < 90 || angle > 270)
            {
                if (angle > 180) angle -= 360;
                if (max > 180) max -= 360;
                if (min > 180) min -= 360;
            }

            angle = Mathf.Clamp(angle, min, max);
            if (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }
    }
}
