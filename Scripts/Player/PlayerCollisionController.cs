using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerFinisherEffectController ps;
    [SerializeField] private ParticleSystem smallPs;
    [SerializeField] private PlayerSizeManager playerSizeManager;
    [SerializeField] private float offset;
    private Collider _collider;
    private Rigidbody _rb;
    private bool _collided; 
    int _numOfPart = -1;
    [SerializeField] private P3dPaintDecal _currentDecal;
    [SerializeField] private float relativeSize = 3f;
    [Range(0f, 1f)] [SerializeField] private float deathPercentage; 

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
        EventManager.AddListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.AddListener<FinisherReplayEvent>(OnFinisherReplay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnObstacleHit);
       EventManager.RemoveListener<FinisherReplayEvent>(OnFinisherReplay);

    }

    private void OnFinisherReplay(FinisherReplayEvent obj)
    {

        _collider.enabled = true;
        _rb.isKinematic = true;
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        _rb.isKinematic = true;
    }


    private void OnObstacleHit(PlayerObstacleHitEvent obj)
    {
        Decal( Vector3.up,transform.position);
        Instantiate(smallPs, transform.position, Quaternion.identity);
        if (playerSizeManager.GetCurrentScale01() > deathPercentage) return;
        StartCoroutine(DelayedDisable(false));
    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        _rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_collided) return;
        _collider.enabled = false;
        _collided = true;
        ParticleSystem newPs =  Instantiate(ps, other.GetContact(0).point + Vector3.up * offset, Quaternion.identity).ParticleSystem;
        var newPsEmission = newPs.emission;
        if(_numOfPart <0 )
        {
            _numOfPart = newPsEmission.GetBurst(0).maxCount;
            _numOfPart = (int) (_numOfPart * playerSizeManager.GetRelativeScale(relativeSize));
        }
        newPsEmission.SetBurst(0, new ParticleSystem.Burst(0, _numOfPart));
        newPs.Play();
        StartCoroutine(DelayedDisable(true));
        Decal(other.contacts[0].normal, other.contacts[0].point);
    }

    private IEnumerator DelayedDisable(bool isWin)
    {
        yield return new WaitForFixedUpdate();
        //gameObject.SetActive(false);
        _collided = false;
        transform.localScale = Vector3.zero;
        if (!isWin)
        {
            var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = false;
            EventManager.Broadcast(evt);
        }
        else
        {
            EventManager.Broadcast(GameEventsHandler.FinisherPlayerReachGroundEvent);
        }
    }
    private void Decal(Vector3 normal, Vector3 point)
    {
        var preview = false;
        var priority = 0; // If you're painting multiple times per frame, or using 'live painting', then this can be used to sort the paint draw order. This should normally be set to 0.
        var pressure = 1.0f; // If you're using modifiers that use paint pressure (e.g. from a finger), then you can set it here. This should normally be set to 1.
        var seed = 0; // If this paint uses modifiers that aren't marked as 'Unique', then this seed will be used. This should normally be set to 0.
        var rotation = Quaternion.LookRotation(-normal); // Get the rotation of the paint. This should point TOWARD the surface we want to paint, so we use the inverse normal.
        _currentDecal.HandleHitPoint(preview, priority, pressure, seed, point, rotation);
    }
}

