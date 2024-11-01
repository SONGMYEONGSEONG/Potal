using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private ObjectPoolingContainer<PuzzleInteractableObject> puzzleInteractableObjectPool = new ObjectPoolingContainer<PuzzleInteractableObject>();
    public ObjectPoolingContainer<PuzzleInteractableObject> PuzzleInteractableObjectPool { get => puzzleInteractableObjectPool; }
}
