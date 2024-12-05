public class GameManager : MonoSingleton<GameManager>
{
    private Player _player;

    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindAnyObjectByType<Player>();
            }
            return _player;
        }
    }
}
