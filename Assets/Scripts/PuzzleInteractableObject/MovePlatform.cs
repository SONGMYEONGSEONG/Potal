using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatform : PuzzleInteractableObject
{
    private Rigidbody rigid;
    public float Speed;
    public List<Vector3> MovePaths;
    private int curPathStartIndex = 0;
    private int curPathEndIndex = 0;

    private float elispedTime = 0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        //�������� ����
        if (MovePaths.Count > 0)
        {
            transform.position = MovePaths[curPathStartIndex];
            curPathStartIndex = 0;
            curPathEndIndex = 1;
        }
        else
        {
            Debug.Log($"{gameObject.name}�� �̵���ΰ� �����ϴ�.");
        }
    }

    public void Start()
    {
        //������ �ܺ� �ε带 �̿��Ͽ� �����÷����� �̵���θ� �����Ұ� 
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Initialize(List<Vector3> paths)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            MovePaths.Add(paths[i]);
        }
    }

    private void Move()
    {
        elispedTime += Time.fixedDeltaTime;

        transform.position = Vector3.Lerp(MovePaths[curPathStartIndex], MovePaths[curPathEndIndex], Speed * elispedTime);

        if (Vector3.Distance(transform.position, MovePaths[curPathEndIndex]) < 0.1f)
        {
            if(curPathEndIndex + 1 == MovePaths.Count)
            {
                curPathEndIndex = 0;
            }
            else
            {
                curPathEndIndex++;
            }
            if(curPathStartIndex + 1 == MovePaths.Count)
            {
                curPathStartIndex = 0;
            }
            else
            {
                curPathStartIndex++;
            }

            elispedTime = 0f;
        }
    }

    /*Player ��ü �߰� ������ ���Ǵ� �ڵ� */
    //private void OnCollisionEnter(Collision collision)
    //{
    //    collision.transform.SetParent(transform);
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    collision.transform.SetParent(null);
    //}

    public override void InterAction()
    {

    }

    public override void Print()
    {

    }
}
