using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _grassBlock;
    [SerializeField] private int _blockCount = 10;
    [SerializeField] private float _spacingOffset;

    private void Start()
    {
        PlaceBlocks();
    }

    private void PlaceBlocks()
    {
        for (int i = 0; i < _blockCount; i++)
        {
            //See bottom of script for full Instantiate() description.
            // Instantiate() method returns the gameobject you created,
            // letting you store it in a variable for use later on if need be.
            GameObject block = Instantiate(_grassBlock, new Vector2(i * _spacingOffset, 0), Quaternion.identity);


            //I'm sure you've seen me use this type of string before,
            //essentially anything inside the { } will be EVALUATED, and then converted to a string (if it can be).
            block.name = $"Block #{i}";
        }
    }
}

/*

---What is Instantiate()?---

Instantiate is a method (from the MonoBehaviour class) that allows us to spawn a GameObject at runtime (while playing). 
Obviously, this is EXTREMELY useful, as it allows both simple and complex spawning behaviours to be reduced to nothing more than a few lines of code.

---What can I use it for?---

Anything! Including:
    - Spawning bullets when the player shoots
    - Player respawn after dying
    - Spawning enemies
    - Loot drops after killing said enemies
    - Spawning LITERALLY any GameObject you want to

---How do I use it?---

At first glance, it looks super scary, especially seeing "Quaternion.identity". However it's honestly so simple. Here's the full function (written in a way that makes sense, not decompiled):
    public GameObject Instantiate(GameObject prefabObject, Vector3 spawningPosition, Quaternion rotation) { //unity does the spawning stuff here }

The only real key point to remember is that the GameObject passed into Instantiate() needs to be a prefab. 
As a reminder, a prefab is a gameobject created in the scene that is then dragged and dropped into the project folder. By creating a prefab, you're basically saving the gameobject as a file in your project.
once created, prefabs can be edited by double-clicking them in the project panel.

Essentially, you know everything except Quaternion. Quats are SCARY to learn and even I don't really fully understand them, but all they are is a more precise/mathematical way to represent rotation.
Quaternion.identity is basically just NO (or default, technically) rotation. In the inspector, you would see it as Rotation (0, 0, 0).
if you DID want rotation, it's quite simple to convert a Vector3 into a Quaternion using the following function:
    Quaternion myQuaternion = Quaternion.Euler(yourVector3);

Specifically to Instantiate(), what you would do is:
    GameObject mySpawnedObject = Instantiate(GameObject: spawnPrefab, Vector3: spawnPosition, Quaternion: Quaternion.identity);

---KEY TAKEAWAYS!!!---

- Instantiate spawns a gameobject into the scene

- requires a prefab (saved game object basically), position to spawn at, and rotation to spawn at (Quaternion.identity = no rotation).
    - Create prefab by dragging and dropping from hierarchy to project panel. (where your saved assets are)
    - Edit prefab by double-clicking the prefab asset inside project panel.

- Can create Quaternions by using Quaternion.Euler(Vector3 myVector); 

***IMPORTANT***
Should be noted that Instantiate() should almost NEVER be used to spawn a gameobject with a script that's also using instantiate.
If done wrong, it could cause an infinite loop of gameobjects spawning, and instacrash the editor.
***IMPORTANT***

*/