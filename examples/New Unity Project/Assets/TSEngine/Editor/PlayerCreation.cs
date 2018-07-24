using UnityEngine;
using UnityEditor;
public class PlayerCreationWindow : EditorWindow
{
    enum ColliderType
    {
        BoxCollider2D, CircleCollider2D, CapsuleCollider2D, PolygonCollider2D
    }

    Vector2 scrollPos;
    ColliderType cType;
    bool legsAndBody = true;
    bool WeaponAndBody = true;

    Sprite bodySprite;
    Sprite legsSprite;
    Sprite weaponSprite;

    GameObject player;
    GameObject body;
    GameObject weapon;
    GameObject legs;
    GameObject spawn;
    GameObject muzzleSpawn;
    GameObject rotationOrigin;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/TSEngine/PlayerCreation")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PlayerCreationWindow window = (PlayerCreationWindow)GetWindow(typeof(PlayerCreationWindow));
        window.Show();
    }

    void OnGUI()
    {
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent("Separate Weapon From Body "));
        EditorGUIUtility.labelWidth = textDimensions.x + 10;
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos,true,false,GUILayout.Width(Screen.width),GUILayout.Height(Screen.height - 20));
        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Player Creation", EditorStyles.boldLabel);

        GUILayout.BeginVertical("HelpBox");

        cType = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", cType);
        legsAndBody = EditorGUILayout.Toggle("Separate Body From Legs", legsAndBody);
        WeaponAndBody = EditorGUILayout.Toggle("Separate Weapon From Body", WeaponAndBody);

        if (legsAndBody)
        {
            bodySprite = (Sprite)EditorGUILayout.ObjectField("Body Sprite", bodySprite, typeof(Sprite), true);
            legsSprite = (Sprite)EditorGUILayout.ObjectField("Legs Sprite", legsSprite, typeof(Sprite), true);
        }
        else
        {
            bodySprite = (Sprite)EditorGUILayout.ObjectField("Body Sprite", bodySprite, typeof(Sprite), true);
        }

        if (WeaponAndBody)
        {
            weaponSprite = (Sprite)EditorGUILayout.ObjectField("Legs Sprite", weaponSprite, typeof(Sprite), true);
        }

        GUILayout.EndVertical();

        if (GUILayout.Button("Create Player"))
        {
            player = new GameObject();
            player.name = "Player";
            player.tag = "Player";
            player.layer = LayerMask.NameToLayer("Player");
            PlayerComponents();

            body = new GameObject();
            body.name = "Body";
            body.transform.parent = player.transform;
            body.layer = LayerMask.NameToLayer("Player");
            BodyComponents();

            if (legsAndBody)
            {
                legs = new GameObject();
                legs.name = "Legs";
                legs.transform.parent = player.transform;
                legs.layer = LayerMask.NameToLayer("Player");
                LegsComponents();
            }

            if (WeaponAndBody)
            {
                weapon = new GameObject();
                weapon.name = "Weapon";
                weapon.transform.parent = player.transform;
                weapon.layer = LayerMask.NameToLayer("Player");
                WeaponComponents();
            }

            spawn = new GameObject();
            spawn.name = "Bullet Spawn Point";
            spawn.transform.parent = body.transform;
            spawn.layer = LayerMask.NameToLayer("Player");

            muzzleSpawn = new GameObject();
            muzzleSpawn.name = "Muzzle Flash Spawn Point";
            muzzleSpawn.transform.parent = body.transform;
            muzzleSpawn.layer = LayerMask.NameToLayer("Player");

            rotationOrigin = new GameObject();
            rotationOrigin.name = "Rotation Origin";
            rotationOrigin.transform.parent = body.transform;
            rotationOrigin.layer = LayerMask.NameToLayer("Player");
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    void PlayerComponents()
    {
        switch (cType)
        {
            case ColliderType.BoxCollider2D:
                player.AddComponent<BoxCollider2D>();
                break;
            case ColliderType.CapsuleCollider2D:
                player.AddComponent<CapsuleCollider2D>();
                break;
            case ColliderType.CircleCollider2D:
                player.AddComponent<CircleCollider2D>();
                break;
            case ColliderType.PolygonCollider2D:
                player.AddComponent<PolygonCollider2D>();
                break;
        }

        player.AddComponent<Animator>();
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<PlayerController>();

    }

    void BodyComponents()
    {
        body.AddComponent<SpriteRenderer>();
        body.AddComponent<Shooting>();
        SpriteRenderer sr = body.GetComponent<SpriteRenderer>();
        sr.sprite = bodySprite;
        sr.sortingLayerName = "Player";
    }

    void LegsComponents()
    {
        legs.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = legs.GetComponent<SpriteRenderer>();
        sr.sprite = legsSprite;
        sr.sortingLayerName = "Player";
        sr.sortingOrder = -1;
    }

    void WeaponComponents()
    {
        weapon.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = weapon.GetComponent<SpriteRenderer>();
        sr.sprite = weaponSprite;
        sr.sortingLayerName = "Player";
    }
}