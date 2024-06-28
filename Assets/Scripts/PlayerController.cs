using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriveMode
{
    Manual,
    Automatic
};
public class PlayerController : MonoBehaviour
{
 
    public DriveMode mode;
    // Enum để lưu trạng thái đích đến của xe
    public enum TargetEnum
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    public float speed = 5f; // Tốc độ di chuyển của xe
    public float turnSpeed = 50f;
    public TargetEnum nextTarget; // Trạng thái đích đến của xe

    // Các vị trí đích đến tương ứng với mỗi TargetEnum
    private Vector3 topLeftPosition = new Vector3(0f, 0.5f, 95f);
    private Vector3 topRightPosition = new Vector3(120f, 0.5f, 95f);
    private Vector3 bottomLeftPosition = new Vector3(120f, 0.5f, -23f);
    private Vector3 bottomRightPosition = new Vector3(0f, 0.5f, -23f);

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Lấy Rigidbody của xe
    }
    // Update is called once per frame
    void Update()
    {
        // Di chuyển xe tới vị trí đích đến
        if (mode == DriveMode.Automatic)
        {
            MoveToTarget();
        }else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            MoveByController();
        }
      
    }
    private void MoveByController()
    {

        // Điều khiển xe bằng tay
        Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        Vector3 rotation = Vector3.up * horizontalInput * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // Giới hạn vị trí của xe trong khu vực đường đua
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, -1.5f, 1.5f),
            rb.position.y,
            Mathf.Clamp(rb.position.z, -1.5f, 1.5f)
        );

    }
    private void MoveToTarget()
    {
        // Xác định vị trí đích đến dựa trên nextTarget
        Vector3 targetPosition = Vector3.zero;
        switch (nextTarget)
        {
            case TargetEnum.TopLeft:
                targetPosition = topLeftPosition;
                break;
            case TargetEnum.TopRight:
                targetPosition = topRightPosition;
                break;
            case TargetEnum.BottomLeft:
                targetPosition = bottomLeftPosition;
                break;
            case TargetEnum.BottomRight:
                targetPosition = bottomRightPosition;
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            NextTarget();
        }
    }
    private void NextTarget()
    {
        // Tăng nextTarget lên 1
        nextTarget = (TargetEnum)(((int)nextTarget + 1) % System.Enum.GetValues(typeof(TargetEnum)).Length);

        // Quẹo cửa khi đến điểm đích đến
        Vector3 currentPosition = transform.position;
        Debug.Log("TargetEnum.nextTarget " + nextTarget);
        switch (nextTarget)
        {
            case TargetEnum.TopLeft:
                RotateTowards(0f); // Quẹo phải
                break;
            case TargetEnum.TopRight:

                RotateTowards(90); // Quẹo phải
                break;
            case TargetEnum.BottomLeft:

                RotateTowards(180f); // Quẹo phải
                break;
            case TargetEnum.BottomRight:

                RotateTowards(-90f); // Quẹo phải
                break;
        }
    }
    private void RotateTowards(float angle)
    {
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}