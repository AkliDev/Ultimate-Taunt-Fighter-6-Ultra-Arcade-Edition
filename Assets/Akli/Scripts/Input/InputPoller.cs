using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPoller : MonoBehaviour
{
    public delegate void ControllerUpdate(int controllerBitFlag);
    public event ControllerUpdate m_ControllerUpdate;

    FGController m_Controller;
    int m_BitflagSize;
    public int BitflagSize { get { return m_BitflagSize; } }
    int m_ControllerBitflag;
    public int ControllerBitflag { get { return m_ControllerBitflag; } }

    // Start is called before the first frame update
    void Awake()
    {
        m_ControllerBitflag = 0;
        m_BitflagSize = Enum.GetNames(typeof(Button)).Length + Enum.GetNames(typeof(CardinalDirection)).Length;
    }

    public void OnEnable()
    {
        if (m_Controller == null)
            m_Controller = new FGController();

        m_Controller.Controller.Enable();

        m_Controller.Controller.Button1.performed += _ => OnPressedButton1();
        m_Controller.Controller.Button2.performed += _ => OnPressedButton2();
        m_Controller.Controller.Button3.performed += _ => OnPressedButton3();
        m_Controller.Controller.Button4.performed += _ => OnPressedButton4();
        m_Controller.Controller.Button5.performed += _ => OnPressedButton5();
        m_Controller.Controller.Button6.performed += _ => OnPressedButton6();
        m_Controller.Controller.Up.performed += _ => OnPressedUp();
        m_Controller.Controller.Down.performed += _ => OnPressedDown();
        m_Controller.Controller.Left.performed += _ => OnPressedLeft();
        m_Controller.Controller.Right.performed += _ => OnPressedRight();

        m_Controller.Controller.Button1.canceled += _ => OnReleasedButton1();
        m_Controller.Controller.Button2.canceled += _ => OnReleasedButton2();
        m_Controller.Controller.Button3.canceled += _ => OnReleasedButton3();
        m_Controller.Controller.Button4.canceled += _ => OnReleasedButton4();
        m_Controller.Controller.Button5.canceled += _ => OnReleasedButton5();
        m_Controller.Controller.Button6.canceled += _ => OnReleasedButton6();
        m_Controller.Controller.Up.canceled += _ => OnReleasedUp();
        m_Controller.Controller.Down.canceled += _ => OnReleasedDown();
        m_Controller.Controller.Left.canceled += _ => OnReleasedLeft();
        m_Controller.Controller.Right.canceled += _ => OnReleasedRight();
    }

    public void OnDisable()
    {
        m_Controller.Controller.Disable();
    }

    public void OnPressedButton1()
    {
        m_ControllerBitflag |= (1 << (int)Button.BUTTON1);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedButton2()
    {
        m_ControllerBitflag |= (1 << (int)Button.BUTTON2);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedButton3()
    {
        m_ControllerBitflag |= (1 << (int)Button.BUTTON3);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedButton4()
    {
        m_ControllerBitflag |= (1 << (int)Button.BUTTON4);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedButton5()
    {
        m_ControllerBitflag |= (1 << (int)Button.BUTTON5);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedButton6()
    {
        m_ControllerBitflag |= (1 << (int)Button.BUTTON6);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedUp()
    {
        m_ControllerBitflag |= (1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.UP));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedDown()
    {
        m_ControllerBitflag |= (1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.DOWN));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedLeft()
    {
        m_ControllerBitflag |= (1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.LEFT));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnPressedRight()
    {
        m_ControllerBitflag |= (1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.RIGHT));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedButton1()
    {
        m_ControllerBitflag &= ~(1 << (int)Button.BUTTON1);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedButton2()
    {
        m_ControllerBitflag &= ~(1 << (int)Button.BUTTON2);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedButton3()
    {
        m_ControllerBitflag &= ~(1 << (int)Button.BUTTON3);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedButton4()
    {
        m_ControllerBitflag &= ~(1 << (int)Button.BUTTON4);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedButton5()
    {
        m_ControllerBitflag &= ~(1 << (int)Button.BUTTON5);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedButton6()
    {
        m_ControllerBitflag &= ~(1 << (int)Button.BUTTON6);
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedUp()
    {
        m_ControllerBitflag &= ~(1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.UP));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedDown()
    {
        m_ControllerBitflag &= ~(1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.DOWN));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedLeft()
    {
        m_ControllerBitflag &= ~(1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.LEFT));
        m_ControllerUpdate(m_ControllerBitflag);
    }

    public void OnReleasedRight()
    {
        m_ControllerBitflag &= ~(1 << (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.RIGHT));
        m_ControllerUpdate(m_ControllerBitflag);
    }
}
