using UnityEngine;

namespace Entity.Player
{
    public class CharacterManager : MonoBehaviour
    {
    
        // singleton setting
        private static CharacterManager instance;
        public static CharacterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
                }

                return instance;
            }
        }

        private Player player;
        public Player Player
        {
            get{return player;}
            set{player = value;}
        }


        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance == this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}