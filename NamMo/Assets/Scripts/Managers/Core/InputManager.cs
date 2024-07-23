using System.Collections.Generic;
using UnityEngine;

public class InputManager
{

    public void Init()
    {
        
    }
    public Define.KeyInput ReturnKey(Vector2 vector)
    {
        if (vector.x.Equals(1) && vector.y.Equals(0))
        {
            return Define.KeyInput.D;
        }
        else if (vector.x.Equals(-1) && vector.y.Equals(0))
        {
            return Define.KeyInput.A;
        }
        else if (vector.x.Equals(0) && vector.y.Equals(1))
        {
            return Define.KeyInput.W;
        }
        else
        {
            return Define.KeyInput.S;
        }
    }

    public Define.KeyInput ActionKey(string action)
    {
        if (action.Equals("Enter"))
        {
            return Define.KeyInput.Enter;
        }
        else
        {
            return Define.KeyInput.Esc;
        }
    }
}