public class LeverSwitch : PuzzleInteractableObject
{
    public bool onSwitch = false; //���߿� private�� �����ؾ���

    public override void InterAction()
    {
        onSwitch = !onSwitch;

    }

    public override void Print()
    {

    }

}
