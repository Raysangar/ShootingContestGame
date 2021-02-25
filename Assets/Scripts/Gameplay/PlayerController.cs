using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public System.Action OnOutOfProjectiles;

    public Weapon Weapon => weapon;

    public void Init(WeaponSettings[] weaponSettings)
    {
        weapon.Init(weaponSettings);
    }

    public void StartGame(int weaponIndex)
    {
        enabled = true;
        weapon.StartGame(weaponIndex);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        weapon.enabled = true;
    }

    private void OnDisable()
    {
        weapon.enabled = false;
    }

    private void Update()
    {
        UpdateMovement();
        UpdateAim();
    }

    private void UpdateMovement()
    {
        float movementForFrame = movementSpeed * Time.deltaTime;
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * movementForFrame, 0, Input.GetAxis("Vertical") * movementForFrame));
    }

    private void UpdateAim()
    {
        float rotationForFrame = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationForFrame);
        cameraRotation.x -= Input.GetAxis("Mouse Y") * rotationForFrame;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, minCameraPitch, maxCameraPitch);
        firstPersonCamera.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    [SerializeField] Weapon weapon;
    [SerializeField] Camera firstPersonCamera;
    [SerializeField] float maxCameraPitch;
    [SerializeField] float minCameraPitch;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    private Vector3 cameraRotation;
    private WeaponSettings[] weaponSettings;
}
