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
        player.OnPlayerMove += GetPlayerInput;
        _line = GameManager.GameSettings.StartLine;
        CalculateLinesPositionX(player);
    }

    private void GetPlayerInput(Vector2 direction, Player player)
    {
        if(direction == Vector2.up || direction == Vector2.down) return;
        
        _isRight = direction == Vector2.right ? true : false;
        _prevLine = _line;
        
        _line += _isRight ? 1 : -1; 
        _line = Mathf.Clamp(_line, 0, GameManager.GameSettings.NumberOfLines - 1);
        
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

    //WIP
    private Vector3 GetMoveLerp(Player player)
    {
        float newX = Mathf.Lerp(player.transform.position.x, _linesPosX[_line], _stepTimer / player.PlayerSettings.StepDuration);
        Vector3 offset = new Vector3(newX, player.transform.position.y, player.transform.position.z);
        return offset;
    }

    private void CalculateLinesPositionX(Player owner)
    {
        _linesPosX = new float[GameManager.NumberOfLines]; 
        _linesPosX[GameManager.StartLine] = owner.transform.position.x;

        for(int i = 0; i < GameManager.NumberOfLines; i++)
        {
            if(i == GameManager.StartLine) continue;
            _linesPosX[i] = GameManager.GameSettings.LineDistance *
                                (i - GameManager.StartLine) + 
                                _linesPosX[GameManager.StartLine];
        }
    }
}