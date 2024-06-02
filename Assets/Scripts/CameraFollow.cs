using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject target; // Mục tiêu mà camera sẽ theo dõi

    // Offset giữa camera và target, bạn có thể điều chỉnh giá trị này trong Inspector
    private Vector3 offset;

    // Tốc độ chuyển động mượt mà của camera
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        // Tính toán offset dựa trên vị trí ban đầu của camera và target
        if (target != null)
        {
            offset = transform.position - target.transform.position;
        }
    }
    void LateUpdate()
    {
        // Kiểm tra nếu target không null
        if (target != null)
        {
            // Tính toán vị trí mong muốn của camera
            Vector3 desiredPosition = target.transform.position + offset;

            // Chuyển động mượt mà giữa vị trí hiện tại và vị trí mong muốn
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Cập nhật vị trí của camera
            transform.position = smoothedPosition;
        }
    }
}
