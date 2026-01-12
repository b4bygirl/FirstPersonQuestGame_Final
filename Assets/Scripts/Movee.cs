using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movee : MonoBehaviour
{

    private Camera playerCamera;
    private GameObject selectedObject = null;
    private float pickupDistance = 20f;
    private float moveSpeed = 5f;
    private float maxDistance = 5f;
    private bool isDragging = false;





    // Размеры пазла (фиксированные)
    private Vector3 puzzleSize = new Vector3(0.8f, 0.08f, 0.4f);

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {

        // Захват объекта
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupDistance))
            {
                if (hit.transform.CompareTag("MovableTwo"))
                {
                    selectedObject = hit.transform.gameObject;
                    isDragging = true;

                    Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                        rb.useGravity = false;
                    }
                }
            }
        }

        // Перемещение объекта
        if (isDragging && selectedObject != null)
        {
            MoveSelectedObject();
        }

        // Отпуск мышки
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null)
            {
                Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.velocity = Vector3.down * 0.1f; // лёгкий толчок вниз, чтобы избежать зависания

                    // Привязываем к сетке
                    Vector3 snappedPos = SnapToGrid(selectedObject.transform.position);
                    selectedObject.transform.position = snappedPos;
                }
            }

            isDragging = false;
            selectedObject = null;
        }
    }

    void MoveSelectedObject()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point;

            float distance = Vector3.Distance(playerCamera.transform.position, targetPosition);
            if (distance > maxDistance)
            {
                Vector3 direction = (targetPosition - playerCamera.transform.position).normalized;
                targetPosition = playerCamera.transform.position + direction * maxDistance;
            }

            // Не даём объекту провалиться под пол (можно убрать, если нужен свободный по высоте пазл)
            targetPosition.y = Mathf.Max(targetPosition.y, selectedObject.transform.position.y);

            selectedObject.transform.position = Vector3.Lerp(
                selectedObject.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        

        float x = Mathf.Round(position.x / puzzleSize.x) * puzzleSize.x;
        float y = position.y; // Можно зафиксировать высоту, если надо
        float z = Mathf.Round(position.z / puzzleSize.z) * puzzleSize.z;
        return new Vector3(x, y, z);
    }
}