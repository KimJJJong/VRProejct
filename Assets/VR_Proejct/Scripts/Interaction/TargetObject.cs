using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public enum TargetType { Large, Medium, Small, Tiny, Minus_Large, Minus_Medium, Minus_Small, Heal, Blocker }

    public TargetType targetType;
    public float lifeTime = 1.5f;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Deactivate), lifeTime);

        switch (targetType)
        {
            case TargetType.Large:
                transform.localScale = Vector3.one * 2.0f;
                break;
            case TargetType.Medium:
                transform.localScale = Vector3.one * 1.5f;
                break;
            case TargetType.Small:
                transform.localScale = Vector3.one * 1.0f;
                break;
            case TargetType.Tiny:
                transform.localScale = Vector3.one * 0.6f;
                break;

            case TargetType.Minus_Large:
                transform.localScale = Vector3.one * 1.5f;
                break;
            case TargetType.Minus_Medium:
                transform.localScale = Vector3.one * 1.0f;
                break;
            case TargetType.Minus_Small:
                transform.localScale = Vector3.one * .5f;
                break;

            case TargetType.Heal:
                transform.localScale = Vector3.one * 1.3f;
                break;

            case TargetType.Blocker:
                transform.localScale = Vector3.one * 1.5f;
                break;
        }
    }

    public void OnHit(Vector3 hitPos)
    {

        AudioManager.Instance.PlaySFX("SFX_Smash");
        switch (targetType)
        {
            case TargetType.Large:
                ScoreManager.Instance.AddScore(10, hitPos);
                break;
            case TargetType.Medium:
                ScoreManager.Instance.AddScore(15, hitPos);
                break;
            case TargetType.Small:
                ScoreManager.Instance.AddScore(20, hitPos);
                break;
            case TargetType.Tiny:
                ScoreManager.Instance.AddScore(30, hitPos);
                break;
            case TargetType.Minus_Large:
                HealthManager.Instance.TakeDamage();
                Debug.Log("Hit Minus");
                break;
            case TargetType.Minus_Medium:
                HealthManager.Instance.TakeDamage();
                break;
            case TargetType.Minus_Small:
                HealthManager.Instance.TakeDamage();
                break;
            case TargetType.Heal:
                HealthManager.Instance.Heal();
                break;
            case TargetType.Blocker:
                ComboBlcokManager.Instance.EnvokeBlockEffect();
                break;
        }

        Destroy(gameObject); // or Return to Pool
    }


    void Deactivate() // or Return to Pool
    {
        gameObject.SetActive(false);
    }

}
