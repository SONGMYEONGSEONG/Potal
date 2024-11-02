public class LeverSwitch : PuzzleInteractableObject
{
    public bool onSwitch = false; //나중에 private로 변경해야함

    public override void InterAction()
    {
        onSwitch = !onSwitch;

    }

    public override void Print()
    {

    }

}
