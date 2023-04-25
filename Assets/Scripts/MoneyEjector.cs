using DG.Tweening;
using UnityEngine;

public class MoneyEjector : MonoBehaviour
{
    [SerializeField] private Canvas _viewPort;
    [SerializeField] private MoneyEjectorConfig _config;

    private const int _jumpsCount = 1;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void ApplyDamageHandle(int damage)
    {
        int visibleMoneyCount = (int)(damage * _config.MoneyByDamage);

        for (int i = 0; i < visibleMoneyCount; i++)
        {
            Eject();
        }
    }

    private void Eject()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Random.insideUnitSphere * _config.Radius;

        startPosition = GetAnchoredPositionFromWorldPosition(startPosition, _camera, _viewPort);
        endPosition = GetAnchoredPositionFromWorldPosition(endPosition, _camera, _viewPort);

        EjectableMoney moneyPrefab = _config.MoneyPrefab;
        EjectableMoney money = Instantiate(moneyPrefab, startPosition, moneyPrefab.transform.rotation, _viewPort.transform);

        float delay = Random.Range(_config.MinDelay, _config.MaxDelay);

        money.transform.DOJump(endPosition, _config.JumpPower, _jumpsCount, _config.Duration).SetDelay(delay).SetEase(_config.Ease).OnComplete(() =>
        {
            JumpTweenCompleteHandle(money);
        });
    }

    private void JumpTweenCompleteHandle(EjectableMoney sender)
    {
        Debug.Log("Complete");
        Destroy(sender.gameObject);
    }

    public static Vector2 GetAnchoredPositionFromWorldPosition(Vector3 worldPostion, Camera camera, Canvas canvas)
    {
        Vector2 myPositionOnScreen = camera.WorldToScreenPoint(worldPostion);
        float scaleFactor = canvas.scaleFactor;
        return new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
    }
}