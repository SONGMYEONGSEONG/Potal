using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    public PlayerStatus status;
    private ObjectGrip objectGrip;

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

    [Header("Slope Handling")] //경사도에 올라갈시 위로 솟아오르는 현상 제어
    public float maxSlopeAngle; //해당 경사도 보다 작은경우 캐릭터가 올라갈수 있게 설정
    private RaycastHit slopeHit; //캐릭터 밑 경사도의 법선벡터 호출용으로 사용되는 변수


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        objectGrip = GetComponent<ObjectGrip>();
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
        ApplyCustomGravity();
    }

    private void LateUpdate()
    {
        Look();
        ExtraDirCheck();
    }

    private void ApplyCustomGravity()
    {
        // 중력을 수동으로 추가하여 y축 낙하를 더 강하게 설정
        Vector3 gravity = Physics.gravity * 3.0f;
        rigid.AddForce(gravity, ForceMode.Acceleration);
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
        float velocity_Y = rigid.velocity.y;

        if (OnSlope())
        {
            moveDirection = GetSlopeMoveDirection();
            velocity_Y = moveDirection.y * status.CurSpeed;
        }

        Vector3 moveVelocity = moveDirection * status.CurSpeed;

        rigid.velocity = new Vector3(moveVelocity.x, velocity_Y, moveVelocity.z) + ExtraDir;
    }

    private bool OnSlope() //플레이어 객체가 경사도에 있는지 체크 및 이동 가능한제 확인하는 함수
    {
        Ray ray = new Ray(GroundPivot.position, Vector3.down);
        //0.3f는 경사도에 있기에 추가로 더 길게 쏜것
        if (Physics.Raycast(ray, out slopeHit, 0.3f, GroundMask))
        {

            //두개의 벡터 사이의 각도값을 구하는 함수
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle <= maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        Vector3 reflectV = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        if (reflectV != Vector3.zero)
        {
            Debug.Log(reflectV);
        }
        //ProjectOnPlane : 투영된 벡터를 계산해주는 함수
        //Debug.Log($"투영벡터 : {reflectV} = {moveDirection} , {slopeHit.normal}");
        return reflectV;
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
