using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private TargetPart[] _parts;
    [SerializeField] private AnimationCurve _shapeCurve;
    
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private bool _rightDirection;

    [SerializeField] private SpawnChances _spawnChances;

    public GameObject Model => _model;

    private bool _isInitialized;
    
    public void Initialize(bool direction, float rotationSpeed, float scale)
    {
        this.name = "Target root";
        _rotationSpeed = rotationSpeed;
        _rightDirection = direction;

        GameObject moneyPrefab = _spawnChances.GetMoney();
        if (moneyPrefab != null)
            Instantiate(moneyPrefab, _model.transform);

        GameObject obstaclePrefab = _spawnChances.GetObstacle();
        if (obstaclePrefab != null)
            Instantiate(obstaclePrefab, _model.transform);

        _isInitialized = true;
    }

    public void Destroy()
    {
        var child = GetComponentsInChildren<Rigidbody2D>();
        foreach (var part in child)
        {
            part.bodyType = RigidbodyType2D.Dynamic;
        }
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
