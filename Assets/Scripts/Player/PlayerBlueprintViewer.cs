using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlueprintViewer : MonoBehaviour
{
    [SerializeField] private GameObject blueprintScreen;
    [SerializeField] private GameObject blueprintButton;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator blueprintAnimator;

    [SerializeField] private bool isBlueprintOpen = false;

    [SerializeField] private AudioClip openBlueprintSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        blueprintScreen.SetActive(false);
        blueprintButton.SetActive(false);
    }

    private void Update()
    {
        if(gameManager.hasBlueprint)
        {
            blueprintButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.M) && !isBlueprintOpen)
            {
                StartCoroutine(OpenBlueprint());
            
            } else if(Input.GetKeyDown(KeyCode.M) && isBlueprintOpen)
            {
                StartCoroutine(CloseBlueprint());
            }
        }
    }

    public IEnumerator OpenBlueprint()
    {
        isBlueprintOpen = true;
        blueprintScreen.SetActive(true);

        blueprintAnimator.Play("Blueprint Fade In");

        PlayBlueprintSound();

        yield return new WaitForSeconds(0.3f);

        Time.timeScale = 0;

    }

    public IEnumerator CloseBlueprint()
    {
        blueprintAnimator.Play("Blueprint Fade Out");
        Time.timeScale = 1;

        Debug.Log("blueprint closing");

        PlayBlueprintSound();

        yield return new WaitForSeconds(0.3f);

        blueprintScreen.SetActive(false);
        isBlueprintOpen = false;
    }

    public void PlayBlueprintSound()
    {
        if (openBlueprintSound == null)
        {
            Debug.LogWarning("No blueprint sounds assigned to the item.");
            return;
        }

        audioSource.clip = openBlueprintSound;
        audioSource.Play();
    }
}
