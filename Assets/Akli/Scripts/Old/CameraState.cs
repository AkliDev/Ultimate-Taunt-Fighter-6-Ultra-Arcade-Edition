using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICameraState
{
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();
}
public class CameraState : MonoBehaviour
{
    private ICameraState _State;
    [HideInInspector] public AudioSource _Audio;
    [HideInInspector] public Camera _Camera;
    public GameObject _Player1, _Player2;
    

    private void Awake()
    {
        _Audio = GetComponent<AudioSource>();
        _Camera = GetComponent<Camera>(); 
    }
    void Start()
    {
        _State = new Normal(this);
    }

    void Update()
    {
        _State.Update();
    }

    public void SwitchState(ICameraState state)
    {
        _State.OnExit();
        _State = state;
        _State.OnEnter();
    }
}

public class Normal : ICameraState
{
    CameraState _Camera;
    Vector3 _Position;
    float _ZoomSize;

    public Normal(CameraState camera)
    {
        _Camera = camera;

        _Position = new Vector3(0, 1, -10);
        _ZoomSize = 5;
    }
    public void OnEnter()
    {
       
    }

    public void Update()
    {
        _Camera.transform.position = Vector3.Lerp(_Camera.transform.position, _Position, 8f * Time.deltaTime);
        _Camera._Camera.orthographicSize = Mathf.Lerp(_Camera._Camera.orthographicSize, _ZoomSize, 8f * Time.deltaTime);
    }

    public void FixedUpdate()
    {

    }

    public void OnExit()
    {
        
    }
}

public class Rumble : ICameraState
{
    CameraState _Camera;
   
    float _Time, _Intensity;
    Vector3 _Preposition;
    Vector3 _AveregePosition;

    public Rumble(CameraState camera, float time, float intensity)
    {
        _Camera = camera;
        _Intensity = intensity;
        _Time = time;
    }

    public void OnEnter()
    {
        _Preposition = _Camera.transform.position;
        _Camera._Audio.Play();
        _AveregePosition = (_Camera._Player1.transform.position + _Camera._Player2.transform.position) * 0.5f;
        _Camera._Camera.orthographicSize = 3f;
    }

    public void Update()
    {
        _Time -= Time.deltaTime;
        _Intensity -= Time.deltaTime;

        _Camera.transform.position = _AveregePosition + new Vector3((Random.insideUnitCircle.x * _Intensity), (Random.insideUnitCircle.y * _Intensity), -10) + new Vector3(0, 2.5f, 0);
            
        if (_Time <= 0)
        {
            _Camera.SwitchState(new Normal(_Camera));
        }
    }

    public void FixedUpdate()
    {

    }

    public void OnExit()
    {
       // _Camera.transform.position = _Preposition;
    }
}