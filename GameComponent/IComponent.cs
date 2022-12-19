using System;
using DingusEngine;
using DingusEngine.GameActor;
using Actor = DingusEngine.GameActor.Actor;

/// <summary>
/// Summary description for Class1
/// </summary>
public interface IComponent
{
	public string Name { get; set; }
	public IActor Owner { get; set; }
    protected GameEngine Engine { get; }

    public abstract void Start();
	public abstract void Update();
}
