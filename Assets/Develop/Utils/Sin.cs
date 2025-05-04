using UnityEngine;

public class Sin : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _phase;

    private float _startPosition;
    private float _time;

    private void Awake()
    {
        _startPosition = transform.position.y;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        float yPosition = _amplitude * Mathf.Sin(_time * _frequency + _phase);

        transform.position = new Vector3(transform.position.x, _startPosition + yPosition, transform.position.z);
    }
}
