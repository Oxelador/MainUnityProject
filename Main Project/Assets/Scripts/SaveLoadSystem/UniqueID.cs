using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id;

    [SerializeField] private static SerializableDictionary<string, GameObject> _idDataBase = new SerializableDictionary<string, GameObject>();

    public string ID => _id;

    private void Awake()
    {
        if(_idDataBase == null) _idDataBase = 
           new SerializableDictionary<string, GameObject>();

        if (_idDataBase.ContainsKey(_id)) Generate();
        else _idDataBase.Add(_id, gameObject);
    }

    private void OnDestroy()
    {
        if(_idDataBase.ContainsKey(_id)) _idDataBase.Remove(_id);
    }

    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        _idDataBase.Add(_id, gameObject);
    }
}
