using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Text Data", menuName = "Scriptable Object/Interaction Text Data")]
public class Model_InteractionTextData : BaseModel
{
    [SerializeField] private string _interactionText;
    public string InteractionText { get { return _interactionText; } }
}
