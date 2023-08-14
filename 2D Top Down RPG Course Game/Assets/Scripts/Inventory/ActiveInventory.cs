using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayerControls playerControls;

    private void Start() {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot( (int)ctx.ReadValue<float>() );     
    }

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue);
    }

    private void ToggleActiveHighlight(int index)
    {
        activeSlotIndexNum = index;
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
    
        this.transform.GetChild(index - 1).GetChild(0).gameObject.SetActive(true);
    }
}
