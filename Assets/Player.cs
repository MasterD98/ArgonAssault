using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("ms^-1")][SerializeField]float xSpeed = 4f;
    [Tooltip("ms^-1")] [SerializeField] float xRange = 7f;
    [Tooltip("ms^-1")] [SerializeField] float ySpeed = 4f;
    [Tooltip("ms^-1")] [SerializeField] float yMin = -4.35f;
    [Tooltip("ms^-1")] [SerializeField] float yMax = 4.40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalTrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffsetThisFrame = horizontalThrow * Time.deltaTime*xSpeed;
        float yOffsetThisframe = verticalTrow * Time.deltaTime * ySpeed;

        float rawXPos = transform.localPosition.x + xOffsetThisFrame;
        float clampedXPos=Mathf.Clamp(rawXPos, -xRange, xRange);
        float rawYPos = transform.localPosition.y + yOffsetThisframe;
        float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
