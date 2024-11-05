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
    public Vector3 ExtraDir; //�ܺο��� ���� ���ҋ� x,z�� �̵����Ⱚ�� �������ִ� ����

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

    [Header("Slope Handling")] //��絵�� �ö󰥽� ���� �ھƿ����� ���� ����
    public float maxSlopeAngle; //�ش� ��絵 ���� ������� ĳ���Ͱ� �ö󰥼� �ְ� ����
    private RaycastHit slopeHit; //ĳ���� �� ��絵�� �������� ȣ������� ���Ǵ� ����


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        objectGrip = GetComponent<ObjectGrip>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���ӿ��� ���콺 Ŀ���� ����� �ϴ� �޼���
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
        // �߷��� �������� �߰��Ͽ� y�� ���ϸ� �� ���ϰ� ����
        Vector3 gravity = Physics.gravity * 3.0f;
        rigid.AddForce(gravity, ForceMode.Acceleration);
    }

    private void Look()
    {
        //��,�Ʒ� ȸ��
        curRotX += mouseDelta.y * LookSencitive;
        curRotX = Mathf.Clamp(curRotX, MinRotX, MaxRotX);
        cameraContainer.localRotation = Quaternion.Euler(-curRotX, 0f, 0f);

        //��,�� ȸ��
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

    private bool OnSlope() //�÷��̾� ��ü�� ��絵�� �ִ��� üũ �� �̵� �������� Ȯ���ϴ� �Լ�
    {
        Ray ray = new Ray(GroundPivot.position, Vector3.down);
        //0.3f�� ��絵�� �ֱ⿡ �߰��� �� ��� ���
        if (Physics.Raycast(ray, out slopeHit, 0.3f, GroundMask))
        {

            //�ΰ��� ���� ������ �������� ���ϴ� �Լ�
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
        //ProjectOnPlane : ������ ���͸� ������ִ� �Լ�
        //Debug.Log($"�������� : {reflectV} = {moveDirection} , {slopeHit.normal}");
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

    //�ܺο��� ���� ���� ���� ����� �ӵ� �������� 
    //���� ��°�� ���� �ʱ�ȭ�ϴ� �޼���
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
