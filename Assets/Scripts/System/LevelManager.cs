using System.Collections.Generic;
using Entity;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System {
    public class LevelManager : Singleton<LevelManager> {
        [field: SerializeField] public int CurrentIndex { get; private set; }
        private void OnEnable() {
            CurrentIndex = 1;
        }

        public void LoadNext() {
            CurrentIndex = Mathf.Clamp(CurrentIndex + 1, 1, 6);

            Load(CurrentIndex);
        }

        public void LoadIndex(int i) {
            CurrentIndex = Mathf.Clamp(i, 1, 6);
            Load(CurrentIndex);
        }

        public void ReloadCurrent() {
            Load(CurrentIndex);
        }

        public void LoadTutorial() {
            Load("MainMenuTutorial");
        }
        public void LoadMenu() {
            Load("MainMenu");
        }
        private void Load(string levelName)=>SceneManager.LoadScene(levelName);

        private void Load(int i)=>SceneManager.LoadScene(i);
    }
}