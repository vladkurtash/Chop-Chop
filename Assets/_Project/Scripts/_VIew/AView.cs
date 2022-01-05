using UnityEngine;

namespace ChopChop
{
    public abstract class AView : MonoBehaviour, IView
    {
        private Transform _transform;
        public Transform Transform => _transform;

        private void OnEnable()
        {
            _transform = gameObject.GetComponent<Transform>();
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void SetRotation(Vector3 rotation)
        {
            _transform.rotation = Quaternion.Euler(rotation);
        }

        public virtual void Setup() { }
    }

    public abstract class AAxeView : AView
    {
        [SerializeField] private Renderer renderer = null;
        public Renderer Renderer => renderer;
    }
}