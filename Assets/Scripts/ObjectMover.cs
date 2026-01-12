using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject selectedObject = null;
    private float pickupDistance = 5f; // Дистанция, на которой можно схватить объект
    private float moveSpeed = 5f; // Скорость перемещения
    private float maxDistance = 3f; // Максимальное расстояние, на котором объект может находиться от игрока
    private bool isDragging = false; // Флаг, указывающий, перетаскивается ли объект

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Проверка на захват объекта
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            // Если объект не выбран, пытаемся его выбрать
            if (selectedObject == null)
            {
                TrySelectObject();
            }
            // Если объект уже выбран, начинаем перетаскивание
            else
            {
                isDragging = true;
            }
        }

        // Если объект выбран и перетаскивается, перемещаем его
        if (isDragging && selectedObject != null)
        {
            MoveSelectedObject();
        }

        // Если отпустили кнопку мыши, убираем выбор
        if (Input.GetMouseButtonUp(0))
        {
            // Завершаем перетаскивание
            isDragging = false;
        }
    }

    // Попытаться захватить объект
    void TrySelectObject()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.transform.CompareTag("Movable"))  // Убедись, что объект имеет тег "Movable"
            {
                selectedObject = hit.transform.gameObject;
            }
        }
    }

    // Перемещать выбранный объект
    void MoveSelectedObject()
    {
        // Создаем луч, направленный вперед от камеры
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Проверяем, куда попадет луч
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Получаем позицию цели
            Vector3 targetPosition = hit.point;

            // Ограничиваем движение объекта, не давая ему выходить за пределы максимального расстояния от камеры
            float distance = Vector3.Distance(playerCamera.transform.position, targetPosition);
            if (distance > maxDistance)
            {
                // Нормализуем вектор и ограничиваем позицию на maxDistance
                Vector3 direction = (targetPosition - playerCamera.transform.position).normalized;
                targetPosition = playerCamera.transform.position + direction * maxDistance;
            }

            // Зафиксируем высоту объекта, чтобы он не улетал вверх/вниз, но также учитываем возможность поднятия
            targetPosition.y = Mathf.Max(targetPosition.y, selectedObject.transform.position.y);

            // Получаем Rigidbody объекта, если он есть
            Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Останавливаем скорость, чтобы избежать инерции
                rb.velocity = Vector3.zero;

                // Ограничиваем вращение
                rb.angularVelocity = Vector3.zero;

                // Перемещаем объект с помощью Rigidbody (используем MovePosition)
                rb.MovePosition(Vector3.Lerp(selectedObject.transform.position, targetPosition, moveSpeed * Time.deltaTime));
            }
            else
            {
                // Если у объекта нет Rigidbody, используем обычное перемещение
                selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
}