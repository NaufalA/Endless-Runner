using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJumpController : MonoBehaviour
{
    private Button _buttonJump;

    public CharacterMoveController characterController;
    private void Start()
    {
        _buttonJump = gameObject.GetComponent<Button>();
        _buttonJump.onClick.AddListener(characterController.Jump);
    }
}
