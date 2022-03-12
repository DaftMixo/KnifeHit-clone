using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private AnimationCurve _shapeCurve;
    
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private bool _rightDirection;

    [SerializeField] private SpawnChances _spawnChances;

    private bool _isInitialized;
    
    public void Initialize(bool direction, float rotationSpeed)
    {
        this.name = "Target root";
        _rotationSpeed = rotationSpeed;
        _rightDirection = direction;

        Instantiate(_spawnChances.GetMoney(), _model.transform);
        Instantiate(_spawnChances.GetObstacle(), _model.transform);

        _isInitialized = true;
    }
    private void Update()
    {
        if (!_isInitialized) return;
        
        int directionScale = _rightDirection ? -1 : 1;
        _model.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * directionScale);
        
    }

    [ContextMenu("Shape")]
    public void Shape()
    {
        //TODO Make shape
    }
}
