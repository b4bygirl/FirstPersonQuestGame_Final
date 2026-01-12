using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Скорость движения лодки
    public float turnSpeed = 100f;  // Скорость поворота лодки
    public float floatForce = 10f; // Сила, с которой лодка будет "плавать"
    public KeyCode enterKey = KeyCode.E;  // Клавиша для посадки на лодку

    private bool isInBoat = false;  // Флаг, чтобы понять, в лодке ли игрок
    private bool isInExitZone = false;


    private GameObject player;  // Игрок, который садится на лодку

    private Rigidbody boatRigidbody;  // Rigidbody лодки
    private CharacterController playerController;  // CharacterController игрока

    void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
        if (boatRigidbody == null)
        {
            Debug.LogError("Boat Rigidbody is missing!");
        }

        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }

        playerController = player.GetComponent<CharacterController>();  // Получаем CharacterController игрока

        // Устанавливаем значения для Rigidbody лодки
        if (boatRigidbody != null)
        {
            boatRigidbody.mass = 1f;  // Масса лодки
            boatRigidbody.drag = 0.5f;  // Сопротивление
            boatRigidbody.angularDrag = 0.05f;  // Угловое сопротивление
            boatRigidbody.useGravity = true;  // Включаем гравитацию для лодки
        }
    }

    void Update()
    {
        if (isInBoat)
        {
            // Управление движением лодки
            Vector3 movement = Vector3.zero;

            // Проверка, если нажата клавиша W (движение вперёд)
            if (Input.GetKey(KeyCode.W))
            {
                movement += transform.forward * moveSpeed;  // Двигаемся вперёд
            }
            // Проверка, если нажата клавиша S (движение назад)
            else if (Input.GetKey(KeyCode.S))
            {
                movement += -transform.forward * moveSpeed;  // Двигаемся назад
            }

            // Поворот лодки (при нажатии на A или D для поворота)
            float turnInput = Input.GetAxis("Horizontal");
            boatRigidbody.AddTorque(Vector3.up * turnInput * turnSpeed);

            // Применяем силу для движения лодки
            boatRigidbody.velocity = movement;  // Задаем скорость лодки

            // Лодка будет всегда "плавать" на воде, применяя силу вверх
            boatRigidbody.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);

            // ВЫХОД ИЗ ЛОДКИ ТОЛЬКО В ЗОНЕ ВЫХОДА
            if (Input.GetKeyDown(KeyCode.F) && isInExitZone)
            {
                ExitBoat();
            }
        }
        else
        {
            // Проверка, можно ли сесть на лодку (взаимодействие с лодкой)
            if (Vector3.Distance(player.transform.position, transform.position) < 2f && Input.GetKeyDown(enterKey))
            {
                EnterBoat();
            }
        }
    }

    private void EnterBoat()
    {
        if (player == null)
        {
            Debug.LogError("Player object is not assigned!");
            return;
        }

        isInBoat = true;
        player.transform.SetParent(transform);  // Привязываем игрока к лодке
        player.transform.position = transform.position + new Vector3(0, 1, 0);  // Помещаем игрока в лодку

        // Отключаем CharacterController, чтобы он не двигался отдельно от лодки
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Отключаем физику для игрока, чтобы не было конфликтов
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = true;
        }

        Debug.Log("Player entered the boat!");
    }

    private void ExitBoat()
    {
        isInBoat = false;
        player.transform.SetParent(null);  // Убираем игрока из лодки

        // Включаем CharacterController
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Включаем физику для игрока
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
        }

        player.transform.position = transform.position + new Vector3(0, 1, 0);  // Устанавливаем игрока на землю

        Debug.Log("Player exited the boat!");
    }

    // Триггер для зоны выхода
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitZone"))
        {
            isInExitZone = true;
            Debug.Log("Boat entered exit zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ExitZone"))
        {
            isInExitZone = false;
            Debug.Log("Boat left exit zone.");
        }
    }
}