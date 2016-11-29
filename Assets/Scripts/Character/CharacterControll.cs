using UnityEngine;
using System.Collections;
using Character;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterControll : MonoBehaviour
{

    Rigidbody2D _body;
    Animator _animator;
    public float _moveSpeed { get; set; }
    [Range(0f, 0.5f)]
    public float checkOffset;
    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<CharacterDataController>().characterStatistics.FindStatistics(6).ActualValue -= 10;
        }
    }

    void FixedUpdate()
    {
        Controll();
    }

    void Controll()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var newVelocity = new Vector2();
        if (CanBeMoveHorizontal(horizontal))
        {
            newVelocity.x = horizontal * _moveSpeed;
        }
        if (CanBeMoveVertical(vertical))
        {
            newVelocity.y = vertical * _moveSpeed;
        }
        _animator.SetFloat("Vertical", newVelocity.y);
        _animator.SetFloat("Horizontal", newVelocity.x);
        _body.velocity = newVelocity;
    }

    protected bool CanBeMoveVertical(float verticalValue)
    {
        int sign = GetSign(verticalValue);

        if (ActualDrawingMap.GetTileTypeFromPosition(transform.position.x, transform.position.y + sign * checkOffset) == TileMap.TilesTypes.Path)
            return true;

        return false;
    }

    protected bool CanBeMoveHorizontal(float horizontalValue)
    {
        int sign = GetSign(horizontalValue);

        if (ActualDrawingMap.GetTileTypeFromPosition(transform.position.x + sign * checkOffset, transform.position.y) == TileMap.TilesTypes.Path)
            return true;

        return false;
    }

    protected int GetSign(float value)
    {
        if (value < 0)
            return -1;
        else if (value > 0)
            return 1;
        else
            return 0;
    }
}
