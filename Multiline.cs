#if UNITY_EDITOR
using UnityEngine;

public class Multiline : MonoBehaviour {
    [Multiline(50)] public string Notes;
}
#endif