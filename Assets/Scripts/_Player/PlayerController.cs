using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    public PlayerStatus status;

    [Header("Movement")]
    private Vector2 curMoveInput;
    private Vector3 moveDirection;
    public Vector3 ExtraDir; //외부에서 힘을 가할떄 x,z축 이동방향값을 보정해주는 변수

    [Header("Rotation")]
    public Transform cameraContainer;
    private Vector2 mouseDelta;
    public float MinRotX;
    public float MaxRotX;
    public float LookSencitive;
    private float curRotX;

    [Header("IsGrounded")]
    public float RayDistance;
    public Transform GroundPivot;
    public LayerMask GroundMask;

    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임에서 마우스 커서를 숨기게 하는 메서드
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

    }

    private void LateUpdate()
    {
        Look();
        ExtraDirCheck();
    }

    private void Look()
    {
        //위,아래 회전
        curRotX += mouseDelta.y * LookSencitive;
        curRotX = Mathf.Clamp(curRotX, MinRotX, MaxRotX);
        cameraContainer.localRotation = Quaternion.Euler(-curRotX, 0f, 0f);

        //좌,우 회전
        transform.localEulerAngles += new Vector3(0f, mouseDelta.x * LookSencitive, 0f);
    }

    public void Move()
    {
        moveDirection = curMoveInput.y * transform.forward + curMoveInput.x * transform.right;
        moveDirection *= status.CurSpeed;
        moveDirection.y = rigid.velocity.y;

        rigid.velocity = moveDirection + ExtraDir;
    }

    public void Jump(float JumpPower)
    {
        rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    public void Jump(Vector3 dir, float JumpPower)
    {
        rigid.AddForce(dir * JumpPower, ForceMode.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>(); ;
            ExtraDir = Vector2.zero;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    //외부에서 가한 힘에 의해 적용된 속도 보정값을 
    //땅에 닿는경우 값을 초기화하는 메서드
    private void ExtraDirCheck()
    {
        Ray ray = new Ray(GroundPivot.position + (GroundPivot.up * 0.01f), Vector3.down);

        if (Physics.Raycast(ray, 0.1f, GroundMask))
        {
            ExtraDir = Vector3.zero;
        }
    }

    private bool isGrounded()
    {
        Ray[] rays = new Ray[4]
         {
            new Ray(GroundPivot.position + (GroundPivot.forward * 0.2f) + (GroundPivot.up *0.01f), Vector3.down),
            new Ray(GroundPivot.position + (-GroundPivot.forward * 0.2f) + (GroundPivot.up *0.01f), Vector3.down),
            new Ray(GroundPivot.position + (GroundPivot.right * 0.2f) + (GroundPivot.up *0.01f), Vector3.down),
            new Ray(GroundPivot.position + (-GroundPivot.forward * 0.2f) + (GroundPivot.up *0.01f), Vector3.down),
         };


        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, GroundMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && status.CurStamina >= 10)
        {
            if (isGrounded())
            {
                Jump(status.CurJumpPower);
                status.CurStamina = Mathf.Max(0, status.CurStamina - 10);
            }
        }
    }
}
