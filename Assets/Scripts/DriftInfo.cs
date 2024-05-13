using UnityEngine;

[CreateAssetMenu(fileName = "DriftInfo", menuName = "Gameplay/DriftInfo")]
public class DriftInfo : ScriptableObject
{
    [SerializeField] private float _forwardStiffness;
    [SerializeField] private float _sidewaysStiffness;

    public float forwardStiffness => _forwardStiffness;
    public float sidewaysStiffness => _sidewaysStiffness;
}