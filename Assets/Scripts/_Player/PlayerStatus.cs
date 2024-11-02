using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] private float curSpeed;
    [SerializeField] private float curJumpPower;
    [SerializeField] private float speedMin;
    [SerializeField] private float jumpPowerMin;
    [SerializeField] private float speedMax;
    [SerializeField] private float jumpPowerMax;

    [Header("Condition Stats")]
    [SerializeField] private float healthMax;
    [SerializeField] private float staminaMax;
    [SerializeField] private float curHealth;
    [SerializeField] private float curStamina;

    public float HealthMax { get => healthMax; set => healthMax = value; }
    public float StaminaMax { get => staminaMax; set => staminaMax = value; }
    public float CurHealth { get => curHealth; set => curHealth = value; }
    public float CurStamina { get => curStamina; set => curStamina = value; }
    public float CurSpeed { get => curSpeed; set => curSpeed = value; }
    public float CurJumpPower { get => curJumpPower; set => curJumpPower = value; }
    public float SpeedMax { get => speedMax; set => speedMax = value; }
    public float JumpPowerMax { get => jumpPowerMax; set => jumpPowerMax = value; }
    public float SpeedMin { get => speedMin; set => speedMin = value; }
    public float JumpPowerMin { get => jumpPowerMin; set => jumpPowerMin = value; }

    private void Start()
    {
        //CharacterStatusSO characterData = GameManager.Instance.Player.statusSO;

        //curSpeed = characterData.Speed;
        //curJumpPower = characterData.JumpPower;
        //speedMax = characterData.SpeedMax;
        //jumpPowerMax = characterData.JumpPowerMax;
        //speedMin = characterData.SpeedMin;
        //JumpPowerMin = characterData.JumpPowerMin;

        //healthMax = characterData.HealthMax;
        //staminaMax = characterData.StaminaMax;
        //curHealth = characterData.Health;
        //curStamina = characterData.Stamina;

        //Debug
        curHealth = healthMax;
        curStamina = staminaMax;
    }

    //Add함수
    //Subtract함수
    //Orgint함수 필요함
}