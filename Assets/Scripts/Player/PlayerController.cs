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

    //Character
    [SerializeField] private float _speed = 4.0f;
    private Vector3 target;

    //Animation
    private Animator m_Animator;

    private int m_DirXHash = Animator.StringToHash("DirX");
    private int m_DirYHash = Animator.StringToHash("DirY");
    private int m_SpeedHash = Animator.StringToHash("Speed");

    [SerializeField] PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        target = transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = _playerInput.GetTargetHit();

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Vector3 mousePosition = _playerInput.GetTargetPosition();
                    target = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
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
}
