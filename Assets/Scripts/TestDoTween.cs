using DG.Tweening;
using UnityEngine;

public class TestDoTween : MonoBehaviour
{
    [SerializeField]
    private Vector3[] points;

    // Start is called before the first frame update
    private void Start()
    {
        SquareSequence();
    }

    private void SquareSequence()
    {
        Sequence sequence = DOTween.Sequence(); // Tạo một chuỗi sequence mới

        // Vòng lặp qua từng điểm trong mảng và thêm tween vào sequence
        foreach (Vector3 point in points)
        {
            sequence.Append(transform.DOMove(point, 1f)); // Di chuyển đến điểm trong 1 giây
        }

        sequence.SetLoops(-1, LoopType.Restart); // Lặp lại vô hạn chuỗi

        sequence.Play(); // Bắt đầu chạy sequence
    }
}