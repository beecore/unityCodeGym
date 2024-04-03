using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Enum để lưu trạng thái đích đến của xe
    public enum TargetEnum
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    public float speed = 5f; // Tốc độ di chuyển của xe
    public TargetEnum nextTarget; // Trạng thái đích đến của xe

    // Các vị trí đích đến tương ứng với mỗi TargetEnum
    private Vector3 topLeftPosition = new Vector3(0f, 0.5f, 95f);
    private Vector3 topRightPosition = new Vector3(120f, 0.5f, 95f);
    private Vector3 bottomLeftPosition = new Vector3(120f, 0.5f, -23f);
    private Vector3 bottomRightPosition = new Vector3(0f, 0.5f, -23f);

    // Update is called once per frame
    void Update()
    {
        // Di chuyển xe tới vị trí đích đến
        MoveToTarget();
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
            Debug.Log(targetPosition);
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
