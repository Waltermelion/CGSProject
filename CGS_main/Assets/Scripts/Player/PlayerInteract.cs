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
    private LayerMask InformationItem;

    [SerializeField]
    private LayerMask BookClick;

    [SerializeField]
    private LayerMask CofreLayer;

    [SerializeField]
    private LayerMask elevatorLayer;

    [SerializeField]
    private LayerMask QuadroKeypadLayer;

    [SerializeField]
    private LayerMask gavetaAnimacaoLayer;

    [SerializeField]
    private LayerMask botaoLayer;

    [SerializeField]
    private LayerMask botaoLayer3;

    [SerializeField]
    private LayerMask calculadora;

    private float currentThrowForce;
    public float maxThrowForce;
    public float throwForceIncreaseSpeed;

    private InputManager inputManager;

    [Header("Nome dos objetos")]
    public TextMeshProUGUI pickUpText;
    public TextMeshProUGUI itemInformation;

    [Header("Keypad")]
    public GameObject player;
    public GameObject keypad;
    public GameObject book;

    public Text text;
    public string answer = "1796";

    [Header("AnimaçãoDoor")]
    public GameObject door;
    private bool isOpen;

    [Header("AnimaçãoCofre")]
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

    [Header("AnimaçãoQuadro")]
    public GameObject quadroDoor;
    private bool quadroCai;

    [Header("Quadro")]
    public GameObject quadro;

    public Text textQuadro;
    public string passwordQuadro = "125";

    [Header("GavetaAnim")]
    public GameObject gaveta;
    private bool gavetaAbre;

    [Header("Animaçao Botao")]
    public GameObject botao;
    private bool botaoAbre;

    [Header("Animação Parede")]
    public GameObject parede;
    private bool paredeCai;

    [Header("Animação Botao3")]
    public GameObject roda;
    private bool casaRoda;

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

    //private bool canTp = false;
    #endregion

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();

        Time.timeScale = 0f;

        pickUpText.gameObject.SetActive(false);
        keypad.SetActive(false);
        book.SetActive(false);
        cofre.SetActive(false);
        chave.SetActive(false);
        quadro.SetActive(false);
        Calculator.SetActive(false);
        panelInventory.SetActive(false);
        calculator.SetActive(false);
        livro.SetActive(false);
        xilofoneCanvas.SetActive(false);
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

        if (quadro.activeInHierarchy)
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
        Information_Item();
        KeyPad();
        Book();
        Cofre();
        ElevadorAnim();
        QuadroKey();
        GavetaAnim();
        Botao();
        BotaoSeccao3();
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
            pickUpText.gameObject.SetActive(true);
            pickUpText.text = hitInfo.collider.name;

            if (inputManager.onFoot.canPickUp.triggered)
            {
                currentObject = hitInfo.collider.gameObject;
                currentObjectRb = currentObject.GetComponent<Rigidbody>();

                currentObject.transform.parent = objectHolder;
                currentObject.transform.localPosition = Vector3.zero;
                currentObject.transform.localEulerAngles = Vector3.zero;

                foreach (Collider collider in currentObject.GetComponents<Collider>())
                {
                    collider.enabled = false;
                }

                currentObjectRb.isKinematic = true;
            }
        }
        else
        {
            pickUpText.gameObject.SetActive(false);
            pickUpText.text = "";
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

    #region InformationItem
    private void Information_Item()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, InformationItem))
        {
            itemInformation.gameObject.SetActive(true);
            itemInformation.text = hitInfo.collider.name;
        }
        else
        {
            itemInformation.gameObject.SetActive(false);
            itemInformation.text = "";
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

    private void Number(int number)
    {
        text.text += number.ToString();
    }

    private void Execute()
    {
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

    private void Clear()
    {
        {
            text.text = "";
        }
    }

    private void Exit()
    {
        text.text = "";
        keypad.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

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
    }

    private void ExecuteCofre()
    {
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

    private void ClearCofre()
    {
        {
            textCofre.text = "";
        }
    }

    private void ExitCofre()
    {
        textCofre.text = "";
        cofre.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
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

    #region QuadroKeypad
    void QuadroKey()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, QuadroKeypadLayer))
        {
            if (inputManager.onFoot.Interact.triggered)
            {
                Time.timeScale = 0f;
                quadro.SetActive(true);
            }
        }
    }

    public void NumberQuadro(int number)
    {
        textQuadro.text += number.ToString();
    }

    private void ExecuteQuadro()
    {
        if (textQuadro.text == passwordQuadro)
        {
            textQuadro.text = "Correct";

            quadroCai = !quadroCai;
            quadroDoor.GetComponent<Animator>().SetBool("Cai", quadroCai);

            quadro.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            textQuadro.text = "Wrong";
        }
    }

    private void ClearQuadro()
    {
        {
            textQuadro.text = "";
        }
    }

    private void ExitQuadro()
    {
        textQuadro.text = "";
        quadro.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
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
        _currentInput = "";
        _input = 0;
        _input2 = 0;
        _result = 0;
        InputText.SetText("");
    }

    public void ExitCalculator()
    {
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

    #region Botao
    private void Botao()
    {
        var ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, botaoLayer))
        {
            if (inputManager.onFoot.Interact.triggered)
                botaoAbre = !botaoAbre;
            botao.GetComponent<Animator>().SetBool("BotaoCarregado", botaoAbre);
            paredeCai = !paredeCai;
            parede.GetComponent<Animator>().SetBool("ParedeCai", paredeCai);
        }
    }
    #endregion Botao

    #region BotaoSec3
    private void BotaoSeccao3()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, botaoLayer3))
        {
            if (inputManager.onFoot.Interact.triggered)
            {

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
