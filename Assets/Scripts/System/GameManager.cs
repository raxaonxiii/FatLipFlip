using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : StateMachine
{
    public static GameManager current = null;
    public Canvas canvas;
    public MainMenu mainMenu;
    public GameObject fadeAnim;
    public BoolEvent titleScreenUpdate, MainMenuUpdate, GameStartUpdate, Finished, Paused;
    public BoardAnimManager easyBoardAnim, medBoardAnim, hardBoardAnim, exHardBoardAnim;
    public TextMeshProUGUI timerDisplay, finishTimer, versionNum;
    public float timer, easyBestTime, medBestTime, hardBestTime, exHardBestTime;
    public bool easyBestSet, medBestSet, hardBestSet, exHardBestSet;
    private bool timerStarted;
    public Button pauseButton, quitButton, exitButton;
    public GameObject[] Parents;
    public List<Card> easyList, mediumList, hardList, extraHardList;
    public Vector3[] cardSize, easyPos, medPos, hardPos, exHardPos;
    public Card first, second;
    public Image finishMenu;

    void Awake()
    {
#if UNITY_STANDALONE_WIN
        Screen.SetResolution(421, 913, false);
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        if (current == null)
            current = this;

        canvas = GetComponent<Canvas>();
        versionNum.text = "VER. " + Application.version;
#if UNITY_STANDALONE_WIN
        quitButton.gameObject.SetActive(true);

#endif
        Load();
        SetState(new Begin(this));
    }

    void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
            DisplayTimeValue(timer);
        }
    }

    void Load()
    {
        SaveData data = SaveSystem.LoadSave();

        easyBestSet = data.easyBestSet;
        easyBestTime = data.bestEasyTime;

        if (easyBestTime == 0 && easyBestSet)
            easyBestSet = false;

        medBestSet = data.medBestSet;
        medBestTime = data.bestMedTime;

        if (medBestTime == 0 && medBestSet)
            medBestSet = false;

        hardBestSet = data.hardBestSet;
        hardBestTime = data.bestHardTime;

        if (hardBestTime == 0 && hardBestSet)
            hardBestSet = false;

        exHardBestSet = data.exHardBestSet;
        exHardBestTime = data.bestExHardTime;

        if (exHardBestTime == 0 && exHardBestSet)
            exHardBestSet = false;

        mainMenu.SetBest(easyBestTime, medBestTime, hardBestTime, exHardBestTime);
    }

    public void MainMenuToTitle()
    {
        StartCoroutine(MMTT());
    }

    private IEnumerator MMTT()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MainMenuUpdate?.Invoke(false);
        titleScreenUpdate?.Invoke(true);
        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    public void TitleToMainMenu()
    {
        StartCoroutine(TTMM());
    }

    private IEnumerator TTMM()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        titleScreenUpdate?.Invoke(false);
        MainMenuUpdate?.Invoke(true);
#if UNITY_STANDALONE_WIN
        exitButton.gameObject.SetActive(true);
