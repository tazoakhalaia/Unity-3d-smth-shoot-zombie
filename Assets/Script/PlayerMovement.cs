using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private CharacterController controller;
    [SerializeField] public float speed = 500;

    [Header("Camera")]
    public Transform cam;
    private readonly float sensX = 100;
    private readonly float sensY = 100;
    private readonly float min = -25;
    private readonly float max = 50;
    private float xRotation = 0;

    [Header("Gravity")]
    private readonly float gravity = -200;
    private Vector3 velocity;
    [SerializeField] public float jumpHeight = 120;

    [Header("Bullet")]
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    private readonly int bulletSpeed = 5000;

    [Header("Gun")]
    public GameObject gun;

    [Header("Health")]
    public float health = 1;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        CameraMovement();
        GravityFalls();
        Shoot();

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpHeight;
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        Vector3 move = (forward * vertical + right * horizontal).normalized;
        controller.Move(speed * Time.deltaTime * move);
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, min, max);
        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gun.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void GravityFalls()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject spawnBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRb = spawnBullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
            }
            Destroy(spawnBullet, 3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 0.01f;
            HealthBar.instance.UpdateHealthBar(health);
        }
    }
}
