using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public ManagerSOScript managerSO;

    public GameObject paintingPercentageUI;

    public Text percentageText;
    public Text rankingText;
    // Start is called before the first frame update
    private void Start()
    {
        percentageText.text = "% " + 0;
        managerSO.PaintingWallActivationEvent += ActivatePercentageUI;
        managerSO.UpdatePaintingPercantageEvent += UpdatePercentageText;
        managerSO.UpdatePlayerRankingEvent += UpdateRankingText;
    }

    private void ActivatePercentageUI()
    {
        paintingPercentageUI.SetActive(true);
    }

    private void UpdatePercentageText(int percentage)
    {
        percentageText.text = "% " + percentage;
    }

    private void UpdateRankingText(int ranking)
    {
        rankingText.text = ranking.ToString();
    }

    private void OnDisable()
    {
        managerSO.PaintingWallActivationEvent -= ActivatePercentageUI;
        managerSO.UpdatePaintingPercantageEvent -= UpdatePercentageText;
    }

}
