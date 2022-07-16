using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    [SerializeField, Tooltip("The direction towards the compass North points.")]
    private Vector3 _referenceVector = new Vector3(0, 0, 1f);

    private Vector3 _currentVector;
    private float _currentAngle;

    private void Update() 
    {
        // Get where the user is currently facing, we don't care about Y Axis
        _currentVector = _playerTransform.forward;
        _currentVector.y = 0f;
        _currentVector.Normalize();

        // Get the distance from the reference
        _currentVector = _currentVector - _referenceVector;
        _currentVector.y = 0f;
        _currentVector.Normalize();

        // Ensure is not zero because causes issues
        if(_currentVector == Vector3.zero)
            _currentVector = new Vector3(1, 0, 0);

        // Calculate angle in radians and adjust
        _currentAngle = Mathf.Atan2(_currentVector.x, _currentVector.z);
        _currentAngle = (_currentAngle * Mathf.Rad2Deg + 90f) * 2f;

        transform.rotation = Quaternion.AngleAxis(_currentAngle, _referenceVector);
    }
}
