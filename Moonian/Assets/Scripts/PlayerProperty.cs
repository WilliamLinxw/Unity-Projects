using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public static PlayerProperty Instance;
    public float maxHealth, maxO2, maxLS = 100f;
    private float _currentHealth, _currentO2, _currentLS;
    public float currentHealth
    {
        get { return _currentHealth; }
    }
    public float currentO2
    {
        get { return _currentO2;}
    }
    public float currentLS
    {
        get { return _currentLS;}
    }
    public float o2HealthRate, o2ConsumeRate;
    public float lsHealthRate, lsConsumeRate;
    public bool isInBase, isInVehicle = false;
    public bool isInShelters {get {return _isInShelters;}}
    bool _isInShelters;
    public HealthBar healthBar;
    public O2Bar o2Bar;
    public LSBar lsBar;

    private float minHealth, minO2, minLS = 0;
    private int checkHealthState, checkO2State, checkLSState;
    private float _runningConsumingRate;
    

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitializeProperties();
    }

    void Update()
    {
        _isInShelters = (isInBase) && (isInVehicle);
        if (PlayerMovement.Instance.isRunning)
        {
            _runningConsumingRate = 2;
        }
        else
        {
            _runningConsumingRate = 1;
        }
        checkHealthState = CheckPropRange(_currentHealth, maxHealth, minHealth);
        checkO2State = CheckPropRange(_currentO2, maxO2, minO2);
        checkLSState = CheckPropRange(_currentLS, maxLS, minLS);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        if (checkHealthState == 1)
        {
            Death();
        }
        if (checkO2State == 1)
        {
            TakeDamage(o2HealthRate);  // when the oxygen level <= 0 -> start to lose health
        }
        if (checkLSState == 1)
        {
            TakeDamage(lsHealthRate);
        }

        ConsumeO2();
        ConsumeLS();

        UpdateProperties();
    }
    void UpdateProperties()
    {
        healthBar.SetValue(_currentHealth);
        o2Bar.SetValue(_currentO2);
        lsBar.SetValue(_currentLS);
    }

    void TakeDamage(float _dmg)
    {
        _currentHealth -= _dmg;
    }
    public void Cure(float _cure)
    {
        _currentHealth += _cure;

    }
    public void CureUpTo(float _cureTarget)
    {
        if (_currentHealth <= _cureTarget)
        {
            _currentHealth = _cureTarget;
        }
    }
    
    void ConsumeO2()
    {
        if (!_isInShelters)
        { 
            float ocr = o2ConsumeRate * _runningConsumingRate;
            _currentO2 -= ocr;
        }
    }
    public void SupplyO2(float _supply)
    {
        _currentO2 += _supply;
    }
    public void SupplyO2UpTo(float _supply)
    {
        if (_currentO2 <= _supply)
        {
            _currentO2 = _supply;
        }
    }

    void ConsumeLS()
    {
        if (!_isInShelters)
        {
            _currentLS -= lsConsumeRate;
        }
    }
    public void SupplyLS(float _supply)
    {
        _currentLS += _supply;
    }
    public void SupplyLSUpTo(float _supply)
    {
        if (_currentLS <= _supply)
        {
            _currentLS = _supply;
        }
    }

    void InitializeProperties()
    {
        _currentHealth = maxHealth;
        _currentO2 = maxO2;
        healthBar.ResetMaxValue(maxHealth);
        healthBar.SetValue(_currentHealth);
        o2Bar.SetValue(_currentO2);
        _currentLS = maxLS;
        lsBar.SetValue(_currentLS);
    }
    int CheckPropRange(float prop, float propMax, float propMin)
    {
        if (prop >= propMax)
        {
            prop = propMax;
            return 0;
        }
        if (prop <= propMin)
        {
            prop = propMin;
            return 1;
        }
        return -1;
    }
    void Death()
    {

    }
}
