public partial class GameStateEvent
{
    public static event System.Action<GameState> OnChangeGameState;
    public static void Fire_OnChangeGameState(GameState gameState) { OnChangeGameState?.Invoke(gameState); }
}


public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;
    }

    private void Start()
    {
        OnChangeGameState(GameState.Begin);
    }

    void OnChangeGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Begin:
                HandleBegin();
                break;

            case GameState.Play:
                HandlePlay();
                break;

            case GameState.Minigame:
                HandleMinigame();
                break;

            case GameState.GameEnd:
                HandleMinigame();
                break;

            case GameState.Win:
                HandleWin();
                break;

            case GameState.Lose:
                HandleLose();
                break;

            default:
                break;
        }
    }



    public void HandleBegin()
    {
       

    }

    public void HandlePlay()
    {
        
    }

    public void HandleGameEnd()
    {

    }

    public void HandleMinigame()
    {
       
    }

    public void HandleWin()
    {
       
    }

    public void HandleLose()
    {
        
    }

    

    private void OnDisable()
    {
        GameStateEvent.OnChangeGameState -= OnChangeGameState;
    }
}


public enum GameState
{
    Begin,
    Play,
    Minigame,
    GameEnd,
    Win,
    Lose
}

public enum VfxType
{
    Upgrade,
    GameEnd
}