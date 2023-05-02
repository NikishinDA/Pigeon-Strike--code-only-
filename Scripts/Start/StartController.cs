using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class StartController : MonoBehaviour
{
    [Header("Pigeon")]
    [SerializeField] private Transform pigeonTransform;
    [SerializeField] private Animator pigeonAnimator;
    [Header("Landing")]
    [SerializeField] private Transform landPos;
    [SerializeField] private float landTime;

    [Header("Cameras")] 
    [SerializeField] private GameObject flyCamera;
    [SerializeField] private GameObject startCamera;
    [SerializeField] private GameObject playerCamera;

    [Header("Player")] [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerStart;

    [SerializeField] private float spawnTime;
    //[SerializeField] private ObiSoftbody playerSoftbody;

    public Transform PlayerStart => playerStart;

    private void Awake()
    {
        EventManager.AddListener<StartScenePlayEvent>(OnStartScenePlay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<StartScenePlayEvent>(OnStartScenePlay);

    }

    private void Start()
    {
    }

    private void OnStartScenePlay(StartScenePlayEvent obj)
    {
        StartCoroutine(PigeonLandCor());
        flyCamera.SetActive(false);
        startCamera.SetActive(true);
    }

    public void SetPlayer()
    {
        
        playerTransform.position = playerStart.position;
    }
    private IEnumerator PigeonLandCor()
    {
        Vector3 oldPos = pigeonTransform.position;
        Vector3 newPos = landPos.position;
        for (float t = 0; t < landTime; t += Time.deltaTime)
        {
            pigeonTransform.position = Vector3.Lerp(oldPos, newPos, t/landTime);
            yield return null;
        }
        pigeonAnimator.SetTrigger("Land");
        yield return new WaitForSeconds(0.1f);
        pigeonAnimator.SetTrigger("Poop");
        //playerSoftbody.enabled = true;
        for (float t = 0; t < spawnTime; t += Time.deltaTime)
        {
            //playerTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t/spawnTime);
            yield return null;
        } 
        playerTransform.gameObject.SetActive(true);

        pigeonAnimator.SetTrigger("FlyAway");
        startCamera.SetActive(false);
        var evt = GameEventsHandler.GameStartEvent;
        evt.PlayerHeight = playerTransform.position.y;
        EventManager.Broadcast(evt);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
