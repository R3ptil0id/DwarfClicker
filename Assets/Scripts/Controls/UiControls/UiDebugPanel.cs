// using Controllers.DepositoryControllers;
// using Controls.InputsControls;
// using Enums;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using Utils.Ioc;
//
// namespace Controls.UiControls
// {
//     [RegistrateMonoBehaviourInIoc]
//     public class UiDebugPanel : BaseControl
//     {
//         [Inject] private DepositoryController _depositoryController;
//         [Inject] private CurrenciesInputControl _currenciesInputControl;
//
//         [SerializeField] private TMP_InputField _inputField;
//
//         public override void Initialize()
//         {
//             var b = true;
//             base.Initialize();
//         }
//         public void SetActive()
//         {
//             gameObject.SetActive(true);
//         }
//
//         public UiDebugPanel() : base()
//         {
//             var b = true;
//         }
//         public void Close()
//         {
//             gameObject.SetActive(false);
//         }
//
//         #region AddCurrency
//
//         public void ClickAddCurrency0()
//         {
//             if (GetValue(out var value)) return;
//
//             _depositoryController.AddCurrency(CurrencyType.Currency0, value);
//         }
//
//         public void ClickAddCurrency1()
//         {
//             if (GetValue(out var value)) return;
//             _depositoryController.AddCurrency(CurrencyType.Currency1, value);
//         }
//         
//         public void ClickAddCurrency2()
//         {
//             if (GetValue(out var value)) return;
//             _depositoryController.AddCurrency(CurrencyType.Currency2, value);
//         }
//         
//         public void ClickAddCurrency3()
//         {
//             if (GetValue(out var value)) return;
//             _depositoryController.AddCurrency(CurrencyType.Currency3, value);
//         }
//         
//         public void ClickAddCurrency4()
//         {
//             if (GetValue(out var value)) return;
//             _depositoryController.AddCurrency(CurrencyType.Currency4, value);
//         }
//         #endregion
//
//         #region AddMaxCurrency
//
//         public void ClickAddMaxCurrency0()
//         {
//             _currenciesInputControl.ClickAddMaxCurrency0();
//         }
//         
//         public void ClickAddMaxCurrency1()
//         {
//             _currenciesInputControl.ClickAddMaxCurrency1();
//         }
//         
//         public void ClickAddMaxCurrency2()
//         {
//             _currenciesInputControl.ClickAddMaxCurrency2();
//         }
//         
//         public void ClickAddMaxCurrency3()
//         {
//             _currenciesInputControl.ClickAddMaxCurrency3();
//         }
//         
//         public void ClickAddMaxCurrency4()
//         {
//             _currenciesInputControl.ClickAddMaxCurrency4();
//         }
//         #endregion
//         private bool GetValue(out float value)
//         {
//             if (!float.TryParse(_inputField.text, out value))
//             {
//                 return true;
//             }
//
//             return false;
//         }
//     }
// }