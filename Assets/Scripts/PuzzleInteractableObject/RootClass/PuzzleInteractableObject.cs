using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임내의 퍼즐요소이며 상호작용을 할 수 있는 부모클래스
public abstract class PuzzleInteractableObject : MonoBehaviour, IObjectPoolAble<PuzzleInteractableObject>
{
    //해당 오브젝트 사용을 다한경우 해당 오브젝트 풀에 반납하는 변수
    public event Action<PuzzleInteractableObject> ReturnToPoolObject;

    public virtual void InterAction() { }
    public virtual void Print() {}

    private void Start()
    {
        ReturnToPoolObject += ObjectPoolManager.Instance.PuzzleInteractableObjectPool.PushObject;
    }

    public void DisableObject()
    {
        ReturnToPoolObject?.Invoke(this);
    }
}
