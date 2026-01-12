using UnityEngine;

public class Draw : MonoBehaviour
{

    public Camera playerCamera;  // Камера игрока
    public GameObject paintPrefab;  // Префаб краски
    public float drawDistance = 5f;  // Расстояние, на котором можно рисовать
    public LayerMask wallLayer;  // Слой для стен

    private void Update()
    {
        // Проверяем, что игрок нажал левую кнопку мыши
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            // Raycast только на объекты, принадлежащие слою wallLayer
            if (Physics.Raycast(ray, out hit, drawDistance, wallLayer))
            {
                // Если мы попали в стену (или любой другой объект, входящий в слой wallLayer), рисуем
                Vector3 hitPoint = hit.point;
                Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                // Спавним префаб краски
                Instantiate(paintPrefab, hitPoint, hitRotation);
            }
        }
    }
}