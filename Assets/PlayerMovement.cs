using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody для управления физикой
    public float dashDistance = 6f; // Расстояние рывка
    private bool isDashing = false; // Флаг для контроля рывка
    private Animator animator;
    public float speedDash=10f;
    public Manager gameManager;
    public Spawner spawner; // Ссылка на объект спавнера

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<Manager>();
        spawner = FindObjectOfType<Spawner>(); // Находим объект Spawner в сцене
    }

    void Update()
    {
        // Проверка на рывок по нажатию пробела
        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            animator.SetBool("IsFlying", true);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + transform.forward * dashDistance; // Перемещение вперед на 6 по Z

        // Перемещение к целевой позиции
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, dashDistance * Time.deltaTime*speedDash));
            yield return null;
        }

        animator.SetBool("IsFlying", false);
        rb.MovePosition(targetPosition); // Установка позиции в целевую
        isDashing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Получаем имя текущей активной сцены
            string currentSceneName = SceneManager.GetActiveScene().name;
            // Загружаем сцену заново
            //SceneManager.LoadScene(currentSceneName);
        }

        if (other.CompareTag("egg"))
        {
            // Получаем компонент EggInfo у яйца
            EggInfo eggInfo = other.GetComponent<EggInfo>();

            // Проверяем, какой ребенок был активен, и увеличиваем счет
            if (eggInfo != null)
            {
                if (eggInfo.activeChildIndex == 0)
                {
                    gameManager.score += 3; // Если ребенок 0, добавляем 3 к счету
                }
                else if (eggInfo.activeChildIndex == 1)
                {
                    gameManager.score += 2; // Если ребенок 1, добавляем 2
                }
                else if (eggInfo.activeChildIndex == 2)
                {
                    gameManager.score += 1; // Если ребенок 2, добавляем 1
                }
            }

            Destroy(other.gameObject); // Уничтожаем яйцо
            // Сообщаем спавнеру, что яйцо уничтожено
            spawner.EggDestroyed();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, если коллайдер имеет тег "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Получаем имя текущей активной сцены
            string currentSceneName = SceneManager.GetActiveScene().name;
            // Загружаем сцену заново
            //SceneManager.LoadScene(currentSceneName);
        }
    }
}