/**
 *
 * http://rusticode.com/2013/12/11/creating-game-manager-using-state-machine-and-singleton-pattern-in-unity3d/
 */
public class Singletone
{
    // constructor method has been followed by protected keyword, which prevents from calling it
    // The only way you can access singleton object is by referring to its public static property called Instance
    protected Singletone() { }
    private static Singletone _instance = null;

    public static Singletone Instance
    {
        get
        {
            return Singletone._instance == null ?
                new Singletone() : Singletone._instance;
        }
    }
}