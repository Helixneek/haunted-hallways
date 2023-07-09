using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image cursorImage;
    [SerializeField] private AudioClip hoverSound;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        cursorImage.gameObject.SetActive(false);
        audioSource.clip = hoverSound;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorImage.gameObject.SetActive(true);
        animator.SetBool("isHovering", true);
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isHovering", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetBool("isPressed", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetBool("isPressed", false);
    }

    
}
