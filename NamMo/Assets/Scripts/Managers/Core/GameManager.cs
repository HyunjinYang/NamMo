using UnityEngine;

public class GameManager
{
    private PlayerMovement _player;

    public void Init()
    {
        FindPlayer();
    }
    public void FindPlayer()
    {
        _player = GameObject.Find("Nammo").GetComponent<PlayerMovement>();
    }

    public Vector2 ReturnToPlayerPostion()
    {
        Vector2 pos = _player.gameObject.transform.position;
        return pos;
    }
}
