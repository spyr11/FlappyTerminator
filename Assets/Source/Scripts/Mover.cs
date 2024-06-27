using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _maxRotationZ;
    [SerializeField] float _minRotationZ;
    [SerializeField] float _tapForce;

    private Rigidbody2D _rigidBody2D;
    private Vector3 _startPosition;
    private bool _isJumping;
    private Player _player;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;

            transform.rotation = _maxRotation;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isJumping)
        {
            _rigidBody2D.velocity = new Vector2(0f, _tapForce);

            _isJumping = false;
        }
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidBody2D.velocity = Vector2.zero;
    }
}