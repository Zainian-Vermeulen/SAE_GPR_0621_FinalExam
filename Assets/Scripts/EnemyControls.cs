using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using System;

public class EnemyControls : MonoBehaviour
{
    [SerializeField] private Transform _stomperTransform, _playerTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _eyeLidLeft, _eyeLidRight, _eyeLidShutL, _eyeLidShutR;
    [SerializeField] private TMP_Text _bossText;
    private bool isChasing = true;

    private float movementSpeed = 4f;
    private float maxChaseDistance = 10;
    private float minChaseDistance = 5;

    private Vector2 origalPos = new Vector2(120.5f, 6f);

    private Vector2 playerPosCurrent, bossStompPos, bossIdlePos;


 
    // Start is called before the first frame update
    void Start()
    {
        _bossText.enabled = false;
       
        // playerPosCurrent = new Vector2(_playerTransform.position.x, 6f);
         //bossStompPos = new Vector2(_stomperTransform.position.x, -0.4f);
         //bossIdlePos = new Vector2(_stomperTransform.position.x, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosCurrent = new Vector2(_playerTransform.position.x, _playerTransform.position.y);

        bossStompPos = new Vector2(_stomperTransform.position.x, -0.4f);
        bossIdlePos = new Vector2(_stomperTransform.position.x, 6f);

        Vector3 screenPoint = _camera.WorldToViewportPoint(_stomperTransform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 0.9f && screenPoint.y > 0 && screenPoint.y < 1)
        {
            _eyeLidShutL.SetActive(false);
            _eyeLidShutR.SetActive(false);
            _bossText.enabled = true;

            //transform.LookAt(_playerTransform);

           
            BossMove();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, origalPos , movementSpeed * Time.deltaTime);
            _eyeLidShutL.SetActive(true);
            _eyeLidShutR.SetActive(true);
            _eyeLidLeft.SetActive(true);
            _eyeLidRight.SetActive(true);

            _bossText.enabled = false;
        }


    }

    private void BossMove()
    {

        //if (isChasing)
        //{
        //    StartCoroutine(BossMoveToPlayer());

        //}
        //else
        //{
        //   // StopCoroutine(BossMoveToPlayer());
        //    StartCoroutine(BossStomp());
        //}

        //if (Vector2.Distance(bossIdlePos, playerPosCurrent) <= 0.25f)
        //{
        //    StartCoroutine(BossStomp());
        //}
        /*else*/ if(Vector2.Distance(transform.position, _playerTransform.position) >= 0.9f)
        {
            if (isChasing)
            {
                StartCoroutine(BossMoveToPlayer());
            }
            StartCoroutine(BossStomp());
        }
    }
    private IEnumerator BossMoveToPlayer()
    {
       // if (Vector2.Distance(transform.position, _playerTransform.position) >= 0.9f)
       // {
            //isChasing = true;
            Debug.Log("player is in chasing distance");
             //Debug.Log("Distance is: " + Vector2.Distance(transform.position, _playerTransform.position));

        var playerPos = new Vector2(_playerTransform.position.x, 6);
            transform.position = Vector2.MoveTowards(transform.position, playerPos, movementSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0);
       // }
    }
    private IEnumerator BossStomp()
    {
         //if (Vector2.Distance(bossIdlePos, _playerTransform.position) <= 4f)

         if(bossIdlePos.x - playerPosCurrent.x <= 0.25f)
         {
            isChasing = false;


            _eyeLidLeft.SetActive(false);
            _eyeLidLeft.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            
            if (transform.position.y != bossStompPos.y)
            {
            Debug.Log("here1");
                transform.position = Vector2.MoveTowards(transform.position, bossStompPos, movementSpeed * Time.deltaTime);
            }
            //down
            Debug.Log("stoping");

            if (transform.position.y != bossStompPos.y)
            {
                //up
                yield return new WaitForSeconds(0.5f);
                transform.position = Vector2.MoveTowards(transform.position, bossIdlePos, movementSpeed * Time.deltaTime);
            Debug.Log("here2");
            }


            if (transform.position.y == bossIdlePos.y)
            {
            Debug.Log("here3");
                isChasing = true;
            }  
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == Level.Instance.PlayerTranform)
        {
            Debug.Log("Collided with enemy");
            Level.Instance.ResetLevel();

        }
    }
}