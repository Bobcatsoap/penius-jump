using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class Board : MonoBehaviour
{
    private GameObject _player;
    public float jumpForce;
    private BoxCollider2D _boxCollider2D;
    private RectTransform _playerRectTransform, _boardRectTransform;
    private float _playerY, _boardY;
    private bool _playerCanTrigger;

    private void Start()
    {
        _player = FindObjectOfType<Doodle>().gameObject;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerRectTransform = _player.GetComponent<RectTransform>();
        _boardRectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        _playerCanTrigger = false;
        GetComponent<Image>().color = Color.red;

        _boardY = _boardRectTransform.anchoredPosition.y;
        _playerY = _playerRectTransform.anchoredPosition.y;
        //当玩家在跳跃忽略碰撞

        if (!_player.GetComponent<Doodle>().IsJumping)
        {
            if (_playerY >= _boardY)
            {
                _playerCanTrigger = true;
                GetComponent<Image>().color = Color.white;
            }
        }
    }


    /// <summary>
    /// 销毁
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "destroyArea")
        {
            FindObjectOfType<BoardManager>().DestroyBoard(gameObject);
        }

        if (other.gameObject == _player)
        {
            if (_playerCanTrigger)
            {
                //对玩家施加弹力
                Rigidbody2D playerRigidbody2D = _player.GetComponent<Rigidbody2D>();
                playerRigidbody2D.velocity = Vector2.zero;
                //始终保持玩家在屏幕中央
                float halfOfScreenHeight = 360f;
                float jumpMulit = (Mathf.Abs(_playerY) - halfOfScreenHeight) / halfOfScreenHeight;


                playerRigidbody2D.AddForce(new Vector2(0, jumpForce * jumpMulit), ForceMode2D.Impulse);
                FindObjectOfType<BoardManager>().Move(gameObject);
                print(playerRigidbody2D.velocity);
            }
        }
    }
}