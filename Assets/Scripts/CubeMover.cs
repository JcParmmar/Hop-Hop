using UnityEngine;

namespace DefaultNamespace
{
    public class CubeMover : MonoBehaviour
    {
        public float movingSpeed = 5f;// backword moving speed
        public float maxBackShouldGo = -6.374908f;// last point from cube should destroy
        public SpawnManager spawn;//spawn manager ref

        private void Start()
        {
            spawn = SpawnManager.Instance;
        }

        private void Update()
        {
            //if game not start then don't calculate anything
            if(!GameManager.Instance.isGameRunning) return;
            
            //moving back cubes
            transform.Translate(Vector3.back * (movingSpeed * Time.deltaTime));
            
            if (transform.position.z < maxBackShouldGo)
            {
                //when cube rich to max back point remove that cube using spawn Manager
                
                spawn.RemoveCubeFromScene(gameObject);
            }
        }
    }
}