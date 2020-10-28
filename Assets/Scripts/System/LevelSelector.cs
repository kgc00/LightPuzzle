using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace System {
    public class LevelSelector : MonoBehaviour {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI tmpText; 
        private int indexToLoad;
        private void Update() {
            indexToLoad = (int)slider.value;
            tmpText.text = $"Play Level {indexToLoad.ToString()}";
        }

        public void HandleLoadLevel() => LevelManager.Instance.LoadIndex(indexToLoad);
    }
}