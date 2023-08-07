namespace Dominoes;

public class Position
{
    private int _posX;
    private int _posY;
    public bool SetPosition(int posX, int posY)
    {
        if (posX > 0 && posY > 0)
        {
            _posX = posX;
            _posY = posY;
            return true;
        }
        return false;
    }
    public int GetPosX()
    {
        return _posX;
    }
    public int GetPosY()
    {
        return _posY;
    }
}
