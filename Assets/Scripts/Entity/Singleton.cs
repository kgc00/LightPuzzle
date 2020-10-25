using UnityEngine;

namespace Entity {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T instance;

        public static T Instance {
            get {
                if (instance == null) {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return instance;
            }
        }


        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(gameObject);
            }
            else {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}