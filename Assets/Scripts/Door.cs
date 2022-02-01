using UnityEngine;

public class Door : InteractiveObject
{
    [SerializeField] private Transform _exitPosition;

    protected override void Interact(GameObject gameObject)
    {
        MoveObject(gameObject);
    }

    private void MoveObject (GameObject gameObject)
    {
        gameObject.transform.position = _exitPosition.position;
    }
}