using Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField]
    private float _gravity = -10f;
    [SerializeField]
    private float _mass = 10f;
    [SerializeField]
    private SphereCollider _sphereCollider;

    public bool _dontAffectForce;

    [SerializeField]
    private Material _triggerMat;

    private Transform _player;
    private Transform _commet;
    private GameObject _trigger;
    private SpaceBodyControllerBase _spaceBodyControllerBase;

    private CoreManager _coreManager;

    private Vector3 _newDirection;

    private void Awake()
    {
        _spaceBodyControllerBase = GetComponentInParent<SpaceBodyControllerBase>();
    }

    private void Start()
    {
        _gravity += _mass / _gravity;
        _sphereCollider.radius = _gravity * -1;
        _trigger = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _trigger.transform.parent = transform.parent;
        _trigger.GetComponent<Renderer>().material = _triggerMat;
        _trigger.transform.position = transform.position;
        _trigger.transform.localScale = new Vector3(_sphereCollider.radius*2,0.1f, _sphereCollider.radius*2);
        _trigger.name = "Orbit Area";
        _trigger.SetActive(false);
        Destroy(_trigger.GetComponent<SphereCollider>());

        if (_spaceBodyControllerBase.Type != SpaceBodyControllerBase.SpaceBodyType.Commet && _spaceBodyControllerBase.Type != SpaceBodyControllerBase.SpaceBodyType.BlackHole)
        {
            _newDirection = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));

            transform.rotation = Quaternion.LookRotation(_newDirection);
        }

        _coreManager = CoreManager.Instance;
        _coreManager.OnIsDebug += HandlerIsDebug;        
    }
    public void HandlerIsDebug(object sender, EventArgs e)
    {
        _trigger.SetActive(!_trigger.activeSelf);
    }

    public void Attract(Transform body)
    {
        if(_player)
            Attract(body, _gravity, false, false);
    }

    public void Attract(Transform body, float customGravity, bool dontAffectForce, bool dontAffectRot, bool isPlayer = true)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;       

        Vector3 bodyUp = body.up;
        Quaternion targetRotation = Quaternion.identity;
        if (!dontAffectForce)
        {
            if (isPlayer)
            {
                float distance;
                if (_spaceBodyControllerBase.Type.Equals(SpaceBodyControllerBase.SpaceBodyType.BlackHole))
                {
                    distance = Vector3.Distance(body.position, transform.position);
                    Debug.Log("Distance " + distance);
                }

                body.GetComponent<PlayerController>().AddForce(targetDir * customGravity);
            }
            else
                body.GetComponent<Commet>().AddForce(targetDir * customGravity);
        }
        if (!dontAffectRot)
            targetRotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 1);
    }

    public void SetGravity(float force)
    {
        _gravity = force;
    }

    private void FixedUpdate()
    {

        if (_spaceBodyControllerBase.Type != SpaceBodyControllerBase.SpaceBodyType.Commet && _spaceBodyControllerBase.Type != SpaceBodyControllerBase.SpaceBodyType.BlackHole)
            transform.Rotate(0, (360 / (5)) * Time.deltaTime, 0, Space.Self);
       
        if (!_dontAffectForce)
            Attract(_player);

        if(_commet)
            Attract(_commet, _gravity, false, true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _dontAffectForce = false;
            _player = other.transform;
        }
        else if (other.gameObject.name.Contains("Commet"))
        {
            _commet = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _dontAffectForce = true;
            _player = null;
        }
        else if (other.gameObject.name.Contains("Commet"))
        {
            _commet = null;
        }
    }

    private void OnDestroy()
    {
        _coreManager.OnIsDebug -= HandlerIsDebug;
    }

}
