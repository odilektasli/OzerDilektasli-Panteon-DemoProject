using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public ManagerSOScript managerSO;

    public GameObject paintingPercentageUI;

    public Text percentageText;
    // Start is called before the first frame update
    private void Start()
    {
        percentageText.text = "% " + 0;
        managerSO.PaintingWallActivationEvent += ActivatePercentageUI;
        managerSO.UpdatePaintingPercantageEvent += UpdatePercentageText;
    }

    private void ActivatePercentageUI()
    {
        paintingPercentageUI.SetActive(true);
    }

    private void UpdatePercentageText(int percentage)
    {
        percentageText.text = "% " + percentage;
    }

    private void OnDisable()
    {
        managerSO.PaintingWallActivationEvent -= ActivatePercentageUI;
        managerSO.UpdatePaintingPercantageEvent -= UpdatePercentageText;
    }

}
