using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer),typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _cam;
    private Vector3 _mousePos;
    private TrailRenderer _trail;
    private BoxCollider _box;
    private bool _swiping = false;
    // Start is called before the first frame update
    void Awake()
    {
        _cam = Camera.main;
        _trail = GetComponent<TrailRenderer>();
        _box = GetComponent<BoxCollider>();
        _trail.enabled = false;
        _box.enabled = false;
        _gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsGameActive)
        {
            //if Input => swiping = true; else swiping = false; UpdateComponents()
            if (Input.GetMouseButtonDown(0))
            {
                _swiping = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _swiping= false;
            }
            UpdateComponents();
            if(_swiping) UpdateMousePosition();
          
        }
    }
    private void UpdateMousePosition()
    {
        _mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = _mousePos;
    }
    private void UpdateComponents()
    {
        _trail.enabled = _swiping;
        _box.enabled = _swiping;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TargetBehaviour>())
        {
            collision.gameObject.GetComponent<TargetBehaviour>().DestroyTarget();
        }
    }
}
