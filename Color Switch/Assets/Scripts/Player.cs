﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public int points = 0;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;

    [Header("Colors")]
    [SerializeField] private Color _cyan;
    [SerializeField] private Color _magenta;
    [SerializeField] private Color _purple;
    [SerializeField] private Color _yellow;


    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private string _currentColor;
    private bool _levelCompleted;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidBody.gravityScale = .1f;
        SetRandomColor();
        _levelCompleted = false;
    }



    void Update()
    {
        if (_levelCompleted) { return; }

        Movement();

        // If player goes out of the lower camera bounds
        if(transform.position.y < Camera.main.transform.position.y - 10f)
        {
            GameManager.Instance.ReloadLevel();
        }
    }


    private void Movement()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            _rigidBody.gravityScale = _gravity;
            _rigidBody.velocity = Vector2.up * _jumpForce;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_levelCompleted) { return; }

        if (other.tag == _currentColor)
        {
        }
        else if (other.tag == "RandomColorChanger")
        {
            points++;
            SetRandomColor();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Finish") {
            _levelCompleted = true;
            GameManager.Instance.NextLevel();
        }
        else
        {
            GameManager.Instance.ReloadLevel();
        }
    }



    private void SetRandomColor()
    {
        int colorIndex = Random.Range(0, 4);
        string tempColor = _currentColor;

        switch (colorIndex)
        {
            case 0:
                _currentColor = "Cyan";
                _spriteRenderer.material.color = _cyan;
                break;
            case 1:
                _currentColor ="Magenta";
                _spriteRenderer.material.color = _magenta;
                break;
            case 2:
                _currentColor ="Purple";
                _spriteRenderer.material.color = _purple;
                break;
            case 3:
                _currentColor ="Yellow";
                _spriteRenderer.material.color = _yellow;
                break;
        }

        if(tempColor == _currentColor)
        {
            SetRandomColor();
        }
    }

    
}
