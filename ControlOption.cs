using System;
using SplashKitSDK;
namespace CustomProgram;

class ControlOption
{

    private SplashKitSDK.KeyCode _SplashKitKey;
    private String _Key;
    private String _KeyDescription;

    public ControlOption(String k, String kd) {
        
        /*
         * k = Key
         * kd = KeyDescription
        */

        _Key = k;
        _KeyDescription = kd;

    }
    
    public String Key {
        get { return _Key; }
        set { _Key = value; }
    }

    public String Value {
        get { return _KeyDescription; }
        set { _KeyDescription = value; }
    }

}