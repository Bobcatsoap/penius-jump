using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class Doodle : MonoBehaviour
{
    private float _x, _y;

    public float Speed;

    public bool IsJumping;

    private Rigidbody2D _rigidbody2D;

    private Image _playerImage;

    private Sprite _left, _right;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerImage = GetComponent<Image>();
        _left = Resources.Load<Sprite>("doodleL");
        _right = Resources.Load<Sprite>("doodleR");
    }

    // Update is called once per frame
    void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(_x, 0) * Speed);
        IsJumping = _rigidbody2D.velocity.y > 0;
        if (_x < 0)
        {
            _playerImage.sprite = _left;
        }
        else if (_x > 0)
        {
            _playerImage.sprite = _right;
        }
    }
}