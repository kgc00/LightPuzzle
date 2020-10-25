using System.Collections.Generic;
using Entity;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System {
    public class LevelManager : Singleton<LevelManager> {
        [SerializeField] private int currentIndex;
        [SerializeField] private Levels levels;
        private void OnEnable() {
            if (levels == null) levels = UnityEngine.Resources.Load<Levels>("Levels");
        }

        public void LoadNext() {
            print(currentIndex);
            print(levels);
            print(levels.Gameplay);
            print(levels.Gameplay[currentIndex].name);
            currentIndex = Mathf.Clamp(currentIndex + 1, 0, levels.Gameplay.Count - 1);
            Load(levels.Gameplay[currentIndex].name);
        }

        public void ReloadCurrent() {
            print(levels.Gameplay[currentIndex].name);
            Load(levels.Gameplay[currentIndex].name);
        }
        
        private void Load(string name)=>SceneManager.LoadScene(name);
    }
}