using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{
    [SerializeField] private ScriptableObject _driftInfoName;
    private DriftInfo[] _allDriftInfo;

    private void Awake()
    {
        _allDriftInfo = Resources.LoadAll<DriftInfo>("ScriptableObject/Drift");
    }

    private DriftInfo LoadDriftInfoByName()
    {
        foreach (var driftInfo in _allDriftInfo)
        {
            if (driftInfo.name == _driftInfoName.name)
            {
                return driftInfo;
            }
        }

        return null;
    }
}