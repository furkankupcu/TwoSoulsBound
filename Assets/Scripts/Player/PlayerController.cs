using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngineInternal;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    //Managers
    [SerializeField] GameManager GameManager;
    [SerializeField] PlayerInput PlayerInput;

    //Character
    [SerializeField] private float _speed = 4.0f;
    private Vector3 target;
    private Vector2 inputVector;

    //Animation
    private Animator m_Animator;

    private int m_DirXHash = Animator.StringToHash("DirX");
    private int m_DirYHash = Animator.StringToHash("DirY");
    private int m_SpeedHash = Animator.StringToHash("Speed");

    

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = PlayerInput.GetTargetHit();

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    target = new Vector3(inputVector.x, inputVector.y, transform.position.z);
                    StopAllCoroutines();
                    StartCoroutine(MoveToClickedPosition());
                }
            }
        }
    }

    private IEnumerator MoveToClickedPosition()
    {
        while ((transform.position - target).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void InputHandler()
    {
        inputVector = PlayerInput.GetTargetPosition();
    } 
}
