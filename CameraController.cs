using UnityEngine;

// تعریف کلاس CameraController که از MonoBehaviour ارث‌بری می‌کند
public class CameraController : MonoBehaviour
{
    public Transform rubikCube;  // اشاره به آبجکت مکعب روبیک در صحنه
    public Vector3 normalPosition;  // موقعیت دوربین در حالت عادی
    public Vector3 zoomPosition;  // موقعیت دوربین در حالت زوم
    public Vector3 normalRotation;  // روتیشن (چرخش) دوربین در حالت عادی
    public Vector3 zoomRotation;  // روتیشن دوربین در حالت زوم
    public float transitionSpeed = 5f;  // سرعت تغییر موقعیت و روتیشن دوربین بین حالت‌ها
    public GameObject keys;  // اشاره به گیم‌آبجکت (GameObject) مربوط به دکمه‌های اطراف روبیک

    public bool isZoomed = false;  // پرچم (فلگ) برای تعیین حالت زوم دوربین

    void Update()
    {
        // بررسی اینکه آیا کاربر کلید C را فشار داده است تا وضعیت دوربین تغییر کند
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();  // تغییر وضعیت دوربین بین حالت عادی و زوم
        }

        // اگر دوربین در حالت زوم است
        if (isZoomed)
        {
            // تغییر موقعیت دوربین به سمت موقعیت زوم به صورت ملایم
            transform.position = Vector3.Lerp(transform.position, zoomPosition, transitionSpeed * Time.deltaTime);
            // تغییر روتیشن دوربین به سمت روتیشن زوم به صورت ملایم
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(zoomRotation), transitionSpeed * Time.deltaTime);

            // بررسی و گرد کردن زاویه‌های روتیشن مکعب روبیک به نزدیک‌ترین زاویه‌های معتبر
            Vector3 cubeRotation = rubikCube.rotation.eulerAngles;
            cubeRotation.x = RoundToNearestAngle(cubeRotation.x);
            cubeRotation.y = RoundToNearestAngle(cubeRotation.y);
            cubeRotation.z = RoundToNearestAngle(cubeRotation.z);
            rubikCube.rotation = Quaternion.Euler(cubeRotation);  // اعمال روتیشن گرد شده به مکعب روبیک

            // فعال کردن دکمه‌ها (گیم‌آبجکت keys) در حالت زوم
            if (keys != null && !keys.activeSelf)
            {
                keys.SetActive(true);
            }
        }
        else  // در صورتی که دوربین در حالت عادی باشد
        {
            // تغییر موقعیت دوربین به سمت موقعیت عادی به صورت ملایم
            transform.position = Vector3.Lerp(transform.position, normalPosition, transitionSpeed * Time.deltaTime);
            // تغییر روتیشن دوربین به سمت روتیشن عادی به صورت ملایم
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(normalRotation), transitionSpeed * Time.deltaTime);

            // غیرفعال کردن دکمه‌ها (گیم‌آبجکت keys) در حالت عادی
            if (keys != null && keys.activeSelf)
            {
                keys.SetActive(false);
            }
        }
    }

    // متدی برای تغییر وضعیت دوربین بین حالت عادی و زوم
    public void ToggleCamera()
    {
        isZoomed = !isZoomed;  // تغییر مقدار پرچم isZoomed به مقدار متضاد (True یا False)
    }

    // متدی برای گرد کردن زاویه به نزدیک‌ترین زاویه معتبر (0, 90, 180, 270, 360)
    private float RoundToNearestAngle(float angle)
    {
        float[] validAngles = { 0f, 90f, 180f, 270f, 360f };  // آرایه‌ای از زاویه‌های معتبر
        float nearestAngle = validAngles[0];  // فرض اولیه که نزدیک‌ترین زاویه اولین مقدار آرایه است
        float minDistance = Mathf.Abs(angle - nearestAngle);  // فاصله مطلق زاویه فعلی از نزدیک‌ترین زاویه

        // حلقه برای یافتن نزدیک‌ترین زاویه معتبر
        foreach (float validAngle in validAngles)
        {
            float distance = Mathf.Abs(angle - validAngle);  // محاسبه فاصله مطلق زاویه فعلی از زاویه معتبر فعلی
            if (distance < minDistance)  // اگر این فاصله کمتر از فاصله قبلی است
            {
                minDistance = distance;  // به‌روز‌رسانی کمترین فاصله
                nearestAngle = validAngle;  // به‌روز‌رسانی نزدیک‌ترین زاویه
            }
        }

        return nearestAngle;  // بازگشت نزدیک‌ترین زاویه معتبر
    }
}
