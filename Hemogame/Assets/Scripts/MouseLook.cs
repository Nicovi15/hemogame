using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Parameters")]
    [SerializeField]
    float mouseSensitivityX = 100f;

    [SerializeField]
    float mouseSensitivityY = 100f;

    [Header("Player config")]
    [SerializeField]
    Transform playerTransform;

    float xRotation = 0f;

    [SerializeField]
    DialogueUI dialogueUI;

    [SerializeField]
    TransiMEP transi;

    [SerializeField]
    SettingsSave settings;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivityX = settings.camSen;
        mouseSensitivityY = settings.camSen;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen || !transi.isOut || Time.timeScale == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        

        playerTransform.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void updateSens()
    {
        mouseSensitivityX = settings.camSen;
        mouseSensitivityY = settings.camSen;
    }
}
