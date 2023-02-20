using UnityEngine;

public class PlayerLine
{
    private bool _isRight;
    private ushort _currentLine;
    private ushort _line;
    private float[] _linesPosX;

    public PlayerLine(Player player)
    {
        player.OnPlayerMove += GetPlayerInput;
        _line = (ushort)GameManager.GameSettings.StartLine;
        CalculateLinesPositionX(player);
        ChangePlayerLine(player);
    }

    private void GetPlayerInput(Vector2 direction, Player player)
    {
        if(direction == Vector2.up || direction == Vector2.down) return;
        
        _isRight = direction == Vector2.right ? true : false;
        var lastLine = _line; 
        _line += (ushort)(_isRight ? 1 : -1); 

        _line = (ushort)(Mathf.Clamp(_line, 0, GameManager.GameSettings.NumberOfLines - 1));
        ChangePlayerLine(player);
    }

    //WIP
    private void ChangePlayerLine(Player player)
    {
        var offsetX = Vector3.right * (_linesPosX[_line] - player.transform.position.x);
        Debug.Log(_linesPosX[_line] + " - " + player.transform.position.x + " = " + offsetX);
        player.transform.position += offsetX;
    }

    private void ChangePlayerLine(Player player, ushort new_line)
    {   
        _line = new_line;
        ChangePlayerLine(player);
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