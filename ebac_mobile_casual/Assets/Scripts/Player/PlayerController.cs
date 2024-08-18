using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lerp")]
    public Transform alvo;
    public float lerpVelocidade = 1f;


    public float velocidade = 1f;

    public string tagToCheck = "Enemy";
    public string tagEndLine = "EndLine";

    public GameObject endScreen;

    private bool _canRun;
    private Vector3 _pos;

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) return;

        _pos = alvo.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, _pos, lerpVelocidade * Time.deltaTime);
        transform.Translate(transform.forward * velocidade * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagToCheck))
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagEndLine)) EndGame();
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
    }
}
