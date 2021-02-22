using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    // TODO work-out why sometime ship went too speed
    [Header("General")]
    [Tooltip("ms^-1")][SerializeField]float controlSpeed = 20f;
    [Tooltip("m")] [SerializeField] float xRange = 5f;
    [Tooltip("m")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;
    [Header("Screen-Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;
    [Header("Controller-Throw Based")]
    [SerializeField] float controllerPitchFactor = -5f;
    [SerializeField] float controllerRollFactor = -20f;
    float yThrow;
    float xThrow;
    bool isControlsEnable = true;
    
    // Start is called before the first frame update
    void Start()
    {        

    }

    // Update is called once per frame
    void Update()
    {
        if (isControlsEnable) {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire")) {
            SetActiveGuns(true);
        }
        else {
            SetActiveGuns(false);
        }
    }

    private void SetActiveGuns(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
             var emissionModule=gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
            
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y* positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controllerPitchFactor;
        float pitch =pitchDueToPosition + pitchDueToControlThrow ;

        float yaw = transform.localPosition.x*positionYawFactor;

        float roll = xThrow*controllerRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void OnPlayerDeath() {//called by string reference
        isControlsEnable = false;
    }
}
