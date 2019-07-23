using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ReleaseTheBall : MonoBehaviour
{


    public Ground ground;

    public bool stopForcingBall;

    public bool castOnce;

    [SerializeField] private PlayerMove _playerMove;
    


    void Start()
    {
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        ground.IsMove = true;
        castOnce = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (!ground.IsMove)
        {
            transform.DOPause();
            _playerMove.stopForcingBall = stopForcingBall;
            castOnce = true;
        }
        else
        {
            if (castOnce)
            {
                transform.DOPlay();
                castOnce = false;
            }
        }
    }

}
