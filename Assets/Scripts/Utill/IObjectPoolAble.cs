using System;

public interface IObjectPoolAble<T>
{
    public event Action<T> ReturnToPoolObject;
}
