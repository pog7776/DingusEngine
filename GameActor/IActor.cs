using DingusEngine.GameComponent;
using DingusEngine.StandardComponents;
using DingusEngine;
using System;
using System.Numerics;
using GameEngine = DingusEngine.EGameEngine;

/// <summary>
/// Summary description for Class1
/// </summary>
public interface IActor
{
	public ATransform? Transform { get; set; }
	public string Name { get; set; }
	public List<IComponent> Components { get; }
	protected EGameEngine Engine { get; }

	public abstract void Update();

	/// <summary>
	/// Add a component to the actor.
	/// </summary>
	/// <param name="component">The component to add to the actor.</param>
	public T? AddComponent<T>(IComponent component);

	/// <summary>
	/// Add a new instance of a component of the type T.
	/// </summary>
	/// <typeparam name="T">Type of component to add.</typeparam>
	public T? AddComponent<T>() where T : new();

	/// <summary>
	/// Remove the passed in component.
	/// </summary>
	/// <param name="component">Component to remove from actor.</param>
	public void RemoveComponent(IComponent component);

	/// <summary>
	/// Remove first found component of type T.
	/// </summary>
	/// <typeparam name="T">Type to remove.</typeparam>
	public void RemoveComponent<T>();

	/// <summary>
	/// Return the first component of the given type.
	/// </summary>
	/// <typeparam name="T">The type of component to search for.</typeparam>
	/// <returns></returns>
	public T? GetComponent<T>();
}
