using UnityEngine;

public class RotatorObject : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateDirection;
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        transform.Rotate(_rotateDirection * _rotateSpeed * Time.deltaTime);
    }
}
