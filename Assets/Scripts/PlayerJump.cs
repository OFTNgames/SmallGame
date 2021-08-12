using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private ScriptableEventChannel _scriptableEventChannel;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rigidBody;
    
    private bool _canJump;
    private bool _shouldJump;
    private Spawner _jumpParticles;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _jumpParticles = GetComponent<Spawner>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _shouldJump = true;
            _canJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (_shouldJump)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Jump");
            _shouldJump = false;
            _rigidBody.AddForce(Vector2.up * _jumpForce);
            _scriptableEventChannel.ShakeTheCamera?.Invoke(0.05f, 0.025f);
            _jumpParticles.Spawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var platform = collision.collider.gameObject.GetComponent<Platforms>();
        if (platform)
        {
            _canJump = true;
        }
    }
}
