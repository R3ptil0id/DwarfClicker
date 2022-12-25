// namespace Controllers.Depository
// {
//     public class ComplexCurrencyBarController : CurrencyBarController
//     {
        // private List<Action> _listeners;
        // private readonly CurrencyBarControl_1 _control1;
        //
        // public int CurrentBarLvl { get; private set; }
        // public Vector3 Position => _control.transform.localPosition;
        //
        // public ComplexCurrencyBarController(CurrencyBarControl control) : base(control)
        // {
        //     _listeners = new List<Action>();
        //     _control1 = (CurrencyBarControl_1)_control;
        //     _control1.NotifyAnimationComplete += OnAnimationComplete;
        // }
        //
        // public void AddListener(Action listener)
        // {
        //     _listeners.Add(listener);
        // }
        //
        // public void AddLevel(int lvl)
        // {
        //     CurrentBarLvl += lvl;
        //     
        //     if(CurrentBarLvl == DataConstants.CurrencyValues[CurrencyLevel.Units_5])
        //     {
        //         CurrencyLevel = CurrencyLevel.Units_5;
        //         _control1.AddLevel(CurrentBarLvl, CurrencyLevel);
        //     }
        //     else
        //     {
        //         _control1.AddLevel(CurrentBarLvl);    
        //     }
        // }    
        //
        // private void OnAnimationComplete()
        // {
        //     foreach (var listener in _listeners)
        //     {
        //         listener?.Invoke();
        //     }
        //
        //     _control1.NotifyAnimationComplete -= OnAnimationComplete;
        //     _listeners = null;
        // }
//     }
// }