using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    [SerializeField] private Image batteryFill_Img;
    [SerializeField] private Image batteryHole_Img;

    [SerializeField] private GameObject batteryFill;
    [SerializeField] private GameObject batteryHole;

    private float currentFill = 1.0f;
    private readonly float step = 0.083f;
    private readonly float lowBatteryThreshold = 0.253f;
    private readonly Color normalColor = new Color(0.4418298f, 0.7433963f, 0.6312754f);
    private readonly Color lowColor = new Color(0.7960784f, 0.3529412f, 0.1294118f);

    private Coroutine batteryRoutine;

    public void StartBattery()
    {
        if (batteryRoutine != null)  StopCoroutine(batteryRoutine);

        batteryFill.SetActive(true);
        batteryHole.SetActive(true);
        batteryFill_Img.enabled = true;
        batteryHole_Img.enabled = true;
        
        batteryRoutine = StartCoroutine(BatteryDrainRoutine());
    }

    private IEnumerator BatteryDrainRoutine()
    {
        batteryFill_Img.color = normalColor;
        currentFill = 1.0f;
        batteryFill_Img.fillAmount = currentFill;

        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(1f);

            currentFill -= step;
            currentFill = Mathf.Max(currentFill, 0f);
            batteryFill_Img.fillAmount = currentFill;

            if (currentFill <= lowBatteryThreshold)
                batteryFill_Img.color = lowColor;
        }

        yield return StartCoroutine(BlinkAndShutOffRoutine());

        // 루틴 종료 후 null 처리
        batteryRoutine = null;
    }

    private IEnumerator BlinkAndShutOffRoutine()
    {
        float blinkTime = 3f;
        float interval = 0.2f;
        float elapsed = 0f;

        while (elapsed < blinkTime)
        {
            bool visible = Mathf.FloorToInt(elapsed / interval) % 2 == 0;
            batteryFill_Img.enabled = visible;
            batteryHole_Img.enabled = visible;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        batteryFill_Img.enabled = false;
        batteryHole_Img.enabled = false;

        batteryFill.SetActive(false);
        batteryHole.SetActive(false);
    }
}
