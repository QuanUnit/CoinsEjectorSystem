using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyEjectorConfig", menuName = "Data/MoneyEjectorConfig")]
public class MoneyEjectorConfig : ScriptableObject
{
    [field: SerializeField] public EjectableMoney MoneyPrefab { get; private set; }
    [field: SerializeField] [field: Min(0)] public float MoneyByDamage { get; private set; } = 1f;
    [field: SerializeField] [field: Min(0)] public float JumpPower { get; private set; } = 1f;
    [field: SerializeField] [field: Min(0)] public float Duration { get; private set; } = 2f;
    [field: SerializeField] [field: Min(0)] public float Radius { get; private set; } = 2f;
    [field: SerializeField] [field: Min(0)] public float MinDelay { get; private set; } = 0f;
    [field: SerializeField] [field: Min(0)] public float MaxDelay { get; private set; } = 0.2f;
    [field: SerializeField] [field: Min(0)] public Ease Ease { get; private set; }

    private void OnValidate()
    {
        MaxDelay = Mathf.Clamp(MaxDelay, MinDelay, MaxDelay);
    }
}