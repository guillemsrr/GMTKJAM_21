using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplatePool<T> where T : MonoBehaviour
{
    private int _poolSize;

    private GameObject _prefab;

    private List<T> _poolList;

    private Transform _target;

    public void Init(GameObject prefab, Transform target, int poolSize = 50)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        _target = target;
        InitializePool();
    }

    private void InitializePool()
    {
        _poolList = new List<T>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject go = GameObject.Instantiate(_prefab, _target.position, _target.rotation, _target);
            go.SetActive(false);
            _poolList.Add(go.GetComponent<T>());
        }
    }

    public T Instantiate(Vector3 position, Quaternion rotation)
    {
        if (_poolList.Count == 0) return null;
        T template = _poolList[0];
        ((Component)template).transform.position = position;
        template.transform.rotation = rotation;
        template.gameObject.SetActive(true);
        _poolList.Remove(template);

        return template;
    }

    public void ReturnToPool(T template)
    {
        template.gameObject.SetActive(false);
        template.transform.position = _target.position;
        _poolList.Add(template.GetComponent<T>());
    }

    public void DestroyAll()
    {
        foreach(T t in _poolList)
        {
            GameObject.DestroyImmediate(t);
        }
        
    }
}
