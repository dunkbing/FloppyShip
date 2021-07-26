using UnityEngine;

namespace Entities.Background
{
    public class Bound : MonoBehaviour, IMove
    {
        private readonly float _startPos = -30f;

        private Vector3 _newPos;
        private readonly float _speed = 10;

        // Start is called before the first frame update
        private void Start()
        {
            _newPos = new Vector3(90f, 0);
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
        }

        public void Move()
        {
            // reset pos
            if (transform.position.x <= _startPos)
            {
                transform.position += _newPos;
            }
            // move
            transform.position += Vector3.left * (_speed * Time.deltaTime);
        }
    }
}
