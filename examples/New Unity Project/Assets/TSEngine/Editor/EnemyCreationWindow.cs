using UnityEngine;
using UnityEditor;

public class EnemyCreationWindow : EditorWindow {

    enum ColliderType
    {
        BoxCollider2D, CircleCollider2D, CapsuleCollider2D, PolygonCollider2D
    }

    Vector2 scrollPos;
    ColliderType cType;

    bool WeaponAndBody = true;

    Sprite enemySprite;
    Sprite weaponSprite;

    GameObject enemy;
    GameObject weapon;
    GameObject enemyHealthUI;
    GameObject spawn;

    [MenuItem("Window/TSEngine/EnemyCreation")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EnemyCreationWindow window = (EnemyCreationWindow)GetWindow(typeof(EnemyCreationWindow));
        window.Show();
    }

    void OnGUI()
    {
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent("Separate Weapon From Body "));
        EditorGUIUtility.labelWidth = textDimensions.x + 10;
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height - 20));
        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Enemy Creation", EditorStyles.boldLabel);

        GUILayout.BeginVertical("HelpBox");

        cType = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", cType);
        WeaponAndBody = EditorGUILayout.Toggle("Separate Weapon From Body", WeaponAndBody);

        enemyHealthUI = (GameObject)EditorGUILayout.ObjectField("Enemy Health UI", enemyHealthUI, typeof(GameObject), true);

        enemySprite = (Sprite)EditorGUILayout.ObjectField("Body Sprite", enemySprite, typeof(Sprite), true);

        if (WeaponAndBody)
        {
            weaponSprite = (Sprite)EditorGUILayout.ObjectField("Legs Sprite", weaponSprite, typeof(Sprite), true);
        }

        GUILayout.EndVertical();

        if (GUILayout.Button("Create Enemy"))
        {
            enemy = new GameObject();
            enemy.name = "Enemy";
            enemy.tag = "Enemy";
            enemy.layer = LayerMask.NameToLayer("Enemy");
            EnemyComponents();

            if (WeaponAndBody)
            {
                weapon = new GameObject();
                weapon.name = "Weapon";
                weapon.transform.parent = enemy.transform;
                weapon.layer = LayerMask.NameToLayer("Enemy");
                WeaponComponents();
            }

            spawn = new GameObject();
            spawn.name = "Bullet Spawn Point";
            spawn.transform.parent = enemy.transform;
            spawn.layer = LayerMask.NameToLayer("Enemy");

            if (enemyHealthUI != null)
            {
                GameObject clone = Editor.Instantiate(enemyHealthUI);
                clone.name = "Enemy Health";
                clone.transform.parent = enemy.transform;
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    void EnemyComponents()
    {
        enemy.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        if(enemySprite!=null)
        sr.sprite = enemySprite;

        switch (cType)
        {
            case ColliderType.BoxCollider2D:
                enemy.AddComponent<BoxCollider2D>();
                break;
            case ColliderType.CapsuleCollider2D:
                enemy.AddComponent<CapsuleCollider2D>();
                break;
            case ColliderType.CircleCollider2D:
                enemy.AddComponent<CircleCollider2D>();
                break;
            case ColliderType.PolygonCollider2D:
                enemy.AddComponent<PolygonCollider2D>();
                break;
        }

        enemy.AddComponent<Animator>();

        /*
        enemy.AddComponent<Seeker>();
        enemy.AddComponent<AIPath>();
        AIPath ap = enemy.GetComponent<AIPath>();
        ap.enabled = false;
        enemy.AddComponent<Pathfinding.SimpleSmoothModifier>();
        enemy.AddComponent<Pathfinding.DynamicGridObstacle>();
        */  
        
        enemy.AddComponent<Enemy>();
    }

    void WeaponComponents()
    {
        weapon.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = weapon.GetComponent<SpriteRenderer>();
        sr.sprite = weaponSprite;
    }
}