#endif
        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    public void QuitGame()
    {
        StartCoroutine(TTQ());
    }

    private IEnumerator TTQ()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void ReturnToGameSelect()
    {
        State currentState = GetCurrentState();
        Paused?.Invoke(false);
        timer = 0;
        DisplayTimeValue(timer);
        SetTimerStarted(false);
        Finished?.Invoke(false);
        currentState.BoardAnimMan.EnableCards(false);
        currentState.BoardAnimMan.ResetCards();
        currentState.BoardAnimMan.EnableConnectedCard(true);
        currentState.BoardAnimMan.FlipCardsDown();
        StartCoroutine(GTMM());
    }

    private IEnumerator GTMM()
    {
        fadeAnim.SetActive(true);
        Load();
        yield return new WaitForSeconds(0.5f);
        GetCurrentState().BoardAnimMan.gameObject.SetActive(false);
        GameStartUpdate?.Invoke(false);
        MainMenuUpdate?.Invoke(true);
        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
        SetState(new Begin(this));
    }

    public void SetEasy()
    {
        StartCoroutine(Easy());
    }

    private IEnumerator Easy()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MainMenuUpdate?.Invoke(false);
        GameStartUpdate?.Invoke(true);

        foreach (GameObject parent in Parents)
            parent.SetActive(false);
        Parents[0].SetActive(true);

        SetState(new Easy(this));

        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    public void SetMedium()
    {
        StartCoroutine(Medium());
    }
    
    private IEnumerator Medium()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MainMenuUpdate?.Invoke(false);
        GameStartUpdate?.Invoke(true);

        foreach (GameObject parent in Parents)
            parent.SetActive(false);
        Parents[1].SetActive(true);

        SetState(new Medium(this));

        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    public void SetHard()
    {
        StartCoroutine(Hard());
    }
    
    private IEnumerator Hard()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MainMenuUpdate?.Invoke(false);
        GameStartUpdate?.Invoke(true);

        foreach (GameObject parent in Parents)
            parent.SetActive(false);
        Parents[2].SetActive(true);

        SetState(new Hard(this));

        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    public void SetExtraHard()
    {
        StartCoroutine(ExtraHard());
    }
    
    private IEnumerator ExtraHard()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MainMenuUpdate?.Invoke(false);
        GameStartUpdate?.Invoke(true);

        foreach (GameObject parent in Parents)
            parent.SetActive(false);
        Parents[3].SetActive(true);

        SetState(new ExtraHard(this));

        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    public BoardAnimManager GetEasyAnimMan()
    {
        return easyBoardAnim;
    }

    public BoardAnimManager GetMedAnimMan()
    {
        return medBoardAnim;
    }

    public BoardAnimManager GetHardAnimMan()
    {
        return hardBoardAnim;
    }

    public BoardAnimManager GetExHardAnimMan()
    {
        return exHardBoardAnim;
    }

    public void SetTimerStarted(bool value)
    {
        timerStarted = value;
    }

    public void DisplayTimeValue(float seconds)
    {
        int minutes = Mathf.FloorToInt((seconds / 60) % 60);
        int _seconds = Mathf.FloorToInt(seconds % 60);

        timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, _seconds);
    }

    public void DisplayFinishTime(float seconds)
    {
        int minutes = Mathf.FloorToInt((seconds / 60) % 60);
        int _seconds = Mathf.FloorToInt(seconds % 60);

        finishTimer.text = string.Format("{0:00}:{1:00}", minutes, _seconds);
    }

    public void SetCurrentParent(int current)
    {
        for(int i = 0; i < Parents.Length; ++i)
        {
            Parents[i].SetActive(false);
        }

        Parents[current].SetActive(true);
    }
    
    public List<Card> GetCardsEasy()
    {
        foreach (Card card in easyList)
            card.Init();
        return easyList;
    }

    public List<Card> GetCardsMedium()
    {
        foreach (Card card in mediumList)
            card.Init();
        return mediumList;
    }

    public List<Card> GetCardsHard()
    {
        foreach (Card card in hardList)
            card.Init();
        return hardList;
    }

    public List<Card> GetCardsExtraHard()
    {
        foreach (Card card in extraHardList)
            card.Init();
        return extraHardList;
    }
    
    public void FlipCard(Card card)
    {
        StartCoroutine(CheckForPair(card));
    }

    private IEnumerator CheckForPair(Card card)
    {
        foreach(Card button in GetCurrentState().cardsList)
        {
            button.gameObject.GetComponent<Button>().interactable = false;
        }

        if (!first)
            first = card;
        else
        {
            if (card == first)
                DeselectCards();
            else
                second = card;
        }

        card.FlipFaceUp();
        bool match;

        if (first && second)
        {
            if (first.GetCardID() == second.GetCardID())
            {
                match = true;
            }
            else
                match = false;

            if(match)
            {
                yield return new WaitForSeconds(0.5f);//card._anim.GetCurrentAnimatorStateInfo(0).length);
                Debug.Log("It's a match!");
                yield return new WaitForEndOfFrame();
                SFXManager.current.PlayMatch();
                GetCurrentState().MoveMatched(first, second);
                DeselectCards();
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);//card._anim.GetCurrentAnimatorStateInfo(0).length);
                Debug.Log("It's not a match...");
                yield return new WaitForEndOfFrame();
                first.FlipFaceDown();
                second.FlipFaceDown();
                DeselectCards();
                yield break;
            }
        }

        foreach (Card button in GetCurrentState().cardsList)
        {
            if(button != card)
                button.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void DeselectCards()
    {
        first = second = null;
        foreach (Card button in GetCurrentState().cardsList)
        {
            button.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void Pause(bool pause)
    {
        Paused?.Invoke(pause);
        SetTimerStarted(!pause);
    }

    public void Finish()
    {
        SetTimerStarted(false);
        float finishedTime = timer;
        SFXManager.current.PlayWin();
        Finished?.Invoke(true);
        DisplayFinishTime(finishedTime);
        GetCurrentState().SetBestTime(finishedTime);
        finishMenu.sprite = GetCurrentState().GetFinishMenu();
        SaveSystem.SaveTime(this);
    }

    public void SetEasyBestTime(float value)
    {
        easyBestTime = value;
    }

    public void SetMedBestTime(float value)
    {
        medBestTime = value;
    }

    public void SetHardBestTime(float value)
    {
        hardBestTime = value;
    }

    public void SetExHardBestTime(float value)
    {
        exHardBestTime = value;
    }

    public void ClearTimes()
    {
        easyBestTime = medBestTime = hardBestTime = exHardBestTime = 0;
        easyBestSet = medBestSet = hardBestSet = exHardBestSet = false;
    }

    public void Reset()
    {
        State currentState = GetCurrentState();
        Paused?.Invoke(false);
        timer = 0;
        DisplayTimeValue(timer);
        SetTimerStarted(false);
        Finished?.Invoke(false);
        currentState.BoardAnimMan.EnableCards(false);
        currentState.BoardAnimMan.ResetCards();
        currentState.BoardAnimMan.EnableConnectedCard(true);
        currentState.BoardAnimMan.FlipCardsDown();
        StartCoroutine(GetCurrentState().Start());
    }
}
