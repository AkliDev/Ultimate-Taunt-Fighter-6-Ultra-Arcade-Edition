using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collision : MonoBehaviour
{
 

    private BoxCollider2D _Collider;
    private Bounds _Bounds;

    private float _Inset;

    private Vector2 _BottemLelf, _BottemRight, _TopRight, _TopLeft;
    [SerializeField]
    [Range(0, 100)]

    private int _VerticalRayAmount;
    private float _VerticalRaySpacing;
    private Vector2 _VerticalRaySpacingVector;

    [SerializeField]
    [Range(0, 100)]
    private int _HorizontalRayAmount;
    private float _HorizontalRaySpacing;
    private Vector2 _HorizontalRaySpacingVector;
    private float _HorizontalDirection;
    [SerializeField]
    private LayerMask _LayerMask;
    [SerializeField]
    public bool _Grounded;
    [SerializeField]
    private float _SetGravity;
    private float _Gravity;



    private void Start()
    {

        _VerticalRayAmount = 4;
        _HorizontalRayAmount = 4;
        _Collider = GetComponent<BoxCollider2D>();
        _Inset = 0.025f;
        _Gravity = _SetGravity;
    }


    
    public void CheckCollision(ref Vector2 velocity)
    {
        _Gravity = _SetGravity;
        velocity.y += _Gravity * Time.fixedDeltaTime;
        UpdateBounds();
        VerticalCollision(ref velocity);
        HorizontalCollision(ref velocity);

        if (_Grounded == true)
        {
            velocity.y = 0;

        }
      
    }



    private void HorizontalCollision(ref Vector2 velocity)
    {

        if (velocity.x != 0)
        {
            _HorizontalDirection = Mathf.Sign(velocity.x);
        }

        float horizontalRayLength = Mathf.Abs(velocity.x) + _Inset;

        Vector2 rayCastPosition = _HorizontalDirection == -1 ? _BottemLelf : _BottemRight;

        for (int i = 0; i < _HorizontalRayAmount; i++)
        {
            Debug.DrawRay(rayCastPosition + Vector2.up * _HorizontalRaySpacing * i, transform.right * _HorizontalDirection, Color.red);
            RaycastHit2D rayCastHit = Physics2D.Raycast(rayCastPosition + Vector2.up * _HorizontalRaySpacing * i, Vector2.right * _HorizontalDirection, horizontalRayLength, _LayerMask);

            if (rayCastHit)
            {
                velocity.x = (rayCastHit.distance - _Inset) * _HorizontalDirection;
                horizontalRayLength = rayCastHit.distance;
                Debug.Log(rayCastHit);

            }

        }
    }
    private void VerticalCollision(ref Vector2 velocity)
    {

        float verticalDirection = Mathf.Sign(velocity.y);
        float verticalRayLength = Mathf.Abs(velocity.y) + _Inset;

        Vector2 rayCastPosition = verticalDirection == -1 ? _BottemLelf : _TopLeft;



        for (int i = 0; i < _VerticalRayAmount; i++)
        {
            Debug.DrawRay(rayCastPosition + Vector2.right * _VerticalRaySpacing * i, transform.up * verticalDirection, Color.red);
            RaycastHit2D rayCastHit = Physics2D.Raycast(rayCastPosition + Vector2.right * _VerticalRaySpacing * i, Vector2.up * verticalDirection, verticalRayLength, _LayerMask);

            if (rayCastHit)
            {
                _Grounded = true;
                velocity.y = (rayCastHit.distance - _Inset) * verticalDirection;
                verticalRayLength = rayCastHit.distance;
                Debug.Log(rayCastHit);

            }
            else
            {
                _Grounded = false;
            }
            

        }

    }

    private void UpdateBounds()
    {
        _VerticalRaySpacing = _Bounds.size.x / (_VerticalRayAmount - 1);
        _HorizontalRaySpacing = _Bounds.size.y / (_HorizontalRayAmount - 1);


        _Bounds = _Collider.bounds;
        _Bounds.Expand(_Inset * -2);

        _BottemLelf = new Vector2(_Bounds.min.x, _Bounds.min.y);
        _BottemRight = new Vector2(_Bounds.max.x, _Bounds.min.y);
        _TopLeft = new Vector2(_Bounds.min.x, _Bounds.max.y);
    }

   
    

}
