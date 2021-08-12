using UnityEngine;

public class PlayerGravityControl : MonoBehaviour
{
    public static event System.Action<float, float> GravityAmount = delegate { };

    [SerializeField] private float _maxGravityControlTime;

    private ParticleSystem _particleSystem;
    private bool _gravityEnabled;
    private bool _playEffects;
    private float _gravityControlTime;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _gravityEnabled = true;
        _rigidBody = GetComponent<Rigidbody2D>();
        _gravityControlTime = _maxGravityControlTime;
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (_gravityEnabled)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Character/StartGravity");
                _gravityEnabled = false;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Character/EndGravity");
                _gravityEnabled = true;
            }
        }

        if(!_gravityEnabled)
        {
            if(!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
        else
        {
            _particleSystem.Stop();
        }


        GravityTime();
        GravityAmount?.Invoke(_gravityControlTime, _maxGravityControlTime);
    }

    private void GravityTime()
    {
        if(_gravityEnabled)
        {
            _gravityControlTime = Mathf.Clamp(_gravityControlTime + Time.deltaTime, 0, _maxGravityControlTime);
            _rigidBody.gravityScale = 1;
        }
        else
        {
            _gravityControlTime -= Time.deltaTime;
            _rigidBody.gravityScale = 0;
            if (_gravityControlTime <= 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Character/EndGravity");
                _gravityEnabled = true;
            }
        }
    }
}
