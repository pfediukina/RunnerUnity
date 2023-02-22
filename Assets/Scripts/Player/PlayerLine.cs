using UnityEngine;

public class PlayerLine
{
    private bool _isRight;
    private int _line;
    private int _prevLine;
    private float _stepTimer;

    private float[] _linesPosX;

    public PlayerLine(Player player)
    {
        player.OnPlayerInput += GetPlayerInput;
        _line = GameData.GameSettings.StartLine;
        CalculateLinesPositionX(player);
    }

    private void GetPlayerInput(Vector2 direction, Player player)
    {
        if(direction == Vector2.up || direction == Vector2.down) return;
        if(player.StateMachine.CurrentState is DeathState) return;
        
        _isRight = direction == Vector2.right ? true : false;
        _prevLine = _line;
        
        _line += _isRight ? 1 : -1; 
        _line = Mathf.Clamp(_line, 0, GameData.GameSettings.NumberOfLines - 1);
        
        _stepTimer = 0;
    }

    public void UpdateLine(Player player)
    {
        if(!IsPlayerOnCurrentLine(player))
        {
            _stepTimer += Time.deltaTime;
            player.transform.position = GetMoveLerp(player);
        }
    }

    private bool IsPlayerOnCurrentLine(Player player)
    {
        if(player.transform.position.x != _linesPosX[_line]) return false;
        return true;
    }

    private Vector3 GetMoveLerp(Player player)
    {
        float newX = Mathf.Lerp(player.transform.position.x, _linesPosX[_line], _stepTimer / player.PlayerSettings.StepDuration);
        Vector3 offset = new Vector3(newX, player.transform.position.y, player.transform.position.z);
        return offset;
    }

    private void CalculateLinesPositionX(Player owner)
    {
        _linesPosX = new float[GameData.GameSettings.NumberOfLines]; 
        _linesPosX[GameData.GameSettings.StartLine] = owner.transform.position.x;

        for(int i = 0; i < GameData.GameSettings.NumberOfLines; i++)
        {
            if(i == GameData.GameSettings.StartLine) continue;
            _linesPosX[i] = GameData.GameSettings.LineDistance *
                                (i - GameData.GameSettings.StartLine) + 
                                _linesPosX[GameData.GameSettings.StartLine];
        }
    }
}