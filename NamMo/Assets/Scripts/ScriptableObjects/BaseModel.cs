using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel : ScriptableObject
{
    [SerializeField] private Define.ModelType _modelType;
    public Define.ModelType ModelType { get { return _modelType; } }
}
