using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerInteract : MonoBehaviour
{
    #region variaveis
    private Camera cam;
    public Transform objectHolder;

    [Header("Layers")]
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private LayerMask PickUp;
    private GameObject currentObject;
    private Rigidbody currentObjectRb;

    [SerializeField]
    private LayerMask BookClick;

    [SerializeField]
    private LayerMask CofreLayer;

    [SerializeField]
    private LayerMask elevatorLayer;

    [SerializeField]
    private LayerMask gavetaAnimacaoLayer;

    [SerializeField]
    private LayerMask calculadora;

    private float currentThrowForce;
    public float maxThrowForce;
    public float throwForceIncreaseSpeed;

    private InputManager inputManager;

    [Header("Keypad")]
    public GameObject player;
    public GameObject keypad;
    public GameObject book;

    public Text text;
    public string answer = "1796";

    [Header("Anima��oDoor")]
    public GameObject door;
    private bool isOpen;

    [Header("Anima��oCofre")]
    public GameObject cofreDoor;
    private bool cofreAberto;

    [Header("Cofre")]
    public GameObject cofre;
    public Text textCofre;
    public string password = "1080";

    public GameObject doorCofre;
    public GameObject cofreKeypad;
    public GameObject chave;

    [Header("Elevador")]
    public GameObject elevador;
    private bool up;

    [Header("GavetaAnim")]
    public GameObject gaveta;
    private bool gavetaAbre;

    [Header("Calculator")]
    public GameObject Calculator;
    public TextMeshProUGUI InputText;
    private float _result;
    private float _input;
    private float _input2;
    private string _operation;
    private string _currentInput;
    private bool _equalIsPressed;

    [Header("Inventory")]
    public GameObject panelInventory;
    public InventoryObjects inventory;
    public GameObject calculator;
    public GameObject livro;

    [Header("Tutorial")]
    public GameObject tutorial;

    [Header("Monitor")]
    [SerializeField] private LayerMask left;
    [SerializeField] private LayerMask right;
    [SerializeField] private LayerMask Up;
    [SerializeField] private LayerMask Down;

    public GameObject Camera;

    [Header("Xilofone")] 
    public LayerMask xilofone;
    public GameObject xilofoneCanvas;
    public GameObject Acertou;

    [Header("Audio")] 
    public AudioClip pickupSound;
    public AudioClip letgoSound;
    public AudioClip keypadClickSound;
    public AudioSource audioSource2;

    //private bool canTp = false;
    #endregion

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();

        Time.timeScale = 0f;
        
        keypad.SetActive(false);
        book.SetActive(false);
        cofre.SetActive(false);
        chave.SetActive(false);
        Calculator.SetActive(false);
        panelInventory.SetActive(false);
        calculator.SetActive(false);
        livro.SetActive(false);
        xilofoneCanvas.SetActive(false);
        Acertou.SetActive(false);
    }

    private void Update()
    {

        if (keypad.activeInHierarchy)
        {
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (cofre.activeInHierarchy)
        {
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Calculator.activeInHierarchy)
        {
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (tutorial.activeInHierarchy)
        {
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (xilofoneCanvas.activeInHierarchy)
        {
            player.GetComponent<CharacterController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        PickUpObject();
        Throw();
        KeyPad();
        Book();
        Cofre();
        ElevadorAnim();
        GavetaAnim();
        Calculadora();
        Tutorial();
        Inventory();
        Monitor();
        Xilofone();
    }

    #region PickUpObjects
    private void PickUpObject()
    {
        if (currentObject != null)
        {
            return;
        }


        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, PickUp))
        {
            if (inputManager.onFoot.canPickUp.triggered)
            {
                currentObject = hitInfo.collider.gameObject;
                currentObjectRb = currentObject.GetComponent<Rigidbody>();

                currentObject.transform.parent = objectHolder;
                currentObject.transform.localPosition = Vector3.zero;
                currentObject.transform.localEulerAngles = Vector3.zero;
                
                audioSource2.PlayOneShot(pickupSound);

                foreach (Collider collider in currentObject.GetComponents<Collider>())
                {
                    collider.enabled = false;
                }

                currentObjectRb.isKinematic = true;
            }
        }
    }
    #endregion

    #region Throw
    private void Throw()
    {
        if (currentObject == null)
        {
            return;
        }

        if (inputManager.onFoot.Throw.triggered)
        {
            if (currentThrowForce <= maxThrowForce)
            {
                currentThrowForce += throwForceIncreaseSpeed * Time.deltaTime;
            }
        }

        if (currentThrowForce > maxThrowForce)
        {
            currentThrowForce = maxThrowForce;
        }

        if (inputManager.onFoot.Throw.triggered)
        {
            audioSource2.PlayOneShot(letgoSound);
            currentObject.transform.parent = null;

            currentObjectRb.isKinematic = false;
            currentObjectRb.AddForce(currentObject.transform.forward * currentThrowForce, ForceMode.Impulse);

            foreach (Collider collider in currentObject.GetComponents<Collider>())
            {
                collider.enabled = true;
            }

            currentObject = null;
            currentObjectRb = null;

            currentThrowForce = 0f;
        }
    }
    #endregion

    #region Keypad
    void KeyPad()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (inputManager.onFoot.Interact.triggered)
            {
                Time.timeScale = 0f;
                keypad.SetActive(true);
            }
        }
    }

    public void Number(int number)
    {
        text.text += number.ToString();
        audioSource2.PlayOneShot(keypadClickSound);
    }

    public void Execute()
    {
        audioSource2.PlayOneShot(keypadClickSound);
        if (text.text == answer)
        {
            text.text = "Correct";

            isOpen = !isOpen;
            door.GetComponent<Animator>().SetBool("IsOpen", isOpen);

            keypad.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            text.text = "Wrong";
        }
    }

    public void Clear()
    {
        {
            text.text = "";
            audioSource2.PlayOneShot(keypadClickSound);
        }
    }

    public void Exit()
    {
        text.text = "";
        keypad.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        audioSource2.PlayOneShot(keypadClickSound);
    }
    #endregion

    #region Book
    private void Book()
    {
        if (inputManager.onFoot.ExitBook.triggered)
        {
            book.SetActive(false);
        }
    }
    #endregion

    #region CofreKeypad
    void Cofre()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, CofreLayer))
        {
            if (inputManager.onFoot.Interact.triggered)
            {
                Time.timeScale = 0f;
                cofre.SetActive(true);
            }
        }
    }

    public void NumberCofre(int number)
    {
        textCofre.text += number.ToString();
        audioSource2.PlayOneShot(keypadClickSound);
    }

    public void ExecuteCofre()
    {
        audioSource2.PlayOneShot(keypadClickSound);
        if (textCofre.text == password)
        {
            textCofre.text = "Correct";
            chave.SetActive(true);

            cofreAberto = !cofreAberto;
            cofreDoor.GetComponent<Animator>().SetBool("CofreAberto", cofreAberto);

            cofre.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;

        }
        else
        {
            textCofre.text = "Wrong";
        }
    }

    public void ClearCofre()
    {
        {
            textCofre.text = "";
            audioSource2.PlayOneShot(keypadClickSound);
        }
    }

    public void ExitCofre()
    {
        textCofre.text = "";
        cofre.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        audioSource2.PlayOneShot(keypadClickSound);
    }
    #endregion

    #region Elevator
    void ElevadorAnim()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, elevatorLayer))
        {
            if (inputManager.onFoot.Interact.triggered)
            {
                up = !up;
                elevador.GetComponent<Animator>().SetBool("Elevador", up);
            }
        }
    }
    #endregion

    #region Calculator

    private void Calculadora()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, calculadora))
        {
            if (inputManager.onFoot.Interact.triggered)
            {
                Time.timeScale = 0f;
                Calculator.SetActive(true);
            }
        }
    }

    public void ClickNumber(int val)
    {
        audioSource2.PlayOneShot(keypadClickSound);
        Debug.Log($" check val: {val}");
        if (!string.IsNullOrEmpty(_currentInput))
        {
            if (_currentInput.Length < 10)
            {
                _currentInput += val;
            }
        }
        else
        {
            _currentInput = val.ToString();
        }
        InputText.text = $"{_currentInput}";
    }

    public void ClickOperation(string val)
    {
        audioSource2.PlayOneShot(keypadClickSound);
        Debug.Log($" ClickOperation val: {val}");
        if (_input == 0)
        {
            SetCurrentInput();
            _operation = val;
        }
        else
        {
            if (_equalIsPressed)
            {
                _equalIsPressed = false;
                _operation = val;
                _input2 = 0;
            }
            else
            {
                if (_operation.Equals(val, System.StringComparison.OrdinalIgnoreCase))
                {
                    Calculate();
                }
                else
                {
                    _operation = val;
                    _input2 = 0;
                }
            }
        }
    }
    public void ClickEqual(string val)
    {
        audioSource2.PlayOneShot(keypadClickSound);
        Debug.Log($" ClickEqual val: {val}");
        Calculate();
        _equalIsPressed = true;
    }
    private void Calculate()
    {
        if (_input != 0 && !string.IsNullOrEmpty(_operation))
        {
            SetCurrentInput();
            switch (_operation)
            {
                case "+":
                    _result = _input + _input2;
                    break;
                case "-":
                    _result = _input - _input2;
                    break;
                case "*":
                    _result = _input * _input2;
                    break;
                case "/":
                    _result = _input / _input2;
                    break;
            }

            // show the result
            InputText.SetText(_result.ToString());

            // save the last result for next calculation
            _input = _result;
        }
    }

    private void SetCurrentInput()
    {
        if (!string.IsNullOrEmpty(_currentInput))
        {
            if (_input == 0)
            {
                _input = int.Parse(_currentInput);
            }
            else
            {
                _input2 = int.Parse(_currentInput);
            }
            _currentInput = "";
        }
    }

    // clear all the inputs
    public void ClearInput()
    {
        audioSource2.PlayOneShot(keypadClickSound);
        _currentInput = "";
        _input = 0;
        _input2 = 0;
        _result = 0;
        InputText.SetText("");
    }

    public void ExitCalculator()
    {
        audioSource2.PlayOneShot(keypadClickSound);
        _currentInput = "";
        _input = 0;
        _input2 = 0;
        _result = 0;
        InputText.SetText("");
        Calculator.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    #endregion Calculator

    #region GavetaAnimacao
    private void GavetaAnim()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, gavetaAnimacaoLayer))
        {
            if (inputManager.onFoot.Interact.triggered)
            {
                gavetaAbre = !gavetaAbre;
                gaveta.GetComponent<Animator>().SetBool("GavetaAbrir", gavetaAbre);
            }
        }
    }
    #endregion

    #region Tutorial
    private void Tutorial()
    {

    }
    public void ExitTutorial()
    {
        tutorial.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    #endregion

    #region Inventory
    private void Inventory()
    {
        if (inputManager.onFoot.Inventory.triggered)
        {
            panelInventory.SetActive(!panelInventory.activeSelf);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item.name == "Calculator")
        {
            inventory.AddItem(item.item, 1);
            Destroy(item.gameObject);
            calculator.SetActive(true);
        }

        if (item.name == "Livro")
        {
            inventory.AddItem(item.item, 1);
            Destroy(item.gameObject);
            livro.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public void CalculadoreClick()
    {
        Calculator.SetActive(true);
        panelInventory.SetActive(false);
    }

    public void LivroClick()
    {
        book.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        panelInventory.SetActive(false);
    }
    #endregion

    #region Monitor

    private void Monitor()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, left))
        {
            if (inputManager.onFoot.Throw.IsPressed())
            {
                Camera.transform.Rotate(-0.5f, 0.0f, 0.0f);
            }
        }

        if (Physics.Raycast(ray, out hitInfo, distance, right))
        {
            if (inputManager.onFoot.Throw.IsPressed())
            {
                Camera.transform.Rotate(0.5f, 0.0f, 0.0f);
            }
        }

        if (Physics.Raycast(ray, out hitInfo, distance, Up))
        {
            if (inputManager.onFoot.Throw.IsPressed())
            {
                Camera.transform.Rotate(0f, -0.5f, 0.0f);
            }
        }

        if (Physics.Raycast(ray, out hitInfo, distance, Down))
        {
            if (inputManager.onFoot.Throw.IsPressed())
            {
                Camera.transform.Rotate(0f, 0.5f, 0.0f);
            }
        }
    }

    #endregion

    #region Xilofone

    public void Xilofone()
    {
        var ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, xilofone))
        {
            if (inputManager.onFoot.canPickUp.triggered)
            {
                xilofoneCanvas.SetActive(true);
            }
        }
    }

    public void ExitXilofone()
    {
        Acertou.SetActive(false);
        xilofoneCanvas.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

}
