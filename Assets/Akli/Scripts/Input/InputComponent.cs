using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    [SerializeField] InputPoller m_VirtualController;
    [SerializeField] int m_ControllerBitFlag;
    int m_BitflagSize;
    [SerializeField] int m_PreviousControllerBitFlag;
    [SerializeField] InputState[] m_InputStates;

    public InputState InputStates(Button button) { return m_InputStates[(int)button]; }
    public InputState InputStates(CardinalDirection direction) { return m_InputStates[Enum.GetNames(typeof(Button)).Length + (int)direction]; }
    
    Character m_Character;

    private void Start()
    {
        if (m_VirtualController != null)
        {
            m_Character = GetComponent<Character>();
            m_InputStates = new InputState[m_VirtualController.BitflagSize];
            m_VirtualController.m_ControllerUpdate += UpdateInput;
            m_BitflagSize = m_VirtualController.BitflagSize;
        }
    }

    private void UpdateInput(int controllerBitflag)
    {
        if (m_Character.IsFlipped)
            m_ControllerBitFlag = SwapBits(controllerBitflag, (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.LEFT), (m_BitflagSize - Enum.GetNames(typeof(CardinalDirection)).Length + (int)CardinalDirection.RIGHT));
        else
            m_ControllerBitFlag = controllerBitflag;
    }

    private void Update()
    {
        if (m_VirtualController != null)
        {

            for (int i = 0; i < Enum.GetNames(typeof(Button)).Length; i++)
            {
                int bitmask = 1 << i;

                if ((m_ControllerBitFlag & bitmask) == bitmask)
                    m_InputStates[i] = InputState.PRESSED;
                if ((m_ControllerBitFlag & bitmask) == bitmask && (m_PreviousControllerBitFlag & bitmask) == bitmask)
                    m_InputStates[i] = InputState.HELD;
                if ((m_ControllerBitFlag & bitmask) != bitmask && (m_PreviousControllerBitFlag & bitmask) == bitmask)
                    m_InputStates[i] = InputState.RELEASED;
                if ((m_ControllerBitFlag & bitmask) != bitmask && (m_PreviousControllerBitFlag & bitmask) != bitmask)
                    m_InputStates[i] = InputState.NOT_PRESSED;
            }

            for (int i = Enum.GetNames(typeof(Button)).Length; i < Enum.GetNames(typeof(Button)).Length + Enum.GetNames(typeof(CardinalDirection)).Length; i++)
            {
                int bitmask = 1 << (i - (int)Enum.GetNames(typeof(Button)).Length + m_VirtualController.BitflagSize - (int)Enum.GetNames(typeof(CardinalDirection)).Length);

                if ((m_ControllerBitFlag & bitmask) == bitmask)
                    m_InputStates[i] = InputState.PRESSED;
                if ((m_ControllerBitFlag & bitmask) == bitmask && (m_PreviousControllerBitFlag & bitmask) == bitmask)
                    m_InputStates[i] = InputState.HELD;
                if ((m_ControllerBitFlag & bitmask) != bitmask && (m_PreviousControllerBitFlag & bitmask) == bitmask)
                    m_InputStates[i] = InputState.RELEASED;
                if ((m_ControllerBitFlag & bitmask) != bitmask && (m_PreviousControllerBitFlag & bitmask) != bitmask)
                    m_InputStates[i] = InputState.NOT_PRESSED;
            }
        }
        m_PreviousControllerBitFlag = m_ControllerBitFlag;
    }

    int SwapBits(int n, int p1, int p2)
    {
        /* Move p1'th to rightmost side */
        int bit1 = (n >> p1) & 1;

        /* Move p2'th to rightmost side */
        int bit2 = (n >> p2) & 1;

        /* XOR the two bits */
        int x = (bit1 ^ bit2);

        /* Put the xor bit back to their original positions */
        x = (x << p1) | (x << p2);

        /* XOR 'x' with the original number so that the
           two sets are swapped */
        int result = n ^ x;

        return result;
    }
}
