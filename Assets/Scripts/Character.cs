using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _gold;

    public string Name;

    public void AddGold (int goldAmount)
    {
        _gold += goldAmount;
    }

    private void OnValidate()
    {
        if (_gold < 0)
            _gold = 0;

        if (_health < 0)
            _health = 0;
    }
}
