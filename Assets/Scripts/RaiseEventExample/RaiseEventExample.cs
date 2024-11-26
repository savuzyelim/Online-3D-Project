using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventExample : MonoBehaviour
{
    private SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            changeColor();
        }
    }
    private void changeColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        sp.color = new Color(r, g, b, 1f);
    }
}
