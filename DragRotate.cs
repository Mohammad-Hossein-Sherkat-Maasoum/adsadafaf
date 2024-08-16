using UnityEngine;

public class DragRotate : MonoBehaviour
{
    public float rotationSpeed = 5f;  // سرعت چرخش مکعب
    public CameraController cameraController;  // مرجع به CameraController برای دسترسی به حالت زوم

    private Vector3 previousMousePosition;  // موقعیت قبلی ماوس برای محاسبه چرخش

    void Update()
    {
        // اگر دکمه چپ ماوس فشرده شده است و حالت زوم غیرفعال است
        if (Input.GetMouseButtonDown(0) && !cameraController.isZoomed)
        {
            previousMousePosition = Input.mousePosition;  // موقعیت فعلی ماوس را ذخیره کنید
        }

        // اگر دکمه چپ ماوس فشرده نگه داشته شده است و حالت زوم غیرفعال است
        if (Input.GetMouseButton(0) && !cameraController.isZoomed)
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;  // تغییر موقعیت ماوس نسبت به فریم قبلی

            // چرخاندن مکعب روبیک با توجه به حرکت ماوس
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.World);

            previousMousePosition = Input.mousePosition;  // به‌روز‌رسانی موقعیت قبلی ماوس
        }
    }
}
