using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 posicaoPassado;
    public float velocidade = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - posicaoPassado.x);
        }
        posicaoPassado = Input.mousePosition;
    }

    public void Move(float speed)
    {
        transform.position += Vector3.right * Time.deltaTime * speed * velocidade;
    }
}
