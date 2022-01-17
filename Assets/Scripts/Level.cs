using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;
        [SerializeField] private GameObject[] _numberPrefab;
        [SerializeField] private GameObject[] _literaPrefab;

        [SerializeField] private float duration;
        [SerializeField] private float strength;
        [SerializeField] private int vibrato;
        [SerializeField] private float randomness;

        private ArrayList questionList = new ArrayList(20);
        public ArrayList answersList = new ArrayList();

        private int _currentLevel;

        public Action<int, int> SetAnswer;

        [SerializeField] private bool isNumber;

        private int spawnPoints;

        void Start()
        {
            if (isNumber == true)
            {
                for (int i = 0; i < _numberPrefab.Length; i++)
                {
                    questionList.Add(i);
                }
            }
            else
            {
                for (int i = 0; i < _literaPrefab.Length; i++)
                {
                    questionList.Add(i);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                BounceEffect(i);
            }

            Answer();
        }

        private void Answer()
        {
            int answerId = Random.Range(0, (answersList.Count - 1));
            int k = (int)answersList[answerId];

            SetAnswer?.Invoke(answerId, k);
            answersList.Clear();
            _currentLevel++;
        }

        public void LoadLevel()
        {
            Debug.Log(_currentLevel);

            if (_currentLevel == 1)
            {
                spawnPoints = 6;
            }
            else if (_currentLevel == 2)
            {
                spawnPoints = 3;
            }

            for (int i = 0; i < spawnPoints; i++)
            {
                SpawnNumbers(i);
            }

            Answer();
        }

        private void BounceEffect(int i)
        {
            SpawnNumbers(i);

            _buttons[i].gameObject.SetActive(true);
            _buttons[i].transform.DOShakePosition(duration, strength, vibrato, randomness);
        }

        private void SpawnNumbers(int i)
        {
            int r = Random.Range(0, (questionList.Count - 1));
            int k = (int)questionList[r];

            if (isNumber == true)
            {
                GameObject _number = Instantiate(_numberPrefab[k], _buttons[i].transform.position, Quaternion.identity);
                _number.transform.parent = _buttons[i].transform;
            }
            else
            {
                GameObject _number = Instantiate(_literaPrefab[k], _buttons[i].transform.position, Quaternion.identity);
                _number.transform.parent = _buttons[i].transform;
            }


            questionList.RemoveAt(r);

            answersList.Add(k+1); 
        }

    }
}
