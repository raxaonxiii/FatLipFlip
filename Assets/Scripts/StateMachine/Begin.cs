using System.Collections;
using UnityEngine;

public class Begin : State
{
    public Begin(GameManager gameMan) : base(gameMan)
    {
    }

    // Start is called before the first frame update
    public override IEnumerator Start()
    {
        GameMan.timer = 0;
        yield break;
        //yield return new WaitForEndOfFrame();
        //GameMan.SetState(new GameMode(GameMan));
    }
}
