using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region XML Summary
/// <summary>
///     <para>
///         Singletons are used when only ONE MonoBehaviour should exist at any given point.
///         The &lt;T&gt; signifies a generic (which can be named anything, typically denoted as T).<br></br>
///         Breaking the class signature into parts, we have the following:
///     </para>
///
///     <list>
///         <item>
///             <term>
///             <see langword="abstract"/><br></br>
///             </term>
///             <description>
///                 This class is abstract, meaning it's intended to be inherited from.This base class is also inheriting from MonoBehaviour, meaning all
///                 classes inheriting after it will ALSO be MonoBehaviours as well.<br></br>
///             </description>
///         </item>
///         <item>
///             <term>
///                 <typeparamref name="Generics"/> (&lt;<typeparamref name="T"/>&gt;)<br></br>
///             </term>
///             <description>
///                  Generic classes are a way to generalize code functionality. The &lt;T&gt; specifies that when creating it,
///                  we need to pass in an argument to it. T doesn't HAVE to be named T, and could be named numerous things, like TKey and TValue, in the case
///                  of Dictionaries. By default, T's actual value can be ANYTHING. This can pose an issue, and is where Type Constraints come into play.<br></br>
///             </description>
///         </item>
///         <item>
///             <term>
///                 <typeparamref name="Constraints"/> (<see langword="where"/> <typeparamref name="T"/> : <see langword="Something"/>)<br></br>
///             </term>
///             <description>
///                 Type contraints are where we can de-generalize a generalization by essentially saying "T can be anything, as long as it's THIS thing."
///                 For example, maybe we need a list of enemies, but don't care what kind they are. Instead of having a list inside of a class, just
///                 MAKE the class the list!
///                 <br></br>
///                 In our case, we are saying T can be anything, as long as it's a MonoBehaviour.
///             </description>
///         </item>
///     </list>
///
///<example>
///<code>
///public abstract class EnemyManager&lt;T&gt; : MonoBehaviour where T : EnemyData
///</code>
///</example>
/// </summary>
///
/// <remarks>T is ANY MonoBehaviour. This class MUST be inherited from and cannot be added as a component directly.</remarks>
/// <typeparam name="T">T is ANY MonoBehaviour.</typeparam>
#endregion
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null) Destroy(gameObject);


        Instance = this as T;
        SceneManager.sceneLoaded += (_, _) => { Instance = this as T; };
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }
}

public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
