using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject CharacterSlots;

    public GameObject CharacterIllust;
    public Sprite[] CharacterIllusts;
    private Image CharacterIllust_Image;
    private Animator CharacterIllust_Animator;

    public Text CharacterName;
    public Text CharacterDescription;
    public string[] CharacterNames;
    public string[] CharacterDescriptions;

    private int CurrentCursor = 0;
    private int AnimationCursor = -1;

    public Toggle[] Slots;
    

    void Start()
    {
        CharacterIllust_Image = CharacterIllust.GetComponent<Image>();
        CharacterIllust_Animator = CharacterIllust.GetComponent<Animator>();

        int Count = 0;
        foreach (Transform child in CharacterSlots.transform)
        {
            Slots[Count] = child.GetComponent<Toggle>();
                Count++;
        }
    }
    

    void Update()
    {
        KeyInput();

        Slots[CurrentCursor].isOn = true;

        if (CurrentCursor != AnimationCursor)
        {
            CharacterIllust_Animator.Rebind();
            CharacterIllust_Image.sprite = CharacterIllusts[CurrentCursor];
            AnimationCursor = CurrentCursor;

            CharacterName.text = CharacterNames[CurrentCursor];
            CharacterDescription.text = CharacterDescriptions[CurrentCursor];
        }
    }

    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CurrentCursor > 0)
                CurrentCursor--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CurrentCursor < Slots.Length -1 && Slots[CurrentCursor + 1] != null)
                CurrentCursor++;
        }
    }

}
