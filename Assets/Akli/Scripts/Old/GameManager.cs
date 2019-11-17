using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Old
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private Sol _Player1;
        [SerializeField] private Sol _Player2;

        [SerializeField] private Inputs _Player1Input;
        [SerializeField] private Inputs _Player2Input;

        [SerializeField] private TextMeshProUGUI _P1UI;
        [SerializeField] private TextMeshProUGUI _P2UI;
        [SerializeField] private TextMeshProUGUI _WinScreen;

        [SerializeField] private int _Rounds;

        private int _Player1Points;
        private int _Player2Points;



        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (_Player1Points > 0)
                _P1UI.text = _Player1Points.ToString();
            if (_Player2Points > 0)
                _P2UI.text = _Player2Points.ToString();
        }

        public void AddPlayer2Point()
        {
            if (_Player1Points < _Rounds)
            {
                _Player1Input.ResetInput();
                _Player2Input.ResetInput();
                _Player1Input.enabled = false;
                _Player2Input.enabled = false;
                _Player1Points++;
            }
        }

        public void AddPlayer1Point()
        {
            if (_Player2Points < _Rounds)
            {
                _Player1Input.ResetInput();
                _Player2Input.ResetInput();
                _Player1Input.enabled = false;
                _Player2Input.enabled = false;
                _Player2Points++;
            }
        }

        public void Reset1()
        {
            if (_Player1Points == _Rounds)
            {
                _Player1.enabled = false;
                _Player2.enabled = false;
                _WinScreen.gameObject.SetActive(true);
                _WinScreen.text = "WASD GH wins Press Start";
            }
            else
            {
                _Player1Input.enabled = true;
                _Player2Input.enabled = true;
                _Player1.SwitchState(new Idle(_Player1));
                _Player2.SwitchState(new Idle(_Player2));
                _Player1.transform.position = _Player1._StartPosition;
                _Player2.transform.position = _Player2._StartPosition;
            }
        }
        public void Reset2()
        {
            if (_Player2Points == _Rounds)
            {
                _Player1.enabled = false;
                _Player2.enabled = false;
                _WinScreen.gameObject.SetActive(true);
                _WinScreen.text = "Arrows 23 wins Press Start";
            }
            else
            {
                _Player1Input.enabled = true;
                _Player2Input.enabled = true;
                _Player1.SwitchState(new Idle(_Player1));
                _Player2.SwitchState(new Idle(_Player2));
                _Player1.transform.position = _Player1._StartPosition;
                _Player2.transform.position = _Player2._StartPosition;
            }
        }
    }
}
