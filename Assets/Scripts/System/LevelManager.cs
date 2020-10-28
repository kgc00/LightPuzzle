using System.Collections.Generic;
using Entity;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System {
    public class LevelManager : Singleton<LevelManager> {
        [field: SerializeField] public int CurrentIndex { get; private set; }
        [SerializeField] private Levels levels;
        private void OnEnable() {
            if (levels == null) levels = UnityEngine.Resources.Load<Levels>("Levels");
        }

        public void LoadNext() {
            print(CurrentIndex);
            CurrentIndex = Mathf.Clamp(CurrentIndex + 1, 0, levels.Gameplay.Count - 1);
            print(CurrentIndex);

            Load(levels.Gameplay[CurrentIndex].name);
        }

        public void LoadIndex(int i) {
            CurrentIndex = Mathf.Clamp(i, 0, levels.Gameplay.Count - 1);
            Load(levels.Gameplay[CurrentIndex].name);
        }

        public void ReloadCurrent() {
            print(levels.Gameplay[CurrentIndex].name);
            Load(levels.Gameplay[CurrentIndex].name);
        }
        
        private void Load(string name)=>SceneManager.LoadScene(name);
    }
}