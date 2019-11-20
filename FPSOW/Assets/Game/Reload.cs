using System.Collections;

public class Reload : Command
{
    IEnumerator reloadCoroutine;

    public void SetReload(IEnumerator reload)
    {
        reloadCoroutine = reload;
    }
}
