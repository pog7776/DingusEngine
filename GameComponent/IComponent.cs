using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public interface IComponent
{
	public string Name { get; set; }

	public abstract void Update();
}
