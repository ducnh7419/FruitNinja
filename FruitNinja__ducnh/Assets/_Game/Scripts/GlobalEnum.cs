

namespace GlobalEnum{

    public enum EBackgroundMusic{
        None=0,
        MainMenu=1,
        InGame=2
    }

    public enum ESound{
        None=0,
        Slice=1,
        
    }

    public enum EState{
        None=0,
        StartGame=1,
        InGame=2,
        PreEndGame=3,
        EndGame=4

    }
    public enum EFruitType{
        None=0,
        Fruit=1,
        Bomb=2,
        Frost=3,
        RainBow=4,
        Pomegranate=5
    }

    public enum EDirect{
        None=0,
        Vertical=1,
        Horizontal=2
    }

    public enum EEvent{
        None=0,
        ChangeScore=1,
        FreezeTime=2,
        FrenzyTime=3
    }
}