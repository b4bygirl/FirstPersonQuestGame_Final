using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f; // Максимальная дистанция для начала боя
    public KeyCode attackKey = KeyCode.F; // Клавиша для атаки
    public int attackDamage = 10; // Величина урона

    private void Update()
    {
        // Выбор врага и начало боя по левому клику мыши
        if (Input.GetMouseButtonDown(0)) // Левый клик мыши для выбора врага
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Enemy")) // Проверка, что объект - враг
                {
                    // Начало боя
                    hit.collider.GetComponent<EnemyScript>().StartFight();
                }
            }
        }

        // Атака по врагу при нажатии на клавишу (например, F)
        if (Input.GetKeyDown(attackKey)) // Клавиша атаки
        {
            // Проверим, если мы навели на врага и нажали атаку
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    
                    hit.collider.GetComponent<EnemyScript>().TakeDamage(attackDamage);
                }
            }
        }
    }
}