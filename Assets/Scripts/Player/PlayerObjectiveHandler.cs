using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerObjectiveHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI objectiveText;

    [Header("Objective Icons")]
    [SerializeField] private Image securityTapeImage;
    [SerializeField] private Image securityTapeCheckMark;
    [SerializeField] private Image documentImage;
    [SerializeField] private Image documentCheckMark;
    [SerializeField] private Image journalImage;
    [SerializeField] private TextMeshProUGUI journalCountText;

    private Night currentNight;

    private void Start()
    {
        currentNight = gameManager.levelNumber;
        Debug.Log("Current night stored: " + currentNight);

        InitializeIcons();

        switch (currentNight)
        {
            case Night.Night1:
                // Get security footage
                securityTapeImage.gameObject.SetActive(true);

                objectiveText.text = "Malam 1 - Temukan rekaman CCTV";
                break;

            case Night.Night2:
                // Find secret documents
                documentImage.gameObject.SetActive(true);

                objectiveText.text = "Malam 2 - Carilah dokumen rahasia sekolah";
                break;

            case Night.Night3:
                // Get 7 pages;
                journalImage.gameObject.SetActive(true);

                objectiveText.text = "Malam 3 - Carilah 7 halaman jurnal hantu";
                break;
        }
    }

    private void Update()
    {
        switch (currentNight)
        {
            case Night.Night1:
                SecurityTapeCheck();
                break;

            case Night.Night2:
                DocumentCheck();
                break;

            case Night.Night3:
                JournalCountCheck();
                break;
        }
    }

    private void InitializeIcons()
    {
        securityTapeImage.gameObject.SetActive(false);
        securityTapeCheckMark.gameObject.SetActive(false);
        documentImage.gameObject.SetActive(false);
        documentCheckMark.gameObject.SetActive(false);
        journalImage.gameObject.SetActive(false);
        journalCountText.gameObject.SetActive(false);
    }

    private void SecurityTapeCheck()
    {
        if(gameManager.hasSecurityFootage)
        {
            securityTapeCheckMark.gameObject.SetActive(true);
        } 
    }

    private void DocumentCheck()
    {
        if(gameManager.hasDocuments)
        {
            documentCheckMark.gameObject.SetActive(true);
        }
    }

    private void JournalCountCheck()
    {
        journalCountText.gameObject.SetActive(true);
        journalCountText.text = gameManager.amountofPagesOwned + " / 7";
    }
}
