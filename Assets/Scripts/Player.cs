using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Yarn.Unity;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Damageable))]

public class Player : MonoBehaviour
{
    private static Player playerInstance;

    // Singleton behavior
    public static Player MyPlayer { get { return playerInstance; } }

    private static float LEFT_CAMERA_LIMIT = -35f;
    private static float RIGHT_CAMERA_LIMIT = 35f;
    private static float PARALLAX_FACTOR = 5f;
    // private static Vector3 dialogueBoxDisplacement = new Vector3(0, 100, 0);

    // Outside GameObjects and fields we want access to
    [SerializeField] private Camera mainCamera;
    // [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Transform backgroundParent;
    [SerializeField] public List<Ability> abilities = new List<Ability>();
    [SerializeField] private TargetReticle basAtkRet;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;
    private float x_vel;
    private Vector2 facing;
    private bool controllable;
    private int maxHealth = 30;
    [SerializeField] private int currHealth = 30;
    private float regenWait = 5.0f;
    private float timeUntilRegen = 5.0f;
    private int regenAmount = 1;
    private float jumpWait = 2.0f;
    private float timeUntilJump = 0.0f;

    // For dealing with world interaction
    [SerializeField] private LayerMask interactableMask;
    private ContactFilter2D interactableFilter;

    // For checking if Player is grounded
    private bool grounded;
    private BoxCollider2D groundedBox;
    private BoxCollider2D interactBox;
    [SerializeField] private LayerMask groundMask;

    // Sprite 
    private SpriteRenderer sp;

    // Quests
    [SerializeField] private Quest currentQuest;
    [SerializeField] private List<Quest> quests;
    [SerializeField] private GameObject questCanvas;
    [SerializeField] private GameObject dialogueSystem;
    [SerializeField] private GameObject eventManager;
    private DialogueRunner dialogueRunner;
    private QuestManager questManager;

    public int stage = -1;
    public bool paused;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        /*
        if (playerInstance == null) {
            playerInstance = this;
        } else {
            Destroy(gameObject);
        }
        */
        if (playerInstance != null && playerInstance != this)
        {
            Destroy(this.gameObject);
        } else {
            playerInstance = this;
        }
        SceneManager.activeSceneChanged += OnSceneLoad;

        rb = this.GetComponent<Rigidbody2D>();
        basAtkRet = null;
        Transform targetRet = this.gameObject.transform.Find("TargetReticle");
        if (targetRet != null)
        {
            basAtkRet = this.gameObject.transform.Find("TargetReticle").GetComponent<TargetReticle>(); // Hardcoded name but shouldn't be an issue
        }
        abilities.Add(new Test1());
        abilities.Add(new Test2());
        abilities.Add(new Test3());
        abilities.Add(new Test4());
        abilities.Add(new BasicAttack(basAtkRet)); // Treat basic attack like any other ability
        foreach (BoxCollider2D bx in this.GetComponents<BoxCollider2D>())
        {
            if (bx.isTrigger)
            {
                groundedBox = bx;
            }
            else
            {
                interactBox = bx;
            }
        }

        interactableFilter = new ContactFilter2D();
        interactableFilter.SetLayerMask(interactableMask);
        interactableFilter.useTriggers = true;
        x_vel = 0;

