using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

 class ProgressBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Image progressImage;
    [SerializeField] private Transform rotationPoint;


    private void Start()
    {
        enemy.OnHealthChanged += Enemy_OnHealthChanged;
        Hide();
    }

    private void Enemy_OnHealthChanged(float amount)
    {
        ChangeProgressBar(amount);
    }

     void ChangeProgressBar(float amount)
    {
        progressImage.fillAmount = amount;

        if(amount == 0f || amount == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void LateUpdate()
    {
        rotationPoint.rotation = Quaternion.FromToRotation(Vector3.up, Vector3.up);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
