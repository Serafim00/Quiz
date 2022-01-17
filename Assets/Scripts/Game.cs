using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Level _level;

        [SerializeField] private Button[] _button;
        [SerializeField] private GameObject[] _secondLevel;
        [SerializeField] private GameObject[] _thirdLevel;
        [SerializeField] private GameObject _RestartButton;
        [SerializeField] private Text _questionText;

        private int answerId;
        private int _currentLevel;

        private void Start()
        {
            _level.SetAnswer += Question;

            foreach (Button button in _button)
            {
                button.OnClicked += OnSelectAnswer;
            }
        }

        private void Question(int ansId, int k)
        {
            _questionText.text = $"Find {_level.answersList[ansId]}";
            answerId = ansId;
            Invoke(nameof(QuestionSetActive), 1.5f);
        }

        private void QuestionSetActive()
        {
            _questionText.gameObject.SetActive(true);
            _questionText.DOFade(255, 2);
        }

        private void OnSelectAnswer(int i)
        {
            GameObject[] questions;
            questions = GameObject.FindGameObjectsWithTag("question");

            if (i == answerId)
            {
                NextLevel();                
            }
            else
            {
                _RestartButton.SetActive(true);
            }

            foreach (GameObject question in questions)
                Destroy(question);
        }

        public void Restsart()
        {
            SceneManager.LoadScene(0);
        }

        private void NextLevel()
        {
            _currentLevel++;

            if (_currentLevel == 1)
            {
                foreach (GameObject levelComponent in _secondLevel)
                    levelComponent.SetActive(true);
                _level.LoadLevel();
            }
            else if (_currentLevel == 2)
            {
                foreach (GameObject levelComponent in _thirdLevel)
                    levelComponent.SetActive(true);
                _level.LoadLevel();
            }
            else if (_currentLevel == 3)
            {
                _currentLevel = 0;
            }
        }

        private void OnDestroy()
        {
            _level.SetAnswer -= Question;

            foreach (Button button in _button)
            {
                button.OnClicked -= OnSelectAnswer;
            }
        }
    }
}
