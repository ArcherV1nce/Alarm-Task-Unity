using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractiveObject : MonoBehaviour
{
    private Collider2D _collider;

    protected virtual void Awake ()
    {
        _collider = GetComponent<Collider2D> ();
        if (_collider.isTrigger == false)
        {
            _collider.isTrigger = true;
        }
    }

    protected virtual void OnTriggerEnter2D (Collider2D collider2D)
    {
        Interact(collider2D.gameObject);
    }

    protected virtual void Interact (GameObject gameObject)
    {
        Debug.Log("Some action was performed.");
    }
}