using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Old
{
    public interface IState
    {
        void OnEnter();
        void HandleInput();
        void Update();
        void FixedUpdate();
        void OnCollisionEnter(Collision2D collision);
        void OnExit();
    }

    public enum AnimationState
    {
        Idle,
        Alt_Idle,
        Crouch,
        F_Walk,
        B_Walk,
        Jump,
        Falling,
        Sweep,
        Twist,
        Taunt
    }

    public class Sol : MonoBehaviour
    {
        public IState _State;
        public AnimationState _AnimationState;
        public Animator _Animator;
        public SpriteRenderer _Renderer;
        public Inputs _Inputs;
        public AudioSource _Audio;

        [SerializeField] private LayerMask _Opponenet;

        public float _MoveSpeed, _JumpForce, _GravityForce, _KnockBackForce, _WallBounceForce;
        public Vector2 _Velocity;

        private Rigidbody2D _RigidBody;
        private BoxCollider2D _Collider;

        [SerializeField] public bool _Grounded;
        [SerializeField] private LayerMask _LayerMask;

        public Vector3 _AttackOffset;
        public float _Radius;

        private CameraState _Camera;

        public bool _WallBounce, _PreWallBounce;

        public Vector3 _StartPosition;

        InputComponent m_InputComponent;
        public InputComponent InputComponent { get { return m_InputComponent; } }


        private void Awake()
        {
            _RigidBody = GetComponent<Rigidbody2D>();
            _Collider = GetComponent<BoxCollider2D>();
            _Animator = GetComponent<Animator>();
            _Renderer = GetComponent<SpriteRenderer>();
            _Inputs = GetComponent<Inputs>();
            _Camera = GameObject.Find("Camera").GetComponent<CameraState>();
            _Audio = GetComponent<AudioSource>();
            m_InputComponent = GetComponent<InputComponent>();
        }
        void Start()
        {
            _State = new Idle(this);
            _StartPosition = transform.position;

            if (_Renderer.flipX == true)
            {
                _WallBounceForce = -_WallBounceForce;
            }
            else
            {
                _KnockBackForce = -_KnockBackForce;
            }
        }

        private void FixedUpdate()
        {
            _RigidBody.velocity = Vector3.zero;
            ApplyGravity();
            GroundCheck();
            transform.position += new Vector3(_Velocity.x, _Velocity.y) * Time.fixedDeltaTime;
        }
        void Update()
        {
            _State.Update();
        }

        public void SetAnimation(AnimationState aninemationstate)
        {
            _AnimationState = aninemationstate;
        }

        public void SwitchState(IState state)
        {
            _State.OnExit();
            _State = state;
            _State.OnEnter();
            _Animator.SetInteger("State", (int)_AnimationState);
        }

        public void GroundCheck()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 dir = (transform.position - new Vector3(_Collider.bounds.extents.x, 0, 0)) + transform.right * _Collider.bounds.size.x / (4 - 1) * i;

                Debug.DrawRay(dir, -transform.up * 0.1f, Color.red);
                if (Physics2D.Raycast(dir, -transform.up, 0.1f, _LayerMask))
                {
                    _Grounded = true;
                }
                else
                {
                    _Grounded = false;
                }
            }
        }

        public void ApplyGravity()
        {
            if (_Grounded && _AnimationState != AnimationState.Jump)
            {
                _Velocity.y = 0;
            }
            else
            {
                _Velocity.y -= _GravityForce * Time.fixedDeltaTime;
            }
        }

        public void Sweep()
        {
            Collider2D[] hits;
            hits = Physics2D.OverlapCircleAll(transform.position + _AttackOffset, _Radius, _Opponenet);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject.GetComponent<Sol>()._AnimationState != AnimationState.Taunt)
                {
                    AddPoint();
                }

                hits[i].gameObject.GetComponent<Sol>().SwitchState(new Twist(hits[i].gameObject.GetComponent<Sol>()));
                HitLag();
                Rumble();
            }
        }

        public void ReturnToIdle()
        {
            SwitchState(new Idle(this));
        }

        public void HitLag()
        {
            SwitchState(new HitLag(this, _State, 0.5f));
        }

        public void HitStun()
        {
            SwitchState(new HitStun(this, _State, 0.5f, 0.4f));
        }

        public void KnockBack()
        {
            _Velocity.x = _KnockBackForce;
        }
        public void GroundBounce1()
        {
            _Velocity.x -= _Velocity.x * 0.5f;
        }
        public void GroundBounce2()
        {
            _Velocity.x = 0;
        }
        public void Rumble()
        {
            _Camera.SwitchState(new Rumble(_Camera, 0.4f, 0.3f));
        }
        public void AddPoint()
        {
            if (_Renderer.flipX)
            {
                GameManager.instance.AddPlayer1Point();
            }
            else
            {
                GameManager.instance.AddPlayer2Point();
            }

        }

        public void Restart()
        {
            if (_Renderer.flipX)
            {
                GameManager.instance.Reset1();
            }
            else
            {
                GameManager.instance.Reset2();
            }
            _WallBounce = false;
            _PreWallBounce = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _State.OnCollisionEnter(collision);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _AttackOffset, _Radius);
        }
    }

    public class Idle : IState
    {
        Sol _Player;

        public Idle(Sol player)
        {
            _Player = player;

        }
        public void OnEnter()
        {
            _Player._Velocity.x = 0;
            _Player.SetAnimation(AnimationState.Idle);
            _Player._Animator.speed = 1;
        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            if (_Player._Inputs._LeftButtonDown || _Player._Inputs._LeftButtonHeld)
            {
                _Player.SwitchState(new FWalk(_Player));
            }

            if (_Player._Inputs._RightButtonDown || _Player._Inputs._RightButtonHeld)
            {
                _Player.SwitchState(new BWalk(_Player));
            }

            if (_Player._Inputs._AttackButtonDown)
            {
                _Player.SwitchState(new Sweep(_Player));
            }

            if (_Player._Inputs._TauntButtonDown)
            {
                _Player.SwitchState(new Taunt(_Player));
            }
        }
        public void FixedUpdate()
        {

        }

        public void OnCollisionEnter(Collision2D collision)
        {

        }

        public void OnExit()
        {

        }
    }


    public class FWalk : IState
    {
        Sol _Player;
        public FWalk(Sol player)
        {
            _Player = player;
        }
        public void OnEnter()
        {
            if (_Player._Renderer.flipX)
            {
                _Player.SetAnimation(AnimationState.F_Walk);
            }
            else
            {
                _Player.SetAnimation(AnimationState.B_Walk);
            }
            _Player._Velocity = new Vector2(-_Player._MoveSpeed, _Player._Velocity.y);
        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            if (_Player._Inputs._LeftButtonUp)
            {
                _Player.SwitchState(new Idle(_Player));
            }

            if (_Player._Inputs._RightButtonDown)
            {
                _Player.SwitchState(new BWalk(_Player));
            }

            if (_Player._Inputs._AttackButtonDown)
            {
                _Player.SwitchState(new Sweep(_Player));
            }

            if (_Player._Inputs._TauntButtonDown)
            {
                _Player.SwitchState(new Taunt(_Player));
            }
        }

        public void FixedUpdate()
        {

        }

        public void OnCollisionEnter(Collision2D collision)
        {

        }

        public void OnExit()
        {

        }
    }
    public class BWalk : IState
    {
        Sol _Player;
        public BWalk(Sol player)
        {
            _Player = player;
        }
        public void OnEnter()
        {
            if (_Player._Renderer.flipX)
            {
                _Player.SetAnimation(AnimationState.B_Walk);
            }
            else
            {
                _Player.SetAnimation(AnimationState.F_Walk);
            }

            _Player._Velocity = new Vector2(_Player._MoveSpeed, _Player._Velocity.y);

        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            if (_Player._Inputs._RightButtonUp)
            {
                _Player.SwitchState(new Idle(_Player));
            }

            if (_Player._Inputs._LeftButtonDown)
            {
                _Player.SwitchState(new FWalk(_Player));
            }

            if (_Player._Inputs._AttackButtonDown)
            {
                _Player.SwitchState(new Sweep(_Player));
            }

            if (_Player._Inputs._TauntButtonDown)
            {
                _Player.SwitchState(new Taunt(_Player));
            }
        }
        public void FixedUpdate()
        {

        }

        public void OnCollisionEnter(Collision2D collision)
        {

        }

        public void OnExit()
        {

        }
    }

    public class Sweep : IState
    {
        Sol _Player;
        public Sweep(Sol player)
        {
            _Player = player;
        }
        public void OnEnter()
        {
            _Player._Velocity.x = 0;
            _Player.SetAnimation(AnimationState.Sweep);
            _Player._Animator.speed = 1;
        }

        public void HandleInput()
        {

        }

        public void Update()
        {

        }
        public void FixedUpdate()
        {

        }
        public void OnCollisionEnter(Collision2D collision)
        {

        }
        public void OnExit()
        {

        }

    }

    public class Twist : IState
    {
        Sol _Player;

        public Twist(Sol player)
        {
            _Player = player;
        }
        public void OnEnter()
        {
            _Player._Velocity.x = 0;
            _Player.SetAnimation(AnimationState.Twist);
            _Player._Animator.speed = 1;
        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            if (_Player._WallBounce && !_Player._PreWallBounce)
            {
                _Player._Velocity.x = _Player._WallBounceForce;
            }

            _Player._PreWallBounce = _Player._WallBounce;
        }
        public void FixedUpdate()
        {

        }
        public void OnCollisionEnter(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                _Player._Audio.Play();
                _Player._WallBounce = true;
                _Player.SwitchState(new HitStun(_Player, _Player._State, 0.4f, 0.4f));
            }
        }
        public void OnExit()
        {

        }
    }

    public class Taunt : IState
    {
        Sol _Player;

        public Taunt(Sol player)
        {
            _Player = player;
        }

        public void OnEnter()
        {
            _Player._Velocity.x = 0;
            _Player.SetAnimation(AnimationState.Taunt);
        }

        public void HandleInput()
        {

        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }

        public void OnCollisionEnter(Collision2D collision)
        {

        }

        public void OnExit()
        {

        }
    }
    public class HitStun : IState
    {
        Sol _Player;
        IState _PreviousState;
        float _TimeInHitLag, _Intensity;

        Vector2 _Preposition;


        public HitStun(Sol player, IState previousState, float time, float intensity)
        {
            _Player = player;
            _PreviousState = previousState;
            _Intensity = intensity;
            _TimeInHitLag = time;

        }
        public void OnEnter()
        {
            _Player._Animator.speed = 0;
            _Preposition = _Player.transform.position;

        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            _TimeInHitLag -= Time.deltaTime;
            _Intensity -= Time.deltaTime;
            _Player.transform.position = _Preposition + (Random.insideUnitCircle * _Intensity);

            if (_TimeInHitLag <= 0)
            {
                _Player.SwitchState(_PreviousState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void OnCollisionEnter(Collision2D collision)
        {

        }
        public void OnExit()
        {
            _Player._Renderer.color = Color.white;
            _Player._Animator.speed = 1;

            _Player.transform.position = _Preposition;
        }
    }

    public class HitLag : IState
    {
        Sol _Player;
        IState _PreviousState;
        float _TimeInHitLag;
        Vector2 _Preposition;

        public HitLag(Sol player, IState previousState, float time)
        {
            _Player = player;
            _PreviousState = previousState;

            _TimeInHitLag = time;
            _Preposition = _Player.transform.position;
        }
        public void OnEnter()
        {
            _Player._Animator.speed = 0;
        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            _TimeInHitLag -= Time.deltaTime;

            if (_TimeInHitLag <= 0)
            {
                _Player.SwitchState(_PreviousState);
            }
        }

        public void FixedUpdate()
        {

        }
        public void OnCollisionEnter(Collision2D collision)
        {

        }

        public void OnExit()
        {
            _Player._Animator.speed = 1;
            _Player.transform.position = _Preposition;
        }
    }
}



