using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform alvo;
    public float lerpVelocidade = 1f;


    public float velocidade = 1f;

    public string tagToCheck = "Enemy";
    public string tagEndLine = "EndLine";

    public GameObject endScreen;
    public bool invencible;

    [Header("Texto")]
    public TextMeshPro uiTextoPowerUp;

    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 5f;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) return;

        _pos = alvo.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, _pos, lerpVelocidade * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagToCheck))
        {
            if (!invencible)
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1f,.3f).SetRelative();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagEndLine))
        {
            if (!invencible) EndGame();
        }
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    #region PowerUpSpeedUp
    public void SetPowerUpText(string s)
    {
        uiTextoPowerUp.text = s;
    }
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    public void ResetSpeed()
    {
        _currentSpeed = velocidade;
    }
    #endregion

    #region Invencibilidade
    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }
    #endregion

    #region ChangeHeight
    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        //var p = transform.position;
        //p.y = _startPosition.y + amount;
        //transform.position = p;
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);a
        SetPowerUpText("VÔO!");
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        //var p = transform.position;
        //p.y = _startPosition.y;
        //transform.position = p;
        SetPowerUpText("");
        transform.DOMoveY(_startPosition.y, .5f);
    }
    #endregion

    #region Coins
    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion  
}
