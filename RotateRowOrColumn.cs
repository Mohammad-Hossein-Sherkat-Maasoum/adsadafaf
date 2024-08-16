using UnityEngine;

public class RotateRowOrColumn : MonoBehaviour
{
    public Transform rowOrColumnParent; // اشاره به والد (parent) ردیف یا ستون
    public Vector3 rotationAxis; // محور چرخش (X, Y, Z)
    public float rotationAngle = 90f; // زاویه چرخش
    public float rotationSpeed = 10f; // سرعت چرخش

    private bool isRotating = false;
    private Quaternion targetRotation;

    void Update()
    {
        if (isRotating)
        {
            // چرخش والد (parent) تا رسیدن به زاویه مورد نظر
            rowOrColumnParent.localRotation = Quaternion.RotateTowards(rowOrColumnParent.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

            // بررسی اینکه آیا چرخش کامل شده است یا خیر
            if (Quaternion.Angle(rowOrColumnParent.localRotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }

    public void Rotate()
    {
        if (!isRotating)
        {
            isRotating = true;
            targetRotation = Quaternion.Euler(rotationAxis * rotationAngle) * rowOrColumnParent.localRotation;
        }
    }
}
