using System.Collections;
using System.Collections.Generic;

public interface IGameManager
{
    ManagerStatus status { get; }
    void StartUp();

    
}
