using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Models {
    [Serializable]
    [CreateAssetMenu(fileName = "Levels", menuName = "Light Puzzle/Levels", order = 0)]
    public class Levels : ScriptableObject {
        [SerializeField] public List<SceneAsset> Gameplay;
    }
}