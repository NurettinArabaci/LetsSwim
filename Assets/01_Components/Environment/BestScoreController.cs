using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScoreController : MonoBehaviour
{
    [SerializeField] TextMeshPro bestScoreAmount;
    private int bestScore;

    private void Start()
    {
        bestScore = ScoreManager.HighScore;

        transform.position = bestScore > 0 ? Vector3.forward * bestScore+Vector3.up*10 : Vector3.forward*10000;

        bestScoreAmount.text = bestScore.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
