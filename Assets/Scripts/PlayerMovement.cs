using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f; // обычная скорость
    public float sprintSpeed = 35f; // скорость при беге
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;

    private bool isGrounded;
    private bool isInBoat = false;  // Флаг для проверки, в лодке ли игрок

    private BoatController boatController; // Ссылка на BoatController



    void Start()
    {
        boatController = FindObjectOfType<BoatController>();  // Находим объект с BoatController
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogSystem.Instance.dialogUIActive == false)
        {
            Movement();
         
        }
    }

    public void Movement()
    {
        // Проверка, коснулись ли мы земли
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isInBoat)
        {
            // Если игрок в лодке, его нельзя двигать с помощью CharacterController
            return;
        }

        // Получаем ввод для движения
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Проверяем, нажата ли клавиша для бега (например, Shift)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift); // или можно использовать любую другую клавишу

        // Настроим скорость в зависимости от того, бежим ли мы
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        // Вычисляем движение
        Vector3 move = transform.right * x + transform.forward * z;

        // Двигаем игрока с учётом скорости
        if (controller.enabled) // Проверяем, активен ли CharacterController
        {
            controller.Move(move * currentSpeed * Time.deltaTime);
        }

        // Проверка для прыжка
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Прыжок
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Применяем гравитацию
        velocity.y += gravity * Time.deltaTime;

        // Применяем гравитацию к движению
        if (controller.enabled) // Проверяем, активен ли CharacterController
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void EnterBoat()
    {
        // Когда игрок садится в лодку, отключаем CharacterController
        if (controller != null)
        {
            controller.enabled = false;  // Отключаем движение
        }

        isInBoat = true;  // Игрок в лодке
    }

    public void ExitBoat()
    {
        // Когда игрок выходит из лодки, включаем CharacterController
        if (controller != null)
        {
            controller.enabled = true;  // Включаем движение
        }

        isInBoat = false;  // Игрок не в лодке
    }
}
 