        controllable = true;
        paused = false;
        sp = GetComponent<SpriteRenderer>();
        currentQuest = quests[0];
    }

    void Start()
    {
        DontDestroyOnLoad(questCanvas);
        // DontDestroyOnLoad(dialogueSystem);
        DontDestroyOnLoad(eventManager);
        dialogueRunner = dialogueSystem.GetComponent<DialogueRunner>();

        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        if (backgroundParent == null)
        {
            backgroundParent = GameObject.Find("WorldBackground").transform;
        }

        OnSceneLoad(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
        questManager = questCanvas.transform.GetChild(0).gameObject.GetComponent<QuestManager>();
        questManager.initQuest();
    }

    public void OnMove(InputValue input)
    {
        if (controllable && !paused)
        {
            x_vel = input.Get<Vector2>().x * speed;

            // Handles direction character is facing
            if (x_vel > 0)
            {
                facing = Vector2.right;
            }
            else if (x_vel < 0)
            {
                facing = Vector2.left;
            }


            float yInput = input.Get<Vector2>().y;
            if (yInput > 0)
            {
                if (groundedBox.IsTouchingLayers(groundMask.value) && timeUntilJump == 0)
                {
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    timeUntilJump += jumpWait;
                }
            }
        } else {
            x_vel = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void OnInteract(InputValue input)
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D temp = new ContactFilter2D();
        temp.layerMask = groundMask;
        interactBox.OverlapCollider(interactableFilter, results);
        foreach (var e in results)
        {
            e.transform.gameObject.GetComponent<Interactable>().interact(this);
        }
    }

    public void FixedUpdate()
    {
        if (currHealth <= 0) {
            SceneManager.LoadScene("MainMenu");
            currHealth = 30;
            Destroy(GameObject.Find("PauseCanvas"));
            Destroy(GameObject.Find("QuestCanvas"));
            Destroy(this.gameObject);
            return;
        }

        if (!paused) {
            rb.velocity = new Vector2(x_vel, rb.velocity.y);
            foreach (var ability in abilities)
            {
                ability.currentCooldown -= Time.deltaTime;
                if (ability.currentCooldown < 0)
                {
                    ability.currentCooldown = 0;
                }
            }

            timeUntilRegen -= Time.deltaTime;
            if (timeUntilRegen < 0) {
                timeUntilRegen = regenWait;
                /*
                Damageable myDmg = this.GetComponent<Damageable>();
                if (myDmg.getHealthLeft() < maxHealth) {
                    myDmg.applyDamage(-1*regenAmount);
                    if (myDmg.getHealthLeft() > maxHealth) {
                        myDmg.setHealthLeft(maxHealth);
                    }
                }
                */
                currHealth += regenAmount;
                if (currHealth > maxHealth) {
                    currHealth = maxHealth;
                }
            }

            timeUntilJump -= Time.deltaTime;
            if (timeUntilJump < 0) {
                timeUntilJump = 0;
            }

            sp.flipX = facing == Vector2.right;
            if (dialogueRunner != null && dialogueRunner.IsDialogueRunning) { rb.velocity = Vector3.zero; }
            if (mainCamera != null) { setCamera(); }
        } else {
            // Time.timeScale = 0 here so won't ever enter "else" section
        }
    }

    public void OnUseAbility1(InputValue input) { abilities[0].action(); }

    public void OnUseAbility2(InputValue input) { abilities[1].action(); }

    public void OnUseAbility3(InputValue input) { abilities[2].action(); }

    public void OnUseAbility4(InputValue input) { abilities[3].action(); }

    public void OnUseBasicAttack(InputValue input) { abilities[4].action(); }

    public void putOnCooldown(Ability ability)
    {
        if (!abilities.Contains(ability))
        {
            return;
        }
        else
        {
            ability.currentCooldown = ability.getCooldown();
        }
    }

    /* Assorted Helpers for Clarity */

    // Sets camera position
    // and calculates parallax
    private void setCamera()
    {
        mainCamera.transform.position = new Vector3(getCameraXPos(), 0, mainCamera.transform.position.z);
        for (int i = 0; i < backgroundParent.childCount - 2; i++)
        {
            float targetX = mainCamera.transform.position.x / RIGHT_CAMERA_LIMIT * PARALLAX_FACTOR * i / (backgroundParent.childCount - 2);
            backgroundParent.GetChild(i).position = new Vector3(targetX, 0, 0);
        }
    }

    // Clamps X position of the camera
    private float getCameraXPos()
    {
        if (this.transform.position.x < LEFT_CAMERA_LIMIT)
        {
            return LEFT_CAMERA_LIMIT;
        }
        else if (this.transform.position.x > RIGHT_CAMERA_LIMIT)
        {
            return RIGHT_CAMERA_LIMIT;
        }
        else
        {
            return this.transform.position.x;
        }
    }

    public void OnSceneLoad(Scene current, Scene next)
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        backgroundParent = GameObject.Find("WorldBackground").transform;
        dialogueSystem = GameObject.Find("Dialogue System");
        if (dialogueSystem != null) {
            dialogueRunner = dialogueSystem.GetComponent<DialogueRunner>();
        } else {
            dialogueRunner = null;
        }
        questCanvas = GameObject.Find("QuestCanvas");
        //this.GetComponent<Damageable>().setHealthLeft(maxHealth);
        currHealth = maxHealth;
        // playNextDialogue(next.name);
    }

    public void playDialogue(string nodeName, string npcName) {
        if (dialogueRunner == null) {
            dialogueSystem = GameObject.Find("Dialogue System");
            dialogueRunner = dialogueSystem.GetComponent<DialogueRunner>();
        }
        if (questCanvas == null) {
            questCanvas = GameObject.Find("QuestCanvas");
        }
        questManager.talk(npcName);
        dialogueRunner.StartDialogue(nodeName);
        stage += 1;
        if (stage < quests.Count) {
            currentQuest = quests[stage];
            questManager.initQuest();
        }
    }

    public void revokeControl() { controllable = false; /*rb.velocity = Vector3.zero;*/ }
    public void returnControl() { controllable = true; }
    public bool isControllable() { return controllable; }
    public Quest getCurrentQuest() { return currentQuest; }

    // MOVED FROM PROJECTILES TO ACCOUNT FOR DontDestroyOnLoad
    void OnTriggerEnter2D(Collider2D p) 
    {
        if (p.gameObject.tag == "Projectile") {
            //this.GetComponent<Damageable>().applyDamage(1);
            currHealth -= 1;
            Destroy(p.gameObject);
        }
    }


}